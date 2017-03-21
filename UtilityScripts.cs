using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UtilityScripts : MonoBehaviour
{
    //Generic utility methods



    /// <summary>
    /// Method to find a child object.
    /// </summary>
    /// <param parent object to search="parentObject"></param>
    /// <param name of child object to find="objectName"></param>
    /// <returns>Child GameObject if found or null</returns>

    public GameObject getChildGameObject(GameObject parentObject, string objectName)
    {
        Transform[] transforms = parentObject.transform.GetComponentsInChildren<Transform>(true);

        foreach (Transform t in transforms) if (t.gameObject.name == objectName) return t.gameObject;

        return null;
    }



    /// <summary>
    /// Method to find the index of an item contained in a 2D array and return it in an array.
    /// Access index with var = GetIndex()[0]; where 0 = x index and 1 = y index.
    /// </summary>
    /// <param name of array containing item="arrayName"></param>
    /// <param item to find the index of="searchItem"></param>
    /// <returns>An array containing the x and y values of the item's index</returns>
    public int[] GetIndex(GameObject[,] arrayName, GameObject searchItem)
    {
        int[] index = new int[2];

        //Iterate through array until gameobject matches search item
        for (int x = 0; x < arrayName.GetLength(0); x++)
        {
            for (int y = 0; y < arrayName.GetLength(1); y++)
            {
                if (arrayName[x, y] == searchItem)
                {
                    index[0] = x;
                    index[1] = y;

                    break;
                }
            }
        }

        return index;
    }



    /// <summary>
    /// Method to deep copy a 2D array of GameObjects.
    /// </summary>
    /// <param the original array to be copied ="arrayToCopy"></param>
    /// <returns>Returns 2Darray arrayToPaste</returns>
    public GameObject[,] DeepCopy2DArray(GameObject[,] arrayToCopy)
    {
        int copyLength = arrayToCopy.GetLength(0);
        int copyHeight = arrayToCopy.GetLength(1);

        GameObject[,] arrayToPaste = new GameObject[copyLength, copyHeight];

        //Iterate through original array in both directions
        for (int x = 0; x < copyLength; x++)
        {
            for (int y = 0; y < copyHeight; y++)
            {
                //Instantiate new cell at each point in the array
                arrayToPaste[x, y] = GameObject.Instantiate(arrayToCopy[x, y]);

                //Update the gameStates of the cells to match the original array
                arrayToPaste[x, y].GetComponent<CellStateScript>().gameState = arrayToCopy[x, y].GetComponent<CellStateScript>().gameState;
            }
        }

        //Return 2D array
        return arrayToPaste;

    }



    /// <summary>
    /// Method to deep copy a 2D array of integers.
    /// </summary>
    /// <param the original array to be copied ="arrayToCopy"></param>
    /// <returns>Returns 2Darray arrayToPaste</returns>
    public int[,] DeepCopy2DArrayInt(int[,] arrayToCopy)
    {
        int copyLength = arrayToCopy.GetLength(0);
        int copyHeight = arrayToCopy.GetLength(1);

        int[,] arrayToPaste = new int[copyLength, copyHeight];

        //Iterate through original array in both directions
        for (int x = 0; x < copyLength; x++)
        {
            for (int y = 0; y < copyHeight; y++)
            {
                //copy each value in the array
                arrayToPaste[x, y] = arrayToCopy[x, y];
            }
        }

        //Return 2D array
        return arrayToPaste;
    }

}
