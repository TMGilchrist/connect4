using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatManager : MonoBehaviour
{
    private ThreatCheckScript getThreat;


    private void Start()
    {
        getThreat = gameObject.GetComponent<ThreatCheckScript>();
    }


    //Collect all the threats for a given player from a single position into a list.
    public List<Threat> CollectThreats(int[,] board, int player, int xPos, int yPos)
    {
        List<Threat> threats = new List<Threat>();
        
        threats.Add(getThreat.CheckVertical(board, player, xPos, yPos));
        threats.Add(getThreat.CheckHorizontal(board, player, xPos, yPos));
        threats.Add(getThreat.CheckDiagonalLeft(board, player, xPos, yPos));
        threats.Add(getThreat.CheckDiagonalRight(board, player, xPos, yPos));

        return threats;
    }



    //Check all threats on board for a given player and collect into a list.
    public List<Threat> CheckBoardThreats(int[,] board, int player)
    {
        List<Threat> threats = new List<Threat>();

        //Check verticals for each column
        for (int x = 0; x < board.GetLength(0); x++)
        {
            for (int y = 0; y < board.GetLength(1); y++)
            {
                threats.Add(getThreat.CheckVertical(board, player, x, y));
            }
        }

        //Check center column for diagonals and horizontals
        for (int y = 0; y < board.GetLength(1); y++)
        {
            threats.Add(getThreat.CheckHorizontal(board, player, (board.GetLength(0) / 2), y));
            threats.Add(getThreat.CheckDiagonalLeft(board, player, (board.GetLength(0) / 2), y));
            threats.Add(getThreat.CheckDiagonalRight(board, player, (board.GetLength(0) / 2), y));
        }

        return threats;
    }

    
    
    //Currently using a second list to store the index of items to remove as removing from a single list will reorder the index.
    //Remove any 3 or shorter threats that are not open
    public List<Threat> FilterBoardThreats(int[,] board, int player)
    {
        List<Threat> threats = new List<Threat>();
        threats = CheckBoardThreats(board, player);

        List<Threat> workList = new List<Threat>();

        for (int i = 0; i < threats.Count; i++)
        {
            Threat currentThreat = threats[i];

            //Remove threats that have no open spaces to finish
            if ((currentThreat.open == 0 & currentThreat.length < 4) || (currentThreat.length <= 1))
            {
                //Debug.Log("Removing threat of length: " + currentThreat.length);
                workList.Add(currentThreat);
            }

            else
            {
                //Debug.Log("Criteria met. Index: " + i);
            }

        }


        //Debug.Log("Before filter. Number of threats: " + threats.Count);

        for (int i = 0; i < workList.Count; i++)
        {
           threats.Remove(workList[i]);
        }

        //Debug.Log("After filter. Number of threats: " + threats.Count);
        return threats;
    }


}
