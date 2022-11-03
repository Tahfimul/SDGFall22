using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoTruck_3 : MonoBehaviour
{

    public Vector3 gapToSideSensors = new Vector3(30f, 10f, 30f);

    public float SensorLength;

    public float SensorsAngle;
    void FixedUpdate()
    {
        Sensors();
    }

    private void Sensors()
    {
        LeftFrontSensors();
    }

    private void LeftFrontSensors()
    {
        RaycastHit hit;

        Vector3 leftFrontSensorsStartPos = transform.position;

        leftFrontSensorsStartPos += transform.up * gapToSideSensors.y;

        leftFrontSensorsStartPos -= transform.right * gapToSideSensors.x;

        leftFrontSensorsStartPos -= transform.forward * gapToSideSensors.z;

        Ray leftFrontSensorsRay =new Ray(leftFrontSensorsStartPos, -transform.right);

        if(Physics.Raycast(leftFrontSensorsRay, out hit, SensorLength))
        {
            CarSensorsManager.Current.reportDetection(Truck.CargoTruck_3, SensorsTypes.LeftFrontCenterSensor);
            Debug.DrawRay(leftFrontSensorsStartPos, -transform.right*hit.distance, Color.red);

        }
        else if(CarSensorsManager.Current.isReported(Truck.CargoTruck_3, SensorsTypes.LeftFrontCenterSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.CargoTruck_3, SensorsTypes.LeftFrontCenterSensor);
        } 
        else
        {
            Debug.DrawRay(leftFrontSensorsStartPos, -transform.right*SensorLength, Color.green);
       
        }

        leftFrontSensorsRay = new Ray(leftFrontSensorsStartPos, Quaternion.AngleAxis(SensorsAngle, transform.up)*-transform.right);

        if(Physics.Raycast(leftFrontSensorsRay, out hit, SensorLength))
        {
            CarSensorsManager.Current.reportDetection(Truck.CargoTruck_2, SensorsTypes.LeftFrontCenterRightSensor);
            Debug.DrawRay(leftFrontSensorsStartPos, Quaternion.AngleAxis(SensorsAngle, transform.up)*-transform.right*hit.distance, Color.red);
        }
        else if(CarSensorsManager.Current.isReported(Truck.CargoTruck_2, SensorsTypes.LeftFrontCenterRightSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.CargoTruck_2, SensorsTypes.LeftFrontCenterRightSensor);
        }
        else
        {
            Debug.DrawRay(leftFrontSensorsStartPos, Quaternion.AngleAxis(SensorsAngle, transform.up)*-transform.right*SensorLength, Color.green);
        }

        leftFrontSensorsRay = new Ray(leftFrontSensorsStartPos, Quaternion.AngleAxis(-SensorsAngle, transform.up)*-transform.right);

        if(Physics.Raycast(leftFrontSensorsRay, out hit, SensorLength))
        {
            CarSensorsManager.Current.reportDetection(Truck.CargoTruck_2, SensorsTypes.LeftFrontCenterLeftSensor);
            Debug.DrawRay(leftFrontSensorsStartPos, Quaternion.AngleAxis(-SensorsAngle, transform.up)*-transform.right*hit.distance, Color.red);
        }
        else if(CarSensorsManager.Current.isReported(Truck.CargoTruck_2, SensorsTypes.LeftFrontCenterLeftSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.CargoTruck_2, SensorsTypes.LeftFrontCenterLeftSensor);
        }
        else
        {
            Debug.DrawRay(leftFrontSensorsStartPos, Quaternion.AngleAxis(-SensorsAngle, transform.up)*-transform.right*SensorLength, Color.green);
        }

        
    }

}
