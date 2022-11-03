using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumper_2 : MonoBehaviour
{
    public Vector3 gapToBackSensors = new Vector3(15f, 10f, 80f);

    public float SensorLength;
    void FixedUpdate()
    {
        BackSensors();
    }
    private void BackSensors()
    {
        RaycastHit hit;

        Vector3 backSensorsStartPos = transform.position;

        backSensorsStartPos -= transform.forward * gapToBackSensors.z;

        backSensorsStartPos += transform.up * gapToBackSensors.y;

        Ray backSensorsRay = new Ray(backSensorsStartPos, -transform.forward);

        if(Physics.Raycast(backSensorsRay, out hit, SensorLength))
        {
            CarSensorsManager.Current.reportDetection(Truck.Dumper_2, SensorsTypes.BackCenterSensor);
            Debug.DrawRay(backSensorsStartPos, -transform.forward*hit.distance, Color.red);
        }
        else if(CarSensorsManager.Current.isReported(Truck.Dumper_2, SensorsTypes.BackCenterSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.Dumper_2, SensorsTypes.BackCenterSensor);
        }
        else
        {
            Debug.DrawRay(backSensorsStartPos, -transform.forward*SensorLength, Color.green);
        }

        backSensorsStartPos += transform.right*gapToBackSensors.x;

        backSensorsRay = new Ray(backSensorsStartPos, -transform.forward);

        if(Physics.Raycast(backSensorsRay, out hit, SensorLength))
        {
            CarSensorsManager.Current.reportDetection(Truck.Dumper_2, SensorsTypes.BackCenterRightSensor);
            Debug.DrawRay(backSensorsStartPos, -transform.forward*hit.distance, Color.red);
        }
        else if(CarSensorsManager.Current.isReported(Truck.Dumper_2, SensorsTypes.BackCenterRightSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.Dumper_2, SensorsTypes.BackCenterRightSensor);
        }
        else
        {
            Debug.DrawRay(backSensorsStartPos, -transform.forward*SensorLength, Color.green);
        }

        backSensorsStartPos -= transform.right *(gapToBackSensors.x*2);

        backSensorsRay = new Ray(backSensorsStartPos, -transform.forward);

        if(Physics.Raycast(backSensorsRay, out hit, SensorLength))
        {
            CarSensorsManager.Current.reportDetection(Truck.Dumper_2, SensorsTypes.BackCenterLeftSensor);
            Debug.DrawRay(backSensorsStartPos, -transform.forward*hit.distance, Color.red);
        }
        else if(CarSensorsManager.Current.isReported(Truck.Dumper_2, SensorsTypes.BackCenterLeftSensor))
        {
            CarSensorsManager.Current.unreportDetection(Truck.Dumper_2, SensorsTypes.BackCenterLeftSensor);
        }
        else
        {
            Debug.DrawRay(backSensorsStartPos, -transform.forward*SensorLength, Color.green);
        }

    }
}
