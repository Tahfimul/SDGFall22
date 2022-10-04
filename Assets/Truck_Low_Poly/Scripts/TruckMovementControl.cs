using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class TruckMovementControl : MonoBehaviour {
	public WheelCollider[] wheelColliders = new WheelCollider[4];
	public Transform[] tyreMeshes = new Transform[4];
	public float maxTorque = 50.0f;
	private Rigidbody m_rigidbody;
	public Transform centerOfMass;

	private float steer;
	private float acceleration;
	private bool isBraking;
	public float maxBrakeForce;

	private bool goForward;
	private bool goBackward;
	private bool goRight;
	private bool goLeft;

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
		Debug.Log("Start Called on MovementConytrol");
		m_rigidbody = GetComponent<Rigidbody>();
		// m_rigidbody.centerOfMass = centerOfMass.localPosition;
	}
	
	void Update()
	{
		UpdateMeshesPositions ();
	}
	
	void FixedUpdate()
	{
		
		Debug.Log("Fixed Update called from MovementControl");
		Debug.Log(m_rigidbody);

		// 0 is front left and 1 is front right
		
		float fixedAngel = steer * 45f;
		wheelColliders [0].steerAngle = fixedAngel;
		wheelColliders [1].steerAngle = fixedAngel;		
		
		for (int i = 0; i < 4; i++) 
		{
			wheelColliders[i].motorTorque = acceleration * maxTorque;
		}

		if(goForward)
		{
			OnForward();
		}

		if(goBackward)
		{
			Debug.Log("Going backward");
			OnBackward();
		}

		if(goRight)
		{
			Debug.Log("oing Right");
			OnRight();
		}

		if(goLeft)
		{
			Debug.Log("Going Left");
			OnLeft();
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
			tyreMeshes[i].rotation = quat;
		}
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

	public void OnSteerReset()
	{
		steer = 0f;
	}

	public void OnForward()
	{
		
		Debug.Log("Moving Forward");
		//If truck was moving backward, set accelaration 0 to move forward
		if(acceleration<0)
		{
			Debug.Log("Resetting accelration");
			acceleration=0f;
			applyBraking();
		}
		else if(isBraking)
		{
			isBraking = false;
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
			Debug.Log("Resetting Accelaration");
			acceleration=0f;
			applyBraking();
		}
		else if(isBraking)
		{
			isBraking = false;
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
		foreach (var wheelCollider in wheelColliders)
		{
			wheelCollider.brakeTorque = 0f;
		}
	}

	void OnForwardEvent(OnForwardPressEvent onForwardPressEvent)
	{

		Debug.Log("On Forward Press Event from TruckMovementControl");
	
		goForward = true;
		
	}

	void OnBackwardEvent(OnBackwardPressEvent onBackwardPressEvent)
	{	
			
		goBackward = true;	
		
	}

	void OnForwardREvent(OnForwardReleaseEvent onForwardReleaseEvent)
	{
		Debug.Log("On Forward Release Event");
		goForward = false;
	}

	void OnBackwardREvent(OnBackwardReleaseEvent onBackwardReleaseEvent)
	{
		Debug.Log("On Backward Release Event");
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
}
