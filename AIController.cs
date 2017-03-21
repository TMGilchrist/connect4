using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour
{
    public GameController gameData;
    public StateManager stateCalc;

    private DrawBoardScript board;

    private bool gettingMove = false;

	// Use this for initialization
	void Start ()
    {
        board = gameObject.GetComponent<DrawBoardScript>();
	}
	
    //Update aiTurn and if true place piece
    void Update()
    {

        if (gameData.playerTurn == false && gettingMove == false)
        {
            Debug.Log("\n Getting ai move \n");

            gettingMove = true;
            MakeMove();
        } 
    }


    //Place piece for ai
    void MakeMove()
    {     
        int cellX = stateCalc.GetAiMove();

        gameData.GetComponent<GamePieceScript>().PlaceGamePiece(board.gameBoardState, cellX, false, 2);
        gettingMove = false;
        gameData.playerTurn = true;
    }


}
