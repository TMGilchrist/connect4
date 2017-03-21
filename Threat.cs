using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Threat
{
    public int length;
    public int player;

    public int open; //1 or 2 open spaces to continue threat or 0 if no open space.

    public int orientation; //0: vertical 1: Horizontal 2: Diagonal left 3: Diagonal Right


    public Threat(int length, int player, int open, int orientation)
    {
        this.length = length;
        this.player = player;
        this.open = open;
        this.orientation = orientation;
    }
    



}
