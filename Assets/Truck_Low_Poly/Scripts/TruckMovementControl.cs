using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
	public Vector3 gapToFrontSensors = new Vector3(30f,10f,40f);
	public Vector3 gapToFrontSideSensors = new Vector3(40f, 10f, 40f);

	public Vector3 gapToBackSideSensors = new Vector3(40f, 10f, 120f);
	public Vector3 gapToBackSensors = new Vector3(30f, 10f, 170f);
	public float sensorsAngle = 30f;

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

		// 0 is front left and 1 is front right
		updateWheelColliders();
		
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

		//Note: the sensors are positioned with the truck facing the
		//z-axis and the camera positoned to point down to the z-axis
		//from the y-axis looking down. So while the game is running,
		//look under the scene tab, with the truck positioned facing the
		//the z-axis and then look down at the truck from the y-axis,
		//in order to be able to view the sensors accurately in the way 
		//that they are positioned.
		frontSensors();
		rightFrontSensors();
		rightBackSensors();
		leftFrontSensors();
		leftBackSensors();
		backSensors();
		
		
		
		 

	}

	private void frontSensors()
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
		
		Vector3 frontSensorStartPos = transform.position + (transform.forward*gapToFrontSensors.z);
		frontSensorStartPos += transform.up * gapToFrontSensors.y;

		var frontSensorRay = new Ray(frontSensorStartPos, this.transform.forward);

		if(Physics.Raycast(frontSensorRay, out hit, sensorLength))
		{

			TruckSensorsManager.Current.reportDetection(SensorsTypes.FrontCenterSensor, hit.collider.gameObject.tag);
			Debug.DrawRay(frontSensorStartPos, transform.forward*hit.distance, Color.red);
			
		}
		else if(TruckSensorsManager.Current.isReported(SensorsTypes.FrontCenterSensor))
		{
			TruckSensorsManager.Current.unreportDetection(SensorsTypes.FrontCenterSensor);
			
		}
		else
		{
			Debug.DrawRay(frontSensorStartPos, transform.forward*sensorLength, Color.green);
		}

		//Front Center Right sensor
		frontSensorStartPos += transform.right * gapToFrontSensors.x;

		frontSensorRay = new Ray(frontSensorStartPos, this.transform.forward);

		if(Physics.Raycast(frontSensorRay, out hit, sensorLength))
		{
			TruckSensorsManager.Current.reportDetection(SensorsTypes.FrontCenterRightSensor, hit.collider.gameObject.tag);
			Debug.DrawRay(frontSensorStartPos, transform.forward*hit.distance, Color.red);
		}
		else if(TruckSensorsManager.Current.isReported(SensorsTypes.FrontCenterRightSensor))
		{
			TruckSensorsManager.Current.unreportDetection(SensorsTypes.FrontCenterRightSensor);
			
		}
		else
		{
			Debug.DrawRay(frontSensorStartPos, transform.forward*sensorLength, Color.green);
		}

		//Front Center left sensor

		frontSensorStartPos -= transform.right*gapToFrontSensors.x*2;


		frontSensorRay = new Ray(frontSensorStartPos, this.transform.forward);

		if(Physics.Raycast(frontSensorRay, out hit, sensorLength)) 
		{
			TruckSensorsManager.Current.reportDetection(SensorsTypes.FrontCenterLeftSensor, hit.collider.gameObject.tag);
			Debug.DrawRay(frontSensorStartPos, transform.forward*hit.distance, Color.red); 
		}
		else if(TruckSensorsManager.Current.isReported(SensorsTypes.FrontCenterLeftSensor))
		{
			TruckSensorsManager.Current.unreportDetection(SensorsTypes.FrontCenterLeftSensor);
			
		}
		else
		{
			Debug.DrawRay(frontSensorStartPos, transform.forward*sensorLength, Color.green);
		}
		
	}

	private void rightFrontSensors()
	{
		RaycastHit hit;
		// Right Front Center Sensor

		//To positon the sensor on the x axis and gapToRightFrontSensor.x units away from the 
		//x-axis value of the center of the truck 
		Vector3 rightFrontSensorStartPos = transform.position+(transform.right*gapToFrontSideSensors.x);
		//To position the sensor so that it is gapToRightFrontSensor.y units on the upward direction
		//of the y axis
		rightFrontSensorStartPos += transform.up * gapToFrontSideSensors.y;
		//To position the sensor so that it is gapToRightFrontSensor.z units backward
		// on the z-axis 
		rightFrontSensorStartPos -= transform.forward * gapToFrontSideSensors.z;

		var rightFrontSensorRay = new Ray(rightFrontSensorStartPos, this.transform.right);

		if(Physics.Raycast(rightFrontSensorRay, out hit, sensorLength))
		{
			TruckSensorsManager.Current.reportDetection(SensorsTypes.RightFrontCenterSensor, hit.collider.gameObject.tag);
			Debug.DrawRay(rightFrontSensorStartPos, transform.right*hit.distance, Color.red);
		}
		else if(TruckSensorsManager.Current.isReported(SensorsTypes.RightFrontCenterSensor))
		{
			TruckSensorsManager.Current.unreportDetection(SensorsTypes.RightFrontCenterSensor);
		}
		else
		{
			
			Debug.DrawRay(rightFrontSensorStartPos, transform.right*sensorLength, Color.green);

		}

		//Right Front Center Right Angle sensor

		rightFrontSensorRay = new Ray(rightFrontSensorStartPos, Quaternion.AngleAxis(sensorsAngle, transform.up)*transform.right);

		if(Physics.Raycast(rightFrontSensorRay, out hit, sensorLength))
		{
			TruckSensorsManager.Current.reportDetection(SensorsTypes.RightFrontCenterRightSensor, hit.collider.gameObject.tag);
			Debug.DrawRay(rightFrontSensorStartPos, Quaternion.AngleAxis(sensorsAngle, transform.up)*transform.right*hit.distance, Color.red);
		}
		else if(TruckSensorsManager.Current.isReported(SensorsTypes.RightFrontCenterRightSensor))
		{
			TruckSensorsManager.Current.unreportDetection(SensorsTypes.RightFrontCenterRightSensor);
		}
		else
		{
			Debug.DrawRay(rightFrontSensorStartPos, Quaternion.AngleAxis(sensorsAngle, transform.up)*transform.right*sensorLength, Color.green);	
		}

		//Right Front Center Left Angle sensor

		rightFrontSensorRay = new Ray(rightFrontSensorStartPos, Quaternion.AngleAxis(-sensorsAngle, transform.up)*transform.right);

		if(Physics.Raycast(rightFrontSensorRay, out hit, sensorLength))
		{
		    TruckSensorsManager.Current.reportDetection(SensorsTypes.RightFrontCenterLeftSensor, hit.collider.gameObject.tag);
			Debug.DrawRay(rightFrontSensorStartPos, Quaternion.AngleAxis(-sensorsAngle, transform.up)*transform.right*hit.distance, Color.red);
		}
		else if(TruckSensorsManager.Current.isReported(SensorsTypes.RightFrontCenterLeftSensor))
		{
			TruckSensorsManager.Current.unreportDetection(SensorsTypes.RightFrontCenterLeftSensor);
		}
		else
		{
			Debug.DrawRay(rightFrontSensorStartPos, Quaternion.AngleAxis(-sensorsAngle, transform.up)*transform.right*sensorLength, Color.green);	
		}

	}

	private void rightBackSensors()
	{
		
		RaycastHit hit;
		//Right Back Center Sensor

		//To positon the sensor on the x axis and gapToBackSideSensors.x units away from the 
		//x-axis value of the center of the truck 
		Vector3 rightBackSensorStartPos = transform.position+(transform.right*gapToBackSideSensors.x);
		//To position the sensor so that it is gapToBackSideSensors.y units on the upward direction
		//of the y axis
		rightBackSensorStartPos += transform.up * gapToBackSideSensors.y;
		//To position the sensor so that it is gapToBackSideSensors.z units backward
		// on the z-axis 
		rightBackSensorStartPos -= transform.forward * gapToBackSideSensors.z;

		var rightBackSensorRay = new Ray(rightBackSensorStartPos, this.transform.right);

		if(Physics.Raycast(rightBackSensorRay, out hit, sensorLength))
		{
			TruckSensorsManager.Current.reportDetection(SensorsTypes.RightBackCenterSensor, hit.collider.gameObject.tag);
			Debug.DrawRay(rightBackSensorStartPos, transform.right*hit.distance, Color.red);
		}
		else if(TruckSensorsManager.Current.isReported(SensorsTypes.RightBackCenterSensor))
		{
			TruckSensorsManager.Current.unreportDetection(SensorsTypes.RightBackCenterSensor);
		}
		else
		{
			
			Debug.DrawRay(rightBackSensorStartPos, transform.right*sensorLength, Color.green);

		}

		//Right Back Center Angle Right Sensor
		rightBackSensorRay = new Ray(rightBackSensorStartPos, Quaternion.AngleAxis(sensorsAngle, transform.up)*transform.right);

		if(Physics.Raycast(rightBackSensorRay, out hit, sensorLength))
		{
			TruckSensorsManager.Current.reportDetection(SensorsTypes.RightBackCenterRightSensor, hit.collider.gameObject.tag);
			Debug.DrawRay(rightBackSensorStartPos, Quaternion.AngleAxis(sensorsAngle, transform.up)*transform.right*hit.distance, Color.red);
		}
		else if(TruckSensorsManager.Current.isReported(SensorsTypes.RightBackCenterRightSensor))
		{
			TruckSensorsManager.Current.unreportDetection(SensorsTypes.RightBackCenterRightSensor);
		}
		else
		{
			Debug.DrawRay(rightBackSensorStartPos, Quaternion.AngleAxis(sensorsAngle, transform.up)*transform.right*sensorLength, Color.green);	
		}

		//Right Back Center Angle Left Sensor
		rightBackSensorRay = new Ray(rightBackSensorStartPos, Quaternion.AngleAxis(-sensorsAngle, transform.up)*transform.right);

		if(Physics.Raycast(rightBackSensorRay, out hit, sensorLength))
		{
			TruckSensorsManager.Current.reportDetection(SensorsTypes.RightBackCenterLeftSensor, hit.collider.gameObject.tag);
			Debug.DrawRay(rightBackSensorStartPos, Quaternion.AngleAxis(-sensorsAngle, transform.up)*transform.right*hit.distance, Color.red);
		}
		else if(TruckSensorsManager.Current.isReported(SensorsTypes.RightBackCenterLeftSensor))
		{
			TruckSensorsManager.Current.unreportDetection(SensorsTypes.RightBackCenterLeftSensor);
		}
		else
		{
			Debug.DrawRay(rightBackSensorStartPos, Quaternion.AngleAxis(-sensorsAngle, transform.up)*transform.right*sensorLength, Color.green);	
		}

	}

	private void leftFrontSensors()
	{
		RaycastHit hit;
		//Left Front Center Sensor

		//To positon the sensor on the x axis and gapToFrontSideSensors.x units away from the 
		//x-axis value of the center of the truck 
		Vector3 leftFrontSensorStartPos = transform.position-(transform.right*gapToFrontSideSensors.x);
		//To position the sensor so that it is gapToFrontSideSensors.y units on the upward direction
		//of the y axis
		leftFrontSensorStartPos += transform.up * gapToFrontSideSensors.y;
		//To position the sensor so that it is gapToFrontSideSensors.z units backward
		// on the z-axis 
		leftFrontSensorStartPos -= transform.forward * gapToFrontSideSensors.z;

		var leftFrontSensorRay = new Ray(leftFrontSensorStartPos, -this.transform.right);

		if(Physics.Raycast(leftFrontSensorRay, out hit, sensorLength))
		{
			TruckSensorsManager.Current.reportDetection(SensorsTypes.LeftFrontCenterSensor, hit.collider.gameObject.tag);
			Debug.DrawRay(leftFrontSensorStartPos, -transform.right*hit.distance, Color.red);
		}
		else if(TruckSensorsManager.Current.isReported(SensorsTypes.LeftFrontCenterSensor))
		{
			TruckSensorsManager.Current.unreportDetection(SensorsTypes.LeftFrontCenterSensor);
		}
		else
		{
			
			Debug.DrawRay(leftFrontSensorStartPos, -transform.right*sensorLength, Color.green);

		}

		//Left Front Center Right Angle sensor

		leftFrontSensorRay = new Ray(leftFrontSensorStartPos, Quaternion.AngleAxis(sensorsAngle, transform.up)*-transform.right);

		if(Physics.Raycast(leftFrontSensorRay, out hit, sensorLength))
		{
			TruckSensorsManager.Current.reportDetection(SensorsTypes.LeftFrontCenterRightSensor, hit.collider.gameObject.tag);
			Debug.DrawRay(leftFrontSensorStartPos, Quaternion.AngleAxis(sensorsAngle, transform.up)*-transform.right*hit.distance, Color.red);
		}
		else if(TruckSensorsManager.Current.isReported(SensorsTypes.LeftFrontCenterRightSensor))
		{
			TruckSensorsManager.Current.unreportDetection(SensorsTypes.LeftFrontCenterRightSensor);
		}
		else
		{
			Debug.DrawRay(leftFrontSensorStartPos, Quaternion.AngleAxis(sensorsAngle, transform.up)*-transform.right*sensorLength, Color.green);	
		}

		//Left Front Center Left Angle sensor

		leftFrontSensorRay = new Ray(leftFrontSensorStartPos, Quaternion.AngleAxis(-sensorsAngle, transform.up)*-transform.right);

		if(Physics.Raycast(leftFrontSensorRay, out hit, sensorLength))
		{
		    TruckSensorsManager.Current.reportDetection(SensorsTypes.LeftFrontCenterLeftSensor, hit.collider.gameObject.tag);
			Debug.DrawRay(leftFrontSensorStartPos, Quaternion.AngleAxis(-sensorsAngle, transform.up)*-transform.right*hit.distance, Color.red);
		}
		else if(TruckSensorsManager.Current.isReported(SensorsTypes.LeftFrontCenterLeftSensor))
		{
			TruckSensorsManager.Current.unreportDetection(SensorsTypes.LeftFrontCenterLeftSensor);
		}
		else
		{
			Debug.DrawRay(leftFrontSensorStartPos, Quaternion.AngleAxis(-sensorsAngle, transform.up)*-transform.right*sensorLength, Color.green);	
		}

	}

	private void leftBackSensors()
	{
		RaycastHit hit;
		//Left Back Center Sensor

		//To positon the sensor on the x axis and gapToBackSideSensors.x units away from the 
		//x-axis value of the center of the truck 
		Vector3 leftBackSensorStartPos = transform.position-(transform.right*gapToBackSideSensors.x);
		//To position the sensor so that it is gapToBackSideSensors.y units on the upward direction
		//of the y axis
		leftBackSensorStartPos += transform.up * gapToBackSideSensors.y;
		//To position the sensor so that it is gapToBackSideSensors.z units backward
		// on the z-axis 
		leftBackSensorStartPos -= transform.forward * gapToBackSideSensors.z;

		var leftBackSensorRay = new Ray(leftBackSensorStartPos, -this.transform.right);

		if(Physics.Raycast(leftBackSensorRay, out hit, sensorLength))
		{
			TruckSensorsManager.Current.reportDetection(SensorsTypes.LeftBackCenterSensor, hit.collider.gameObject.tag);
			Debug.DrawRay(leftBackSensorStartPos, -transform.right*hit.distance, Color.red);
		}
		else if(TruckSensorsManager.Current.isReported(SensorsTypes.LeftBackCenterSensor))
		{
			TruckSensorsManager.Current.unreportDetection(SensorsTypes.LeftBackCenterSensor);
		}
		else
		{
			
			Debug.DrawRay(leftBackSensorStartPos, -transform.right*sensorLength, Color.green);

		}

		//Left Back Center Angle Right Sensor
		leftBackSensorRay = new Ray(leftBackSensorStartPos, Quaternion.AngleAxis(sensorsAngle, transform.up)*-transform.right);

		if(Physics.Raycast(leftBackSensorRay, out hit, sensorLength))
		{
			TruckSensorsManager.Current.reportDetection(SensorsTypes.LeftBackCenterRightSensor, hit.collider.gameObject.tag);
			Debug.DrawRay(leftBackSensorStartPos, Quaternion.AngleAxis(sensorsAngle, transform.up)*-transform.right*hit.distance, Color.red);
		}
		else if(TruckSensorsManager.Current.isReported(SensorsTypes.LeftBackCenterRightSensor))
		{
			TruckSensorsManager.Current.unreportDetection(SensorsTypes.LeftBackCenterRightSensor);
		}
		else
		{
			Debug.DrawRay(leftBackSensorStartPos, Quaternion.AngleAxis(sensorsAngle, transform.up)*-transform.right*sensorLength, Color.green);	
		}

		//Left Back Center Angle Left Sensor
		leftBackSensorRay = new Ray(leftBackSensorStartPos, Quaternion.AngleAxis(-sensorsAngle, transform.up)*-transform.right);

		if(Physics.Raycast(leftBackSensorRay, out hit, sensorLength))
		{
			TruckSensorsManager.Current.reportDetection(SensorsTypes.LeftBackCenterLeftSensor, hit.collider.gameObject.tag);
			Debug.DrawRay(leftBackSensorStartPos, Quaternion.AngleAxis(-sensorsAngle, transform.up)*-transform.right*hit.distance, Color.red);
		}
		else if(TruckSensorsManager.Current.isReported(SensorsTypes.LeftBackCenterLeftSensor))
		{
			TruckSensorsManager.Current.unreportDetection(SensorsTypes.LeftBackCenterLeftSensor);
		}
		else
		{
			Debug.DrawRay(leftBackSensorStartPos, Quaternion.AngleAxis(-sensorsAngle, transform.up)*-transform.right*sensorLength, Color.green);	
		}
	}

	private void backSensors()
	{

		RaycastHit hit;

		//Back Center Sensor
		Vector3 backSensorsStartPos = transform.position;

		backSensorsStartPos -= transform.forward*gapToBackSensors.z;

		backSensorsStartPos += transform.up * gapToBackSensors.y;

		var backSensorsRay = new Ray(backSensorsStartPos, -transform.forward);

		if(Physics.Raycast(backSensorsRay, out hit, sensorLength))
		{
			TruckSensorsManager.Current.reportDetection(SensorsTypes.BackCenterSensor, hit.collider.gameObject.tag);
			Debug.DrawRay(backSensorsStartPos, -transform.forward*hit.distance, Color.red);
		}
		else if(TruckSensorsManager.Current.isReported(SensorsTypes.BackCenterSensor))
		{
			TruckSensorsManager.Current.unreportDetection(SensorsTypes.BackCenterSensor);
		}
		else
		{
			Debug.DrawRay(backSensorsStartPos, -transform.forward*sensorLength, Color.green); 
	
		}

		//Back Center Right Sensor
		backSensorsStartPos += transform.right * gapToBackSensors.x;

		backSensorsRay = new Ray(backSensorsStartPos, -transform.forward);

		if(Physics.Raycast(backSensorsRay, out hit, sensorLength))
		{
			TruckSensorsManager.Current.reportDetection(SensorsTypes.BackCenterRightSensor, hit.collider.gameObject.tag);
			Debug.DrawRay(backSensorsStartPos, -transform.forward*hit.distance, Color.red);
		}
		else if(TruckSensorsManager.Current.isReported(SensorsTypes.BackCenterRightSensor))
		{
			TruckSensorsManager.Current.unreportDetection(SensorsTypes.BackCenterRightSensor);
		}
		else
		{
			Debug.DrawRay(backSensorsStartPos, -transform.forward*sensorLength, Color.green);
		}

		//Back Center Left Sensor
		backSensorsStartPos -= transform.right*(gapToBackSensors.x*2);

		backSensorsRay = new Ray(backSensorsStartPos, -transform.forward);

		if(Physics.Raycast(backSensorsRay, out hit, sensorLength))
		{
			TruckSensorsManager.Current.reportDetection(SensorsTypes.BackCenterLeftSensor, hit.collider.gameObject.tag);
			Debug.DrawRay(backSensorsStartPos, -transform.forward*hit.distance, Color.red);
		}
		else if(TruckSensorsManager.Current.isReported(SensorsTypes.BackCenterLeftSensor))
		{
			TruckSensorsManager.Current.unreportDetection(SensorsTypes.BackCenterLeftSensor);

		}
		else
		{
			Debug.DrawRay(backSensorsStartPos, -transform.forward*sensorLength, Color.green);
		}



	}
	
	public void ExitScene()
	{
		SceneManager.LoadScene("Main_Scene");
	}

}
