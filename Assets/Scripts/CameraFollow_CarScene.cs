
using UnityEngine;

public class CameraFollow_CarScene : MonoBehaviour
{
  //The target that we want the camera to follow
  public Transform player;
  
  //Need this variable to define how quickly the camera will snap to target, being player
  //the smaller the smooth speed is, the longer the time will be spent to smoothing
  //Value varies between 0 and 1
  public float smoothSpeed = 0.125f;
  
  public Vector3 offset;
  
  //Some times the target's position is changed in the Update function
  //So if we use LateUpdate, then the camera will be updated after the 
  //target's position is updated, this way the Camera will not be 
  //competing with target to update its position
  void LateUpdate()
  {
     Vector3 desiredPosition = player.position + offset;
     
     //Ensures camera moves between and inclusive to transform.position and desiredPosition
     //depending on smoothSpeed Value. If smoothSpeed value is 0, then camera will move to transform.position
     //if smoothSpeed value is 1, then camera will move to desiredPosition
     //if smoothSpeed value is between 0 and 1, then camera will move between transform.position
     //and desiredPosition 
     Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
     transform.position = smoothedPosition;
  }
}
