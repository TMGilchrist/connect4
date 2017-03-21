using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    //Dimensions of game board
    public int columns = 7;
    public int rows = 6;

    //Number of pieces in a row needed to win
    public int winSize = 4;
    public bool againstAI = true;

    //Win state
    public int winner = 0;


    public CameraController cameraControls;

    public bool playerTurn = true;

    public Transform newPiece;
    private PlayerController player;



	// Use this for initialization
	void Start ()
    {
        player = gameObject.GetComponent<PlayerController>();
 
        //Draw board
        DrawBoardScript drawBoard = gameObject.GetComponent<DrawBoardScript>();
        drawBoard.DrawBoard(columns, rows);

        //Set camera position to center of board
        float halfCellSize = drawBoard.cellSize / 2;

        float cameraX = (columns * halfCellSize) - halfCellSize;
        float cameraY = (rows * halfCellSize) - halfCellSize;

        cameraControls.PositionCamera(cameraX, cameraY);
    }

	

}
