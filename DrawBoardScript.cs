using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawBoardScript : MonoBehaviour
{
    //2D array of cells that make up the gameboard.
    public GameObject[,] drawnGameBoard;
    public int[,] gameBoardState;

    //prefab to be used to draw board
    public Transform cellPrefab;

    public float cellSize;


    //Iterates for x and y dimensions of board and draws cells
    public void DrawBoard(int columnNum, int rowNum)
    {
        //gameBoard = new int[columnNum, rowNum];
        drawnGameBoard = new GameObject[columnNum, rowNum];
        gameBoardState = new int[columnNum, rowNum];

        //float cellSize;
        cellSize = cellPrefab.GetComponent<Renderer>().bounds.size.x;

        //Iterate for x value
        for (int x = 0; x < columnNum; x++)
        {
            //Iterate for y value
            for (int y = 0; y < rowNum; y++)
            {
                //Instantiate board cell
                Transform newCell = Instantiate(cellPrefab, new Vector3(x * cellSize, y * cellSize, 0), Quaternion.identity) as Transform;
                newCell.parent = GameObject.Find("GameBoard").transform;

                //Populate array of cells
                drawnGameBoard[x, y] = newCell.gameObject;

                gameBoardState[x, y] = 0;
            }
        }
    }


    public void UpdateBoard(int x, int y)
    {
        drawnGameBoard[x, y].GetComponent<CellStateScript>().gameState = gameBoardState[x, y];
    }


}
