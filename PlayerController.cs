using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private GamePieceScript placePiece;
    private GameObject[,] gameBoard;
    private int[,] gameBoardState;
    private UtilityScripts utility;
    private GameController gameData;
    

    private int player = 2;


    // Use this for initialization
    void Start ()
    {
        placePiece = gameObject.GetComponent<GamePieceScript>();
        gameBoard = gameObject.GetComponent<DrawBoardScript>().drawnGameBoard;
        utility = gameObject.GetComponent<UtilityScripts>();
        gameData = gameObject.GetComponent<GameController>();
        gameBoardState = gameObject.GetComponent<DrawBoardScript>().gameBoardState;
    }
	

	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetButtonDown("Fire1"))
        {
            //Get current player
            if (gameData.againstAI)
            {
                player = 1;
            }

            else
            {
                player = (3 - player);
            }

            //Get position of cursor on screen in relation to camera
            Vector2 cursorPos = Input.mousePosition;
            Vector2 screenPos = Camera.main.ScreenToWorldPoint(cursorPos);
            
            //Raycast downwards from cursor position to find the column to place the game piece in
            RaycastHit2D hit = Physics2D.Raycast(screenPos, Vector2.down, Mathf.Infinity);

            Debug.DrawRay(screenPos, Vector2.down);

            if (hit.collider != null)
            {
                //Get column to place piece in
                int column = utility.GetIndex(gameBoard, hit.collider.gameObject)[0];

                placePiece.PlaceGamePiece(gameBoardState, column, false, player);
            }
        }
	}




}
