using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public float posSmoothing;
    public float rotSmoothing;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //positions camera to the amount of posSmoothing value between transform.position and player.position
        transform.position = Vector3.Lerp(transform.position, player.position, posSmoothing);
        //rotates camera to the amount of rotSmoothing value angle between transform.rotation and player.rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, rotSmoothing);
        //Set x, z angles to 0 and only leave y angle
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
    }
}
