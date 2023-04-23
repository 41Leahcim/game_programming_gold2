using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{
    public void AdjustCameraHeight(float objectHeight){
        // Move the camera up, if the current object dropped below the height of the camera
        if(transform.position.y < objectHeight){
            float heightDifference = objectHeight - transform.position.y;
            transform.Translate(heightDifference * Vector3.up);
        }
    }
}
