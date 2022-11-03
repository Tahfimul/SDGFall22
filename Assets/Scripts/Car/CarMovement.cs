using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	UpdateMeshesPositions();
   }

   void FixedUpdate()
   {
		updateWheelColliders();
		// UpdateMeshesPositions(); 
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
			// Debug.Log(quat);
			// tyreMeshes[i].rotation = quat;
		}
		Quaternion wheelCollider_0_quat = new Quaternion(0, wheelColliders[0].steerAngle, 0, 0);
		Quaternion wheelCollider_1_quat = new Quaternion(0, wheelColliders[1].steerAngle, 0, 0);  
		tyreMeshes[0].rotation = wheelCollider_0_quat;
		tyreMeshes[1].rotation = wheelCollider_1_quat;
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

   void OnForwardEvent(OnForwardPressEvent onForwardPressEvent)
	{
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
	}


}
