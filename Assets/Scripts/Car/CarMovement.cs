using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CarMovement : MonoBehaviour
{
   public WheelCollider[] wheelColliders = new WheelCollider[4];
   public Transform[] tyreMeshes = new Transform[4];

   public float maxTorque;

   public float maxBrakeForce;

   private bool goForward;
   private bool goBackward;

   private bool goRight;

   private bool goLeft;

   private float steer;
   private float acceleration;
   private bool isBraking;

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

   void Update()
   {
	// UpdateMeshesPositions();	
   }

   void FixedUpdate()
   {
		updateWheelColliders();
		UpdateMeshesPositions(); 
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


   }

   	void updateWheelColliders()
	{
		float fixedAngle = steer * 45f;
		wheelColliders [0].steerAngle = fixedAngle;
		wheelColliders [1].steerAngle = fixedAngle;		
		
		for (int i = 0; i < 4; i++) 
		{
			Debug.Log("Car Acceleration: ");
			Debug.Log(acceleration);
			wheelColliders[i].motorTorque = acceleration * maxTorque;
		}

	}

   	void UpdateMeshesPositions()
	{
		for(int i = 0; i < 4 ; i++)
		{
			Quaternion quat;
			Vector3 pos;
			wheelColliders[i].GetWorldPose(out pos, out quat);


			tyreMeshes[i].position = pos;
			// Note: not setting tyremeshes rotation value provided by
			// GetWorldPose because doing so leads the wheelMeshes being
			// turned 90 degrees and wheelMeshes not being able to turn
			// along with wheelCollider 
			// tyreMeshes[i].rotation = quat;
		}

		//To ensure that front left tyreMesh rotates along with wheelCollider
		tyreMeshes[0].localEulerAngles = new Vector3(tyreMeshes[0].localEulerAngles.x, wheelColliders[0].steerAngle - tyreMeshes[0].localEulerAngles.z, tyreMeshes[0].localEulerAngles.z);
		
		//To ensure that front right tyreMesh rotates along with wheelCollider
		tyreMeshes[1].localEulerAngles = new Vector3(tyreMeshes[1].localEulerAngles.x, wheelColliders[1].steerAngle - tyreMeshes[1].localEulerAngles.z, tyreMeshes[1].localEulerAngles.z);
	
	}
	public void OnRight()
	{
		if(steer<1)
		{
			steer+=0.05f;		
		}
		
	}

	public void OnLeft()
	{
		if(steer>-1)
		{
			steer-=0.05f;
		}
	}

	public void OnForward()
	{
		
		//If truck was moving backward, set accelaration 0 to move forward
		if(acceleration<0)
		{
			acceleration=0f;
			applyBraking();
		}
		else if(isBraking)
		{			
			resetBraking();
		}
			
		if(acceleration<1)
		{
			acceleration += 0.089f;
		}
	}

	public void OnBackward()
	{
		//If truck was moving forward, set accelaration to 0 to move backward
		if(acceleration>0)
		{
			acceleration=0f;
			applyBraking();
		}
		else if(isBraking)
		{
			resetBraking();
		}

		if(acceleration>-1)
		{
			acceleration-=0.089f;
		}
	} 

	public void applyBraking()
	{
		isBraking = true;
		foreach (var wheelCollider in wheelColliders)
		{
			wheelCollider.brakeTorque = maxBrakeForce;
		}
	}

	private void resetBraking()
	{
		isBraking = false;
		foreach (var wheelCollider in wheelColliders)
		{
			wheelCollider.brakeTorque = 0f;
		}
	}


   void OnForwardEvent(OnForwardPressEvent onForwardPressEvent)
	{
		Debug.Log("On Forward Car Event");
		goForward = true;
	}

	void OnBackwardEvent(OnBackwardPressEvent onBackwardPressEvent)
	{	
			
		goBackward = true;	
		
	}

	void OnForwardREvent(OnForwardReleaseEvent onForwardReleaseEvent)
	{
		goForward = false;
	}

	void OnBackwardREvent(OnBackwardReleaseEvent onBackwardReleaseEvent)
	{
		goBackward = false;
	}

	void OnRightEvent(OnRightPressEvent onRightPressEvent)
	{
		goRight = true;
	}

	void OnRightREvent(OnRightReleaseEvent onRightReleaseEvent)
	{
		goRight = false;
		OnSteerReset();
	}

	void OnLeftEvent(OnLeftPressEvent onLeftPressEvent)
	{
		goLeft = true;
	}

	void OnLeftREvent(OnLeftReleaseEvent onLeftReleaseEvent)
	{
		goLeft = false;
		OnSteerReset();
	}

   public void OnSteerReset()
	{
		steer = 0f;
		UpdateMeshesPositions();
	}

   public void ExitScene()
   {
		SceneManager.LoadScene("Main_Scene");
   }


}
