using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumper_3_ : MonoBehaviour
{
    public Vector3 gapToSideSensors = new Vector3(30f, 10f, 15f);
    public float sensorLength = 120f;
    public float sensorsAngle = 30f;

    void FixedUpdate()
    {
        Sensors();
    }

    private void Sensors()
    {
        rightFrontSensors();
        rightBackSensors();
    }

     private void rightFrontSensors()
    {
        RaycastHit hit;

        //Right Front Center Sensor
        Vector3 rightSensorsStartPosition = transform.position;

        rightSensorsStartPosition += transform.right * gapToSideSensors.x;

        rightSensorsStartPosition += transform.up * gapToSideSensors.y;

        rightSensorsStartPosition -= transform.forward*gapToSideSensors.z;

        Ray rightSensorsRay = new Ray(rightSensorsStartPosition, transform.right);
        if(Physics.Raycast(rightSensorsRay, out hit, sensorLength))
        {
            CarSensorsManager.Current.reportDetection(Truck.Dumper_3, SensorsTypes.RightFrontCenterSensor);
           
            Debug.DrawRay(rightSensorsStartPosition, transform.right*hit.distance, Color.red);
        }
        else if(CarSensorsManager.Current.isReported(Truck.Dumper_3, SensorsTypes.RightFrontCenterSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.Dumper_3, SensorsTypes.RightFrontCenterSensor);
           
        }
        else
        {
            
            Debug.DrawRay(rightSensorsStartPosition, transform.right*sensorLength, Color.green);
        
        }

        //Right Front Center Right Angle Sensor
        rightSensorsRay = new Ray(rightSensorsStartPosition, Quaternion.AngleAxis(sensorsAngle, transform.up)*transform.right);

        if(Physics.Raycast(rightSensorsRay, out hit, sensorLength))
        {
            CarSensorsManager.Current.reportDetection(Truck.Dumper_3, SensorsTypes.RightFrontCenterRightSensor);
           
            Debug.DrawRay(rightSensorsStartPosition, Quaternion.AngleAxis(sensorsAngle, transform.up)*transform.right*hit.distance, Color.red);
        }
        else if(CarSensorsManager.Current.isReported(Truck.Dumper_3, SensorsTypes.RightFrontCenterRightSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.Dumper_3, SensorsTypes.RightFrontCenterRightSensor);
           
        }
        else{
            Debug.DrawRay(rightSensorsStartPosition, Quaternion.AngleAxis(sensorsAngle, transform.up)*transform.right*sensorLength, Color.green);
        }

        rightSensorsRay = new Ray(rightSensorsStartPosition, Quaternion.AngleAxis(-sensorsAngle, transform.up)*transform.right);

        if(Physics.Raycast(rightSensorsRay, out hit, sensorLength))
        {
            CarSensorsManager.Current.reportDetection(Truck.Dumper_3, SensorsTypes.RightFrontCenterLeftSensor);
           
            Debug.DrawRay(rightSensorsStartPosition, Quaternion.AngleAxis(-sensorsAngle, transform.up)*transform.right*hit.distance);
        }
        else if(CarSensorsManager.Current.isReported(Truck.Dumper_3, SensorsTypes.RightFrontCenterLeftSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.Dumper_3, SensorsTypes.RightFrontCenterLeftSensor);
        }
        else
        {
            
            Debug.DrawRay(rightSensorsStartPosition, Quaternion.AngleAxis(-sensorsAngle, transform.up)*transform.right*sensorLength, Color.green);
    
        }
    }

    private void rightBackSensors()
    {
        RaycastHit hit;

        Vector3 rightBackSensorsStartPos = transform.position;

        rightBackSensorsStartPos += transform.right*gapToSideSensors.x;

        rightBackSensorsStartPos -= transform.forward * (gapToSideSensors.z*3);

        rightBackSensorsStartPos += transform.up * gapToSideSensors.y;

        Ray rightBackSensorsRay = new Ray(rightBackSensorsStartPos, transform.right);
        if(Physics.Raycast(rightBackSensorsRay, out hit, sensorLength))
        {
            CarSensorsManager.Current.reportDetection(Truck.Dumper_3, SensorsTypes.RightBackCenterSensor);
            Debug.DrawRay(rightBackSensorsStartPos, transform.right*hit.distance, Color.red);
        }
        else if(CarSensorsManager.Current.isReported(Truck.Dumper_3, SensorsTypes.RightBackCenterSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.Dumper_3, SensorsTypes.RightBackCenterSensor);
        }
        else
        {
            Debug.DrawRay(rightBackSensorsStartPos, transform.right*sensorLength, Color.green);
        } 


        rightBackSensorsRay = new Ray(rightBackSensorsStartPos, Quaternion.AngleAxis(sensorsAngle, transform.up)*transform.right);
        if(Physics.Raycast(rightBackSensorsRay, out hit, sensorLength))
        {
            CarSensorsManager.Current.reportDetection(Truck.Dumper_3, SensorsTypes.RightBackCenterSensor);
            Debug.DrawRay(rightBackSensorsStartPos, Quaternion.AngleAxis(sensorsAngle, transform.up)*transform.right*hit.distance, Color.red);
        }
        else if(CarSensorsManager.Current.isReported(Truck.Dumper_3, SensorsTypes.RightBackCenterSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.Dumper_3, SensorsTypes.RightBackCenterSensor);
        }
        else
        {
            Debug.DrawRay(rightBackSensorsStartPos, Quaternion.AngleAxis(sensorsAngle, transform.up)*transform.right*sensorLength, Color.green);
        } 

         rightBackSensorsRay = new Ray(rightBackSensorsStartPos, Quaternion.AngleAxis(-sensorsAngle, transform.up)*transform.right);
        if(Physics.Raycast(rightBackSensorsRay, out hit, sensorLength))
        {
            CarSensorsManager.Current.reportDetection(Truck.Dumper_3, SensorsTypes.RightBackCenterSensor);
            Debug.DrawRay(rightBackSensorsStartPos, Quaternion.AngleAxis(-sensorsAngle, transform.up)*transform.right*hit.distance, Color.red);
        }
        else if(CarSensorsManager.Current.isReported(Truck.Dumper_3, SensorsTypes.RightBackCenterSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.Dumper_3, SensorsTypes.RightBackCenterSensor);
        }
        else
        {
            Debug.DrawRay(rightBackSensorsStartPos, Quaternion.AngleAxis(-sensorsAngle, transform.up)*transform.right*sensorLength, Color.green);
        } 

    }


}
