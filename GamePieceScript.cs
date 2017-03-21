using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GamePieceScript : MonoBehaviour
{
    private CellStateScript cellState;
    private UtilityScripts utility;
    private ThreatCheckScript winConditions;
    private DrawBoardScript drawBoard;

    public int placedPieceX;
    public int placedPieceY;

    void Start()
    {
        utility = gameObject.GetComponent<UtilityScripts>();
        winConditions = gameObject.GetComponent<ThreatCheckScript>();
        drawBoard = gameObject.GetComponent<DrawBoardScript>();
    }


    //Get available empty spaces on board. Replaces getPossibleMoves in negamax algorithm
    //use unmakeMove to undo the move after -negamax is called.    
    public List<BoardSpace> GetAvailableSpaces(int[,] gameBoard)
    {
        List<BoardSpace> spaces = new List<BoardSpace>();

        for (int x = 0; x < gameBoard.GetLength(0); x++)
        {
            for (int y = 0; y < gameBoard.GetLength(1); y++)
            {
                //Catch IndexOutOfRange expection if player tries to place piece outside gameBoard
                try
                {
                    //Get gameState of current cell
                    int cellGameState = gameBoard[x, y];

                    //If current cell filled, set gamestate of previous cell to player or ai piece. Check for win. Advance turn
                    if (cellGameState == 0)
                    {
                        //available space
                        spaces.Add(new BoardSpace(x, y));
                        break;
                    }

                    //If column is full and new piece is placed, ignore it. 
                    else if (y == 5 && (cellGameState == 1 || cellGameState == 2))
                    {
                        //Debug.Log("Column is full while getting spaces");
                        break;
                    }
                }

                //Log error if piece is placed outside game board
                catch (Exception e)
                {
                    Debug.LogException(e);
                    Debug.Log("Not legal move. Turn should not move on until legal move is made.");
                }
            }
        }

        return spaces;

    }
    


    //MakeMove for ai calculation in negamax
    public void MakeMove(int[,] gameBoard, BoardSpace space, int player)
    {
        //Debug.Log("MakeMove space x, y: " + space.x + ", " + space.y + "\n");

        //Debug.Log("gameBoard length: " + gameBoard.GetLength(0));

        gameBoard[space.x, space.y] = player;
    }


    //Method to place piece. Includes calculation to check for empty space. Only requires x position
    public void PlaceGamePiece(int[,] gameBoardStates, int cellX, bool virtualMove, int player)
    {
        //Set local variable PlayerTurn to value of global turn tracker
        bool playerTurn = gameObject.GetComponent<GameController>().playerTurn;
        int newGameState = player;
        int winState = 0;


        //Iterate down y axis of current column to check for empty cells
        for (int y = gameBoardStates.GetLength(1)-1; y >= 0; y--)
        {            
            //Catch IndexOutOfRange expection if player tries to place piece outside gameBoard
            try
            {
                //Get gameState of current cell
                int cellGameState = gameBoardStates[cellX, y];


                //If current cell filled, set gamestate of previous cell to player or ai piece. Check for win. Advance turn
                if ((cellGameState == 1 || cellGameState == 2) && y < 5)
                {                   
                    //Isnt updating gameBoardState in drawBoardScript? check this
                    gameBoardStates[cellX, (y + 1)] = newGameState;
                    drawBoard.UpdateBoard(cellX, (y + 1));

                    placedPieceX = cellX;
                    placedPieceY = y + 1;
                    
                    winState = winConditions.CheckForWin(gameBoardStates, newGameState, cellX, y+1);

                    //Only set winner if the move is an actual move, not an ai calculation
                    if (virtualMove == false)
                    {
                        gameObject.GetComponent<GameController>().winner = winState;
                    }

                    gameObject.GetComponent<GameController>().playerTurn = !playerTurn;

                    break;
                }


                //If column is full and new piece is placed, ignore it. 
                else if (y == 5 && (cellGameState == 1 || cellGameState == 2))
                {
                    Debug.Log("Column is full");
                    break;
                }


                //If bottom of column reached then set gamestate of current cell. Check for win. Advance turn
                else if (y == 0)
                {                  
                    gameBoardStates[cellX, y] = newGameState;
                    drawBoard.UpdateBoard(cellX, (y));

                    placedPieceX = cellX;
                    placedPieceY = y;

                    winState = winConditions.CheckForWin(gameBoardStates, newGameState, cellX, y);

                    //Only set winner if the move is an actual move, not an ai calculation
                    if (virtualMove == false)
                    {
                        gameObject.GetComponent<GameController>().winner = winState;
                    }

                    gameObject.GetComponent<GameController>().playerTurn = !playerTurn;

                    break;
                }
            } 

            //Log error if piece is placed outside game board
            catch (Exception e)
            {
                Debug.LogException(e);
                Debug.Log("Not legal move. Turn should not move on until legal move is made.");
            }
        }
        
    }
    

    

    public void UnmakeMove(int[,] gameBoardStates, BoardSpace space)
    {
        gameBoardStates[space.x, space.y] = 0;
    }

}
