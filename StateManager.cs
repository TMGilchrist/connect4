using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour
{
    public MiniMaxScript moveAlgorithm;
  

    public int GetAiMove()
    {
        BoardSpace aiMove;

        aiMove = moveAlgorithm.BestMove();
        Debug.Log("\n aiMove set \n");

        return aiMove.x;
    }



}
