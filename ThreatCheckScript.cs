using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ThreatCheckScript : MonoBehaviour
{
    private ThreatManager threatManager;

    private void Start()
    {
        threatManager = gameObject.GetComponent<ThreatManager>();
    }




    /// <summary>
    /// Method to check for a win. 
    /// Calls CheckForLength to find a line of 4 or more.
    /// </summary>
    /// <param Array of cells on board ="gameBoard"></param>
    /// <param Gamestate to compare for win. 1 = player. 2 = ai. ="gameState"></param>
    /// <param x index of last piece placed on gameboard ="xPos"></param>
    /// <param y index of last piece placed on gameboard ="yPos"></param>
    /// <returns>Integer. 0 = no win. 1 = player 1 win. 2 = player 2 win. True if CheckForLength returns a length of 4</returns>
    public int CheckForWin(int[,] board, int player, int xPos, int yPos)
    {
        int win = 0;

        List<Threat> threats = threatManager.FilterBoardThreats(board, player);

        //List<Threat> threats = threatManager.CollectThreats(board, player, xPos, yPos);

        //Debug.Log("Checking for threats");
        for (int i = 0; i < threats.Count; i++)
        {
            Threat currentThreat = threats[i];

            Debug.Log("Threat of length " + currentThreat.length + " and orientation " + currentThreat.orientation + " with " + currentThreat.open + " open space(s), for player " + currentThreat.player);

            if (currentThreat.length >= 4)
            {
                win = player;
                Debug.Log("Winning Threat");
                Debug.Log("Winner: " + win);
                //Debug.Log("Threat of length " + threats[i].length + " and orientation " + threats[i].orientation + " for player " + threats[i].player);
                break;
            }
        }

        //Find the longest length threat to see if the player has got 4 in a row.
        /*if(threats.Max(x => x.length) >= 4)
        {
            win = player;
        }*/

        return win;
    }




    //Needs to add check for open threat. Y should prehaps iterate up column not down?

    //Method to check for vertical win. Check is performed downwards from last placed piece
    public Threat CheckVertical(int[,] gameBoard, int player, int xPos, int yPos)
    {
        int connectScore = 0;

        Threat newThreat = new Threat(connectScore, player, 1, 0);

        //Iterate down y axis of current column to check gamestates of cells
        //----------------------------------------------------------------------------------------------------------

        for (int y = yPos; y >= 0; y--)
        {
            //Compare state of cell with the desired state. If they match, increment score
            if (gameBoard[xPos, y] == player)
            {
                connectScore++;    
            }

            //If they do not match, break
            else if (gameBoard[xPos, y] != player)
            {
                break;
            }
        
        }
        //----------------------------------------------------------------------------------------------------------

        newThreat.length = connectScore;

        return newThreat;
    }



    //Method to check for horizontal win. Check is performed to right left of last placed piece. 
    //Need to implement a check for gap in line that doesnt flag as win.
    public Threat CheckHorizontal(int[,] gameBoard, int player, int xPos, int yPos)
    {
        int connectScore = 0;

        Threat newThreat = new Threat(connectScore, player, 0, 1);

        //Starting at (xPos, yPos), check to right
        //----------------------------------------------------------------------------------------------------------

        for (int x = xPos; x < gameBoard.GetLength(0); x++)
        {
            //Compare state of cell with the desired state. If they match, increment score
            if (gameBoard[x, yPos] == player)
            {
                connectScore++;
            }

            //If an opponent's piece is found, break.
            else if (gameBoard[x, yPos] == (3 - player))
            {
                break;
            }

            //If an empty space is found, set threat status to open and break. 
            //Commented code below would check for a break in a line of pieces. However this leads to false win detections. 
            else if (gameBoard[x, yPos] == 0)
            {
                /*//If not the edge of the board
                if (x+1 < gameBoard.GetLength(0))
                {
                    //If there is an empty space, check the next space too (used to find threats with gaps in them)
                    if (gameBoard[x + 1, yPos] != player)
                    {
                        newThreat.open++;
                        break;
                    }
                }

                else
                {
                    newThreat.open++;
                    break;
                }*/

                newThreat.open++;
                break;
            }

        }
        //----------------------------------------------------------------------------------------------------------


        //Starting at position to left of (xPos, yPos) (so that the piece does not get counted twice), check to left
        //----------------------------------------------------------------------------------------------------------

        for (int x = xPos-1; x >= 0; x--)
        {
            //Compare state of cell with the desired state. If they match, increment score
            if (gameBoard[x, yPos] == player)
            {
                connectScore++;
            }

            //If an opponent's piece is found, break.
            else if (gameBoard[x, yPos] == (3 - player))
            {
                break;
            }

            //If an empty space is found, set threat status to open and break.
            else if (gameBoard[x, yPos] == 0)
            {
                newThreat.open++;
                break;
            }

        }
        //----------------------------------------------------------------------------------------------------------

        newThreat.length = connectScore;

        return newThreat;
    }



    //Method to check for left diagonal win. Check is performed to bottom right and top left of last placed piece
    public Threat CheckDiagonalLeft(int[,] gameBoard, int player, int xPos, int yPos)
    {
        int connectScore = 0;
        int y = yPos;

        Threat newThreat = new Threat(connectScore, player, 0, 2);

        //Starting at position of last piece placed, check to bottom right
        //----------------------------------------------------------------------------------------------------------

        for (int x = xPos; x < gameBoard.GetLength(0); x++)
        {            

            //Compare state of cell with the desired state. If they match, increment score
            if (gameBoard[x, y] == player)
            {
                connectScore++;
            }

            //If an opponent's piece is found, break.
            else if (gameBoard[x, y] == (3 - player))
            {
                break;
            }

            //If an empty space is found, set threat status to open and break.
            else if (gameBoard[x, y] == 0)
            {
                newThreat.open++;
                break;
            }


            //Move y position down one place for next check
            if (y > 0)
            {
                y--;
            }

            //If bottom of board, break.
            else
            {
                break;
            }
        }
        //----------------------------------------------------------------------------------------------------------




        //Starting at position to top left of last piece placed, check to top left
        //----------------------------------------------------------------------------------------------------------

        //Reset y position and plus 1 so that the piece does not get counted twice

        y = yPos + 1;

        if (y <= 5)
        {
            for (int x = xPos - 1; x >= 0; x--)
            {
                //Compare state of cell with the desired state. If they match, increment score
                if (gameBoard[x, y] == player)
                {
                    connectScore++;
                }

                //If an opponent's piece is found, break.
                else if (gameBoard[x, y] == (3 - player))
                {
                    break;
                }

                //If an empty space is found, set threat status to open and break.
                else if (gameBoard[x, y] == 0)
                {
                    newThreat.open++;
                    break;
                }

                //Move y position up one place for next check
                if (y < 5)
                {
                    y++;
                }

                //If top of board, break.
                else
                {
                    break;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------------

        newThreat.length = connectScore;

        return newThreat;
    }



    //Method to check for right diagonal win. Check is performed to bottom left and top right of last placed piece
    public Threat CheckDiagonalRight(int[,] gameBoard, int player, int xPos, int yPos)
    {
        int connectScore = 0;
        int y = yPos;

        Threat newThreat = new Threat(connectScore, player, 0, 3);

        //Starting at position of last piece placed, check to top right
        //----------------------------------------------------------------------------------------------------------

        for (int x = xPos; x < gameBoard.GetLength(0); x++)
        {

            //Compare state of cell with the desired state. If they match, increment score
            if (gameBoard[x, y] == player)
            {
                connectScore++;
            }

            //If an opponent's piece is found, break.
            else if (gameBoard[x, y] == (3 - player))
            {
                break;
            }

            //If an empty space is found, set threat status to open and break.
            else if (gameBoard[x, y] == 0)
            {
                newThreat.open++;
                break;
            }

            //Move y position up one place for next check
            if (y < 5)
            {                
                y++;
            }

            //If top of board, break.
            else
            {
                break;
            }
        }
        //----------------------------------------------------------------------------------------------------------




        //Starting at position to bottom left of last piece placed, check to bottom left
        //----------------------------------------------------------------------------------------------------------

        //Reset y position and minus 1 so that the piece does not get counted twice
        y = yPos - 1;

        if (y >= 0)
        {
            for (int x = xPos - 1; x >= 0; x--)
            {
                //Compare state of cell with the desired state. If they match, increment score
                if (gameBoard[x, y] == player)
                {
                    connectScore++;
                }

                //If an opponent's piece is found, break.
                else if (gameBoard[x, y] == (3 - player))
                {
                    break;
                }

                //If an empty space is found, set threat status to open and break.
                else if (gameBoard[x, y] == 0)
                {
                    newThreat.open++;
                    break;
                }

                //Move y position down one place for next check
                if (y > 0)
                {
                    y--;
                }

                //If bottom of board, break.
                else
                {
                    break;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------------

        newThreat.length = connectScore;

        return newThreat;
    }


}
