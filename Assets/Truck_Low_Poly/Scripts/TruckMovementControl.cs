using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class TruckMovementControl : MonoBehaviour {
	public WheelCollider[] wheelColliders = new WheelCollider[4];
	public Transform[] tyreMeshes = new Transform[4];
	public float maxTorque = 50.0f;

	private float steer;
	private float acceleration;
	private bool isBraking;
	public float maxBrakeForce;

	private bool goForward;
	private bool goBackward;
	private bool goRight;
	private bool goLeft;


	[Header("Sensors")]
	public float sensorLength = 5f;
	public float gapToFrontSensorXAxisPosition = 10f;
	public float gapTestToFrontSensorZAxis = 20f;

	public Vector3 gapToFrontSensor = new Vector3(0,10f,40f);
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
		UpdateMeshesPositions ();
	}
	
	void FixedUpdate()
	{

		// 0 is front left and 1 is front right
		
		float fixedAngle = steer * 45f;
		wheelColliders [0].steerAngle = fixedAngle;
		wheelColliders [1].steerAngle = fixedAngle;		
		
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

		Sensors();
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

	private void Sensors()
	{
		//Front center sensor
		RaycastHit hit;
		//transform.postion returns the approximate center 
		//of the truck.


		//Since this sensor will be placed at the 
		//front of the truck which is facing the z-axis
		//and transform.position returns the approximate
		//center of the truck, the starting position of
		//the sensor for the front of the truck must be
		//some gap away from the center of the truck
		//on the z-axis and x-axis. We achieve this using
		//the gapToFrontSensor Vector3 object
		//adding (transform.forward*gapToFrontSensor.z) ensures that
		//the sensor is pointing in the direction of the 
		//z-axis of the truck
		//adding (transform.up*gapToFrontSensor.y) ensures that
		//the sensor height(in y-axis) is higher than being below the 
		//truck on the y-axis
		
		Vector3 sensorStartPos = transform.position + (transform.forward*gapToFrontSensor.z);
		sensorStartPos += transform.up * gapToFrontSensor.y;

		Debug.Log(transform.up);

		var ray = new Ray(sensorStartPos, this.transform.forward);

		if(Physics.Raycast(ray, out hit, sensorLength))
		{

			TruckSensorsManager.Current.reportDetection(SensorsTypes.FrontCenterSensor, hit.collider.gameObject.tag);
			Debug.DrawRay(sensorStartPos, transform.forward*hit.distance, Color.red);
			
		}
		else if(TruckSensorsManager.Current.isReported(SensorsTypes.FrontCenterSensor))
		{
			TruckSensorsManager.Current.unreportDetection(SensorsTypes.FrontCenterSensor);
			
		}

		//Front Center Right sensor
		sensorStartPos += transform.right * gapToFrontSensorXAxisPosition;

		ray = new Ray(sensorStartPos, this.transform.forward);

		if(Physics.Raycast(ray, out hit, sensorLength))
		{
			TruckSensorsManager.Current.reportDetection(SensorsTypes.FrontCenterRightSensor, hit.collider.gameObject.tag);
			Debug.DrawRay(sensorStartPos, transform.forward*hit.distance, Color.red);
		}
		else if(TruckSensorsManager.Current.isReported(SensorsTypes.FrontCenterRightSensor))
		{
			TruckSensorsManager.Current.unreportDetection(SensorsTypes.FrontCenterRightSensor);
			
		}

		//Front Center left sensor

		sensorStartPos -= transform.right*gapToFrontSensorXAxisPosition*2;


		ray = new Ray(sensorStartPos, this.transform.forward);

		if(Physics.Raycast(ray, out hit, sensorLength))
		{
			TruckSensorsManager.Current.reportDetection(SensorsTypes.FrontCenterLeftSensor, hit.collider.gameObject.tag);
			Debug.DrawRay(sensorStartPos, transform.forward*hit.distance, Color.red); 
		}
		else if(TruckSensorsManager.Current.isReported(SensorsTypes.FrontCenterLeftSensor))
		{
			TruckSensorsManager.Current.unreportDetection(SensorsTypes.FrontCenterLeftSensor);
			
		}



	}
}
