using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ThirdPersonMovement : MonoBehaviour
{
    //Note: Think of the CharacterController as the motor that
    //drives the player. In this script we need to tell it
    //how to make the motor move.
    public CharacterController controller;

    public float speed = 6f;

	private bool goForward;
	private bool goBackward;
	private bool goRight;
	private bool goLeft;

    private float horizontal;
    private float vertical;

	//For rotating player smoothing
	public float turnSmoothTime = 0.1f;
	float turnSmoothVelocity;
    void Start()
    {
        CallbackEventSystem.Current.RegisterListener<OnForwardPressEvent>(OnForwardEvent);
		CallbackEventSystem.Current.RegisterListener<OnBackwardPressEvent>(OnBackwardEvent);
		CallbackEventSystem.Current.RegisterListener<OnBackwardReleaseEvent>(OnBackwardREvent);
		CallbackEventSystem.Current.RegisterListener<OnForwardReleaseEvent>(OnForwardREvent);
		CallbackEventSystem.Current.RegisterListener<OnRightPressEvent>(OnRightEvent);
		CallbackEventSystem.Current.RegisterListener<OnRightReleaseEvent>(OnRightREvent);
		CallbackEventSystem.Current.RegisterListener<OnLeftPressEvent>(OnLeftEvent);
		CallbackEventSystem.Current.RegisterListener<OnLeftReleaseEvent>(OnLeftREvent);
    }

    void FixedUpdate()
    {

        if(goForward)
		{
			OnForward();
		}

		if(goBackward)
		{
			OnBackward();
		}

		if(goRight)
		{
			OnRight();
		}

		if(goLeft)
		{
			OnLeft();
		}

		//Since we want the player to move in the x-axis and z-axis 
		//We normalize the direction vector so that the player does not move
		//faster when holding down two keys.
		Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

		//To check if the player is moving in any direction
		//We are checking if the length of the direction vector
		//is not zero
		if(direction.magnitude >= 0.1f)
		{
			//To rotate the player in the direction it is pointed towards
			//Note: check out this youtube tutorial for more info: 
			//https://www.youtube.com/watch?v=4HpC--2iowE&t=745s
			float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			transform.rotation = Quaternion.Euler(0f, angle, 0f);
			


			//Multiplying by Time.deltaTime to make the frame rate
			//independent
			controller.Move(direction*speed*Time.deltaTime);

		}
	}

    public void OnRight()
	{
		if(horizontal<1)
		{
			horizontal+=0.05f;		
		}
		
	}

	public void OnLeft()
	{
		if(horizontal>-1)
		{
			horizontal-=0.05f;
		}
	}

	public void OnHorizontalReset()
	{
		horizontal = 0f;
	}

    public void OnVerticalReset()
    {
        vertical = 0f;
    }

    public void OnForward()
	{
		
		//If truck was moving backward, set accelaration 0 to move forward
		if(vertical<0)
		{
			vertical=0f;
        }
			
		if(vertical<1)
		{
			vertical += 0.089f;
		}
	}

	public void OnBackward()
	{
		//If truck was moving forward, set accelaration to 0 to move backward
		if(vertical>0)
		{
			vertical=0f;
		}
		if(vertical>-1)
		{
			vertical-=0.089f;
		}
	} 

    void OnForwardEvent(OnForwardPressEvent onForwardPressEvent)
	{
        Debug.Log("Forward Event");
		goForward = true;
	}

	void OnBackwardEvent(OnBackwardPressEvent onBackwardPressEvent)
	{	
		Debug.Log("Backward Event");	
		goBackward = true;	
		
	}

	void OnForwardREvent(OnForwardReleaseEvent onForwardReleaseEvent)
	{
		goForward = false;
        OnVerticalReset();
	}

	void OnBackwardREvent(OnBackwardReleaseEvent onBackwardReleaseEvent)
	{
		goBackward = false;
        OnVerticalReset();
	}

	void OnRightEvent(OnRightPressEvent onRightPressEvent)
	{
        Debug.Log("Right Event");
		goRight = true;
	}

	void OnRightREvent(OnRightReleaseEvent onRightReleaseEvent)
	{
		goRight = false;
		OnHorizontalReset();
	}

	void OnLeftEvent(OnLeftPressEvent onLeftPressEvent)
	{
        Debug.Log("Left Event");
		goLeft = true;
	}

	void OnLeftREvent(OnLeftReleaseEvent onLeftReleaseEvent)
	{
		goLeft = false;
		OnHorizontalReset();
	}


   public void ExitScene()
   {
		SceneManager.LoadScene("Main_Scene");
   }

}
