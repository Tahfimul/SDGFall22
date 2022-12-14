using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanker_ : MonoBehaviour
{
    
    public Vector3 gapToFrontSensors = new Vector3(15f, 10f, 10f);

    public float sensorLength = 120f;

    void FixedUpdate()
    {
        Sensors();
    }
    private void Sensors()
    {
        frontSenors();
    }

    private void frontSenors()
    {
        RaycastHit hit;

        //Front Center Sensor
        
        Vector3 frontSensorsStartPos = transform.position;
        frontSensorsStartPos += transform.forward*gapToFrontSensors.z;
        frontSensorsStartPos += transform.up * gapToFrontSensors.y;

        Ray frontSensorsRay = new Ray(frontSensorsStartPos, transform.forward);

        if(Physics.Raycast(frontSensorsRay, out hit, sensorLength))
        {
            Debug.DrawRay(frontSensorsStartPos, transform.forward*hit.distance, Color.red);
            CarSensorsManager.Current.reportDetection(Truck.Tanker, SensorsTypes.FrontCenterSensor);
            
        }
        else if(CarSensorsManager.Current.isReported(Truck.Tanker, SensorsTypes.FrontCenterSensor))
        {
           Debug.Log("clearing is reported"); 
           CarSensorsManager.Current.unreportDetection(Truck.Tanker, SensorsTypes.FrontCenterSensor);
                 
        }
        else
        {   
            Debug.DrawRay(frontSensorsStartPos, transform.forward*sensorLength, Color.green);
            
        }

        //front center right sensor
        frontSensorsStartPos += transform.right * gapToFrontSensors.x;

        frontSensorsRay = new Ray(frontSensorsStartPos, transform.forward);

        if(Physics.Raycast(frontSensorsRay, out hit, sensorLength))
        {
            CarSensorsManager.Current.reportDetection(Truck.Tanker, SensorsTypes.FrontCenterRightSensor);
           
            Debug.DrawRay(frontSensorsStartPos, transform.forward*hit.distance, Color.red);
        }
        else if(CarSensorsManager.Current.isReported(Truck.Tanker, SensorsTypes.FrontCenterRightSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.Tanker, SensorsTypes.FrontCenterRightSensor);
        }

        else
        {
            Debug.DrawRay(frontSensorsStartPos, transform.forward*sensorLength, Color.green);
        }

        frontSensorsStartPos -= transform.right * (gapToFrontSensors.x*2);

        frontSensorsRay = new Ray(frontSensorsStartPos, transform.forward);

        if(Physics.Raycast(frontSensorsRay, out hit, sensorLength))
        {
            CarSensorsManager.Current.reportDetection(Truck.Tanker, SensorsTypes.FrontCenterLeftSensor);
           
            Debug.DrawRay(frontSensorsStartPos, transform.forward*sensorLength, Color.red);    
        }
        else if(CarSensorsManager.Current.isReported(Truck.Tanker, SensorsTypes.FrontCenterLeftSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.Tanker, SensorsTypes.FrontCenterLeftSensor);
        }
        else
        {
            Debug.DrawRay(frontSensorsStartPos, transform.forward*sensorLength, Color.green);
        }
    }
}
