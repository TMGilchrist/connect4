using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MiniMaxScript : MonoBehaviour
{
    public GameController gameData;

    private ThreatCheckScript winConditions;
    private DrawBoardScript board;
    private UtilityScripts utility;
    private GamePieceScript placePiece;
    private EvaluateBoardScript eval;

    private int[,] tempBoard;

    public int currentPlayer;

    public BoardSpace bestSpace;

    // Use this for initialization
    void Start()
    {
        winConditions = gameObject.GetComponent<ThreatCheckScript>();
        board = gameObject.GetComponent<DrawBoardScript>();
        utility = gameObject.GetComponent<UtilityScripts>();
        placePiece = gameObject.GetComponent<GamePieceScript>();
        eval = gameObject.GetComponent<EvaluateBoardScript>();
    }
    

    //changed return type from move to BoardSpace
    //Method to find the best move to make. Calls MiniMax to find best score then searches list of moves to find move with that score.
    public BoardSpace BestMove()
    {
        //Move bestMove;
        BoardSpace spaceToMove;

        //Set current player
        if (gameData.playerTurn)
        {
            //player
            currentPlayer = 1;
        }

        else
        {
            //ai
            currentPlayer = 2;
        }

        //As unmaking moves, the original board state can be used. Tempboard should be replaced in the negaMax function with this.
        tempBoard = board.gameBoardState;

        spaceToMove = NegaMaxMove(6, currentPlayer);

        return spaceToMove;
    }



    //Negamax returning a boardspace to move to. Calls abNegamax. currently used.
    public BoardSpace NegaMaxMove(int depth, int player)
    {
        //Initalise maxMove score as a very small number
        int maxScore = int.MinValue + 1;
        BoardSpace bestMove = null;

        //Iterate through each available space
        foreach (BoardSpace space in placePiece.GetAvailableSpaces(tempBoard))
        {
            //Make move in each space
            placePiece.MakeMove(tempBoard, space, player);

            //temporary value v represents score for each move. 3 - player switches to other player's perspective 
            int v = -abNegaMax(depth - 1, 3 - player, int.MinValue + 1, int.MaxValue - 1);

            Debug.Log("Score for player " + player + " move: " + space.x + ", " + space.y + " is: " + v);

            placePiece.UnmakeMove(tempBoard, space);

            //Set maxScore to largest value
            if (v > maxScore)
            {
                maxScore = v;
                bestMove = space;
            }

        }

        Debug.Log("Returning Best Move: " + bestMove.x + ", " + bestMove.y + " with a score of: " + maxScore);
        return bestMove;
    }


    //Negamax with alpha-beta pruning.
    public int abNegaMax(int depth, int player, int alpha, int beta)
    {
        int evalResult = eval.Evaluate(tempBoard, player, depth);

        //If winning move made, evaluate the score of the current board position
        //evalResult != 0
        if (eval.winner != 0 || depth == 0)
        {
           


            //Debug.Log("Player " + eval.winner + " wins");
            eval.winner = 0;
            return evalResult;
        }

        //Initalise maxMove score as a very small number
        int maxScore = int.MinValue + 1;

        //Iterate through each available space
        foreach (BoardSpace space in placePiece.GetAvailableSpaces(tempBoard))
        {
            //Make move in each space
            placePiece.MakeMove(tempBoard, space, player);
            //Debug.Log("\nPlace piece at space: " + space.x + ", " + space.y + " For player: " + player);

            //temporary value represents score for each move (3-player)
            int v = -abNegaMax(depth - 1, 3 - player, -beta, -alpha);

            //Debug.Log("Score for move: " + space.x + ", " + space.y + " is: " + v);

            placePiece.UnmakeMove(tempBoard, space);

            if (v > maxScore)
            {
                maxScore = v;
            }

            if (v > alpha)
            {
                alpha = v;
                bestSpace = space;
            }

            if (alpha >= beta)
            {
                break;
                //return beta;
            }

        }

        return maxScore;
    }




        



    //Non alpha-beta negamax implementation. Replaced by abNegaMax.
    /*

    //Negamax returning a boardspace to move to. Calls Negamax3. Unused
    public BoardSpace NegaMaxMove2(int depth, int player)
    {
        //Initalise maxMove score as a very small number
        int maxScore = int.MinValue + 1;
        BoardSpace bestMove = null;

        //Iterate through each available space
        foreach (BoardSpace space in placePiece.GetAvailableSpaces(tempBoard))
        {
            //Make move in each space
            placePiece.MakeMove(tempBoard, space, player);

            //temporary value v represents score for each move. 3 - player switches to other player's perspective 
            int v = -NegaMax3(depth - 1, 3 - player);

            Debug.Log("Score for player " + player + " move: " + space.x + ", " + space.y + " is: " + v);

            placePiece.UnmakeMove(tempBoard, space);

            //Set maxScore to largest value
            if (v > maxScore)
            {
                maxScore = v;
                bestMove = space;
            }


        }

        Debug.Log("Returning Best Move: " + bestMove.x + ", " + bestMove.y + " with a score of: " + maxScore);
        return bestMove;
    }
    
    //Original NegaMax using unmakeMove instead of board clones.
    public int NegaMax3(int depth, int player)
    {
        //Debug.Log("\nDepth: " + depth);
        int evalResult = eval.Evaluate(tempBoard, player, depth);

        //If winning move made, evaluate the score of the current board position
        if (evalResult != 0 || depth == 0)
        {
            //Debug.Log("\nDepth: " + depth);
            //Debug.Log("\nPlayer: " + player);
            //Debug.Log("\nevaluate score" + evalResult);

            //Debug.Log("\nEvaluate winner: " + eval.winner);
            eval.winner = 0;
            return evalResult;
        }

        //Initalise maxMove score as a very small number
        int maxScore = int.MinValue + 1;

        //Iterate through each available space
        foreach (BoardSpace space in placePiece.GetAvailableSpaces(tempBoard))
        {
            //Make move in each space
            placePiece.MakeMove(tempBoard, space, player);
            //Debug.Log("\nPlace piece at space: " + space.x + ", " + space.y + " For player: " + player);

            //temporary value represents score for each move (3-player) changed to -1*player
            int v = -NegaMax3(depth - 1, 3 - player);

            //Debug.Log("Score for player " + player + " move: " + space.x + ", " + space.y + " is: " + v);

            placePiece.UnmakeMove(tempBoard, space);

            //Set maxScore to largest value
            if (v > maxScore)
            {
                maxScore = v;
                bestSpace = space;
                //Debug.Log("\nBest Space: " + bestSpace.x + ", " + bestSpace.y + " Score: " + maxScore);
            }
        }

        return maxScore;
    }


    */
}
