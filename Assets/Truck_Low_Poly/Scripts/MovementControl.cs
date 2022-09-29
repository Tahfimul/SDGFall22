using UnityEngine;
using System.Collections;

public class MovementControl : MonoBehaviour {
	public WheelCollider[] wheelColliders = new WheelCollider[4];
	public Transform[] tyreMeshes = new Transform[4];
	public float maxTorque = 50.0f;
	private Rigidbody m_rigidbody;
	public Transform centerOfMass;

	private float steer;
	private float acceleration;
	private bool isBraking;
	public float maxBrakeForce;

	void start()
	{
		m_rigidbody = GetComponent<Rigidbody>();
		m_rigidbody.centerOfMass = centerOfMass.localPosition;
	}
	
	void Update()
	{
		UpdateMeshesPositions ();
	}
	
	void FixedUpdate()
	{// 0 is front left and 1 is front right
		
		float fixedAngel = steer * 45f;
		wheelColliders [0].steerAngle = fixedAngel;
		wheelColliders [1].steerAngle = fixedAngel;

		
		
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
		//If truck was moving backward, set accelaration 0 to move forward
		if(acceleration<0)
		{
			Debug.Log("Resetting accelration");
			acceleration=0f;
			isBraking = true;
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
			isBraking = true;
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

	private void applyBraking()
	{
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

}
