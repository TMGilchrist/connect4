using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EvaluateBoardScript : MonoBehaviour
{
    private ThreatManager threatManager;

    public int winner = 0;


    void Start()
    {
        threatManager = gameObject.GetComponent<ThreatManager>();
    }


    //return score based on length of players connected lines
    public int Evaluate(int[,] board, int maximisingPlayer, int depth)
    {
        int opponent = 0;
        opponent = 3 - maximisingPlayer;

        //Populate a list of each player's threats
        List<Threat> playerLines = threatManager.FilterBoardThreats(board, maximisingPlayer);
        List<Threat> opponentLines = threatManager.FilterBoardThreats(board, opponent);


        //Count the number of different length threats
        int fours = CheckFours(playerLines);
        int opponentFours = CheckFours(opponentLines);


        int threes = GetThreeScore(playerLines);
        int opponentThrees = GetThreeScore(opponentLines);


        int score = (threes) - (opponentThrees) - (opponentFours);
        

        //If player has one or more four in a row's, return a winning score
        if (fours > 0)
        {
            winner = maximisingPlayer;
            return (100000 + depth) * fours;
        }


        //If opponent has one or more four in a row's, return a losing score
        if (opponentFours > 0)
        {
            winner = opponent;
            return (-100000 - depth) * opponentFours;
        }
        
        //Otherwise a score should be returned based on the available threats
        else
        {
            winner = 0;
            return score;
        }
               

    }

    
    //Count number of four in a rows
    public int CheckFours(List<Threat> threats)
    {
        int fours = 0;

        foreach (Threat threat in threats)
        {
            if (threat.length >= 4)
            {
                fours++;
                //lines.Remove(line);
            }
        }

        return fours;
    }


    //Count number of three in a rows
    private int CheckThrees(List<Threat> threats)
    {
        int threes = 0;

        foreach (Threat threat in threats)
        {
            if (threat.length == 3)
            {
                threes++;
                //lines.Remove(line);
            }
        }

        return threes;
    }


    //Count number of open three in a rows
    private int GetThreeScore(List<Threat> threats)
    {
        int threes = 0;

        foreach (Threat threat in threats)
        {
            if (threat.length == 3)
            {
                threes = threes + (1000 * threat.open);
            }
        }

        return threes;
    }
        

    //Count number of two in a rows
    private int CheckTwos(List<Threat> threats)
    {
        int twos = 0;

        foreach (Threat threat in threats)
        {
            if (threat.length == 2)
            {
                twos++;
                //lines.Remove(line);
            }
        }

        return twos;
    }





}
