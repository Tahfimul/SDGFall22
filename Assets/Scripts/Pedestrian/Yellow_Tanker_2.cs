using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow_Tanker_2 : MonoBehaviour
{
    public Vector3 gapToBackSensors = new Vector3(15f, 10f, 80f);
    public Vector3 gapToSideSensors = new Vector3(30f, 10f, 15f);
    public float sensorLength = 120f;

    public float sensorsAngle = 30f;

    void FixedUpdate()
    {
        Sensors();
    }

    private void Sensors()
    {
        rightBackSensors();
        backSensors();
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
            CarSensorsManager.Current.reportDetection(Truck.YellowTanker_2, SensorsTypes.RightBackCenterSensor);
            Debug.DrawRay(rightBackSensorsStartPos, transform.right*hit.distance, Color.red);
        }
        else if(CarSensorsManager.Current.isReported(Truck.YellowTanker_2, SensorsTypes.RightBackCenterSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.YellowTanker_2, SensorsTypes.RightBackCenterSensor);
        }
        else
        {
            Debug.DrawRay(rightBackSensorsStartPos, transform.right*sensorLength, Color.green);
        } 


        rightBackSensorsRay = new Ray(rightBackSensorsStartPos, Quaternion.AngleAxis(sensorsAngle, transform.up)*transform.right);
        if(Physics.Raycast(rightBackSensorsRay, out hit, sensorLength))
        {
            CarSensorsManager.Current.reportDetection(Truck.YellowTanker_2, SensorsTypes.RightBackCenterSensor);
            Debug.DrawRay(rightBackSensorsStartPos, Quaternion.AngleAxis(sensorsAngle, transform.up)*transform.right*hit.distance, Color.red);
        }
        else if(CarSensorsManager.Current.isReported(Truck.YellowTanker_2, SensorsTypes.RightBackCenterSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.YellowTanker_2, SensorsTypes.RightBackCenterSensor);
        }
        else
        {
            Debug.DrawRay(rightBackSensorsStartPos, Quaternion.AngleAxis(sensorsAngle, transform.up)*transform.right*sensorLength, Color.green);
        } 

         rightBackSensorsRay = new Ray(rightBackSensorsStartPos, Quaternion.AngleAxis(-sensorsAngle, transform.up)*transform.right);
        if(Physics.Raycast(rightBackSensorsRay, out hit, sensorLength))
        {
            CarSensorsManager.Current.reportDetection(Truck.YellowTanker_2, SensorsTypes.RightBackCenterSensor);
            Debug.DrawRay(rightBackSensorsStartPos, Quaternion.AngleAxis(-sensorsAngle, transform.up)*transform.right*hit.distance, Color.red);
        }
        else if(CarSensorsManager.Current.isReported(Truck.YellowTanker_2, SensorsTypes.RightBackCenterSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.YellowTanker_2, SensorsTypes.RightBackCenterSensor);
        }
        else
        {
            Debug.DrawRay(rightBackSensorsStartPos, Quaternion.AngleAxis(-sensorsAngle, transform.up)*transform.right*sensorLength, Color.green);
        } 
    }

    private void backSensors()
    {

        RaycastHit hit;

        Vector3 backSensorsStartPos = transform.position;

        backSensorsStartPos -= transform.forward * gapToBackSensors.z;

        backSensorsStartPos += transform.up * gapToBackSensors.y;

        Ray backSensorsRay = new Ray(backSensorsStartPos, -transform.forward);

        if(Physics.Raycast(backSensorsRay, out hit, sensorLength))
        {
          Debug.DrawRay(backSensorsStartPos, -transform.forward*hit.distance, Color.red);  
          CarSensorsManager.Current.reportDetection(Truck.YellowTanker_2, SensorsTypes.BackCenterSensor);      
        }
        else if(CarSensorsManager.Current.isReported(Truck.YellowTanker_2, SensorsTypes.BackCenterSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.YellowTanker_2, SensorsTypes.BackCenterSensor);
        }
        else
        {
            Debug.DrawRay(backSensorsStartPos, -transform.forward*sensorLength, Color.green);
    
        }

        backSensorsStartPos += transform.right*gapToBackSensors.x;

        backSensorsRay = new Ray(backSensorsStartPos, -transform.forward);

        if(Physics.Raycast(backSensorsRay, out hit, sensorLength))
        {
            Debug.DrawRay(backSensorsStartPos, -transform.forward*hit.distance, Color.red);

            CarSensorsManager.Current.reportDetection(Truck.YellowTanker_2, SensorsTypes.BackCenterRightSensor);
        }
        else if(CarSensorsManager.Current.isReported(Truck.YellowTanker_2, SensorsTypes.BackCenterRightSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.YellowTanker_2, SensorsTypes.BackCenterRightSensor);
        }
        else
        {
            
            Debug.DrawRay(backSensorsStartPos, -transform.forward*sensorLength, Color.green);
    
        }

        backSensorsStartPos -= transform.right*(gapToBackSensors.x*2);

        backSensorsRay = new Ray(backSensorsStartPos, -transform.forward);

        if(Physics.Raycast(backSensorsRay, out hit, sensorLength))
        {
            Debug.DrawRay(backSensorsStartPos, -transform.forward*hit.distance, Color.red);
            CarSensorsManager.Current.reportDetection(Truck.YellowTanker_2, SensorsTypes.BackCenterLeftSensor);
        }
        else if(CarSensorsManager.Current.isReported(Truck.YellowTanker_2, SensorsTypes.BackCenterLeftSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.YellowTanker_2, SensorsTypes.BackCenterLeftSensor);
        } 
        else
        {
            Debug.DrawRay(backSensorsStartPos, -transform.forward*sensorLength, Color.green);
        }
    }

    

}
