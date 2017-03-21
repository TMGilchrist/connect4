using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    
    GameObject[] finishObjects;

    public GameController gameData;
    private GameObject gameComponents;
    private List<MonoBehaviour> gameScripts;

    // Use this for initialization
    void Start ()
    {
        gameComponents = GameObject.FindWithTag("GameController");
        List<MonoBehaviour> gameScripts = new List<MonoBehaviour>();
        Time.timeScale = 1;
        
        //Populate list with all scripts attached to the GameController component
        foreach (MonoBehaviour gameScript in gameComponents.GetComponents<MonoBehaviour>())
        {
            gameScripts.Add(gameScript);
        }

        //Populate arrays of UI objects and hide them
        finishObjects = GameObject.FindGameObjectsWithTag("ShowOnFinish");
        ShowFinished(false);

    }
	

    //Restart button not working. Timescale not reset to 1


	// Update is called once per frame
	void Update ()
    {    
        if (gameData.winner != 0)
        {
            EndGame();
            gameData.winner = 0;

        }
	}

    //Enables or disables MonoBehaviour scripts attached to the GameController object
    public void EnableGameScripts(bool enabled)
    {
        foreach (MonoBehaviour script in gameScripts)
        {
            script.enabled = enabled;
        }
    }


    //Stop the game and display the winning player
    public void EndGame()
    {      
        Debug.Log("endgame script run");

        if (gameData.winner == 1)
        {
            Debug.Log("\n Winner set \n");
            ShowFinished(true);
            SetWinText("Player 1 has won the game");
        }


        else if (gameData.winner == 2)
        {
            Debug.Log("\n Winner set \n");
            ShowFinished(true);
            SetWinText("Player 2 has won the game");
        }

        //EnableGameScripts(false);
    }


    //Controls pausing or unpausing the game and showing the game over menu
    public void PauseControl()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            ShowFinished(true);
        }

        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            ShowFinished(false);
        }

        Debug.Log(Time.timeScale);
    }
    

    //Set the text on the finish screen to display the winner
    public void SetWinText(string text)
    {
        Text win = GameObject.Find("WinText").GetComponent<Text>();

        win.text = text;
    }
    

    //Show or hide objects with ShowOnFinish tag
    public void ShowFinished(bool showFinishMenu)
    {
        foreach (GameObject g in finishObjects)
        {
            g.SetActive(showFinishMenu);
        }
    }


    //Load level from function parameter
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }


    //Exit application
    public void ExitGame()
    {
        Application.Quit();
    }



}
