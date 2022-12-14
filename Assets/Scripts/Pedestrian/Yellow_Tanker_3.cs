using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow_Tanker_3 : MonoBehaviour
{
    public Vector3 gapToFrontSensors = new Vector3(15f, 10f, 10f);
    public Vector3 gapToSideSensors = new Vector3(30f, 10f, 15f);
    public Vector3 gapToBackSensors = new Vector3(15f, 10f, 80f);
    public float sensorLength = 120f;

    public float sensorsAngle = 30;

    void FixedUpdate()
    {
        Sensors();
    }

    private void Sensors()
    {
        frontSenors();
        rightFrontSensors();
        rightBackSensors();
        backSensors();
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
            CarSensorsManager.Current.reportDetection(Truck.YellowTanker_3, SensorsTypes.FrontCenterSensor);
            
        }
        else if(CarSensorsManager.Current.isReported(Truck.YellowTanker_3, SensorsTypes.FrontCenterSensor))
        {
           Debug.Log("clearing is reported"); 
           CarSensorsManager.Current.unreportDetection(Truck.YellowTanker_3, SensorsTypes.FrontCenterSensor);
                 
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
            CarSensorsManager.Current.reportDetection(Truck.YellowTanker_3, SensorsTypes.FrontCenterRightSensor);
           
            Debug.DrawRay(frontSensorsStartPos, transform.forward*hit.distance, Color.red);
        }
        else if(CarSensorsManager.Current.isReported(Truck.YellowTanker_3, SensorsTypes.FrontCenterRightSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.YellowTanker_3, SensorsTypes.FrontCenterRightSensor);
        }

        else
        {
            Debug.DrawRay(frontSensorsStartPos, transform.forward*sensorLength, Color.green);
        }

        frontSensorsStartPos -= transform.right * (gapToFrontSensors.x*2);

        frontSensorsRay = new Ray(frontSensorsStartPos, transform.forward);

        if(Physics.Raycast(frontSensorsRay, out hit, sensorLength))
        {
            CarSensorsManager.Current.reportDetection(Truck.YellowTanker_3, SensorsTypes.FrontCenterLeftSensor);
           
            Debug.DrawRay(frontSensorsStartPos, transform.forward*sensorLength, Color.red);    
        }
        else if(CarSensorsManager.Current.isReported(Truck.YellowTanker_3, SensorsTypes.FrontCenterLeftSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.YellowTanker_3, SensorsTypes.FrontCenterLeftSensor);
        }
        else
        {
            Debug.DrawRay(frontSensorsStartPos, transform.forward*sensorLength, Color.green);
        }

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
            CarSensorsManager.Current.reportDetection(Truck.YellowTanker_3, SensorsTypes.RightFrontCenterSensor);
           
            Debug.DrawRay(rightSensorsStartPosition, transform.right*hit.distance, Color.red);
        }
        else if(CarSensorsManager.Current.isReported(Truck.YellowTanker_3, SensorsTypes.RightFrontCenterSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.YellowTanker_3, SensorsTypes.RightFrontCenterSensor);
           
        }
        else
        {
            
            Debug.DrawRay(rightSensorsStartPosition, transform.right*sensorLength, Color.green);
        
        }

        //Right Front Center Right Angle Sensor
        rightSensorsRay = new Ray(rightSensorsStartPosition, Quaternion.AngleAxis(sensorsAngle, transform.up)*transform.right);

        if(Physics.Raycast(rightSensorsRay, out hit, sensorLength))
        {
            CarSensorsManager.Current.reportDetection(Truck.YellowTanker_3, SensorsTypes.RightFrontCenterRightSensor);
           
            Debug.DrawRay(rightSensorsStartPosition, Quaternion.AngleAxis(sensorsAngle, transform.up)*transform.right*hit.distance, Color.red);
        }
        else if(CarSensorsManager.Current.isReported(Truck.YellowTanker_3, SensorsTypes.RightFrontCenterRightSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.YellowTanker_3, SensorsTypes.RightFrontCenterRightSensor);
           
        }
        else{
            Debug.DrawRay(rightSensorsStartPosition, Quaternion.AngleAxis(sensorsAngle, transform.up)*transform.right*sensorLength, Color.green);
        }

        rightSensorsRay = new Ray(rightSensorsStartPosition, Quaternion.AngleAxis(-sensorsAngle, transform.up)*transform.right);

        if(Physics.Raycast(rightSensorsRay, out hit, sensorLength))
        {
            CarSensorsManager.Current.reportDetection(Truck.YellowTanker_3, SensorsTypes.RightFrontCenterLeftSensor);
           
            Debug.DrawRay(rightSensorsStartPosition, Quaternion.AngleAxis(-sensorsAngle, transform.up)*transform.right*hit.distance);
        }
        else if(CarSensorsManager.Current.isReported(Truck.YellowTanker_3, SensorsTypes.RightFrontCenterLeftSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.YellowTanker_3, SensorsTypes.RightFrontCenterLeftSensor);
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
            CarSensorsManager.Current.reportDetection(Truck.YellowTanker_3, SensorsTypes.RightBackCenterSensor);
            Debug.DrawRay(rightBackSensorsStartPos, transform.right*hit.distance, Color.red);
        }
        else if(CarSensorsManager.Current.isReported(Truck.YellowTanker_3, SensorsTypes.RightBackCenterSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.YellowTanker_3, SensorsTypes.RightBackCenterSensor);
        }
        else
        {
            Debug.DrawRay(rightBackSensorsStartPos, transform.right*sensorLength, Color.green);
        } 


        rightBackSensorsRay = new Ray(rightBackSensorsStartPos, Quaternion.AngleAxis(sensorsAngle, transform.up)*transform.right);
        if(Physics.Raycast(rightBackSensorsRay, out hit, sensorLength))
        {
            CarSensorsManager.Current.reportDetection(Truck.YellowTanker_3, SensorsTypes.RightBackCenterSensor);
            Debug.DrawRay(rightBackSensorsStartPos, Quaternion.AngleAxis(sensorsAngle, transform.up)*transform.right*hit.distance, Color.red);
        }
        else if(CarSensorsManager.Current.isReported(Truck.YellowTanker_3, SensorsTypes.RightBackCenterSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.YellowTanker_3, SensorsTypes.RightBackCenterSensor);
        }
        else
        {
            Debug.DrawRay(rightBackSensorsStartPos, Quaternion.AngleAxis(sensorsAngle, transform.up)*transform.right*sensorLength, Color.green);
        } 

         rightBackSensorsRay = new Ray(rightBackSensorsStartPos, Quaternion.AngleAxis(-sensorsAngle, transform.up)*transform.right);
        if(Physics.Raycast(rightBackSensorsRay, out hit, sensorLength))
        {
            CarSensorsManager.Current.reportDetection(Truck.YellowTanker_3, SensorsTypes.RightBackCenterSensor);
            Debug.DrawRay(rightBackSensorsStartPos, Quaternion.AngleAxis(-sensorsAngle, transform.up)*transform.right*hit.distance, Color.red);
        }
        else if(CarSensorsManager.Current.isReported(Truck.YellowTanker_3, SensorsTypes.RightBackCenterSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.YellowTanker_3, SensorsTypes.RightBackCenterSensor);
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
          CarSensorsManager.Current.reportDetection(Truck.YellowTanker_3, SensorsTypes.BackCenterSensor);      
        }
        else if(CarSensorsManager.Current.isReported(Truck.YellowTanker_3, SensorsTypes.BackCenterSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.YellowTanker_3, SensorsTypes.BackCenterSensor);
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

            CarSensorsManager.Current.reportDetection(Truck.YellowTanker_3, SensorsTypes.BackCenterRightSensor);
        }
        else if(CarSensorsManager.Current.isReported(Truck.YellowTanker_3, SensorsTypes.BackCenterRightSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.YellowTanker_3, SensorsTypes.BackCenterRightSensor);
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
            CarSensorsManager.Current.reportDetection(Truck.YellowTanker_3, SensorsTypes.BackCenterLeftSensor);
        }
        else if(CarSensorsManager.Current.isReported(Truck.YellowTanker_3, SensorsTypes.BackCenterLeftSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.YellowTanker_3, SensorsTypes.BackCenterLeftSensor);
        } 
        else
        {
            Debug.DrawRay(backSensorsStartPos, -transform.forward*sensorLength, Color.green);
        }
    }
}
