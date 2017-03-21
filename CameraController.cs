using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    //Set camera position
    public void PositionCamera(float xPos, float yPos)
    {
        gameObject.transform.position = new Vector3(xPos, yPos, -10);
    }


}
