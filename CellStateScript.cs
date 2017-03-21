using UnityEngine;
using System.Collections;

public class CellStateScript : MonoBehaviour
{
    /// <summary>
    /// GameState of a cell
    /// 0 = empty
    /// 1 = player (yellow)
    /// 2 = AI (red)
    /// </summary>
    public int gameState = 0;

    //Materials to be used
    public Material backgroundMat;
    public Material yellowPieceMat;
    public Material redPieceMat;

    private MeshRenderer backgroundMesh;
    //private UtilityScripts utility;
    //private GameObject background;

    void Start()
    {
        //Get MeshRenderer of each cell background
        backgroundMesh = gameObject.GetComponent<UtilityScripts>().getChildGameObject(gameObject, "background").GetComponent<MeshRenderer>();

    }


    //Set background material of a cell based on its gameState
    void Update()
    {
        if (gameState == 0)
        {
            backgroundMesh.material = backgroundMat;
        }

        else if (gameState == 1)
        {
            backgroundMesh.material = yellowPieceMat;
        }

        else if (gameState == 2)
        {
            backgroundMesh.material = redPieceMat;
        }
    }
}
