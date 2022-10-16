using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Linq;
enum SensorsTypes 
{
  FrontCenterSensor,
  FrontCenterRightSensor,
  FrontCenterLeftSensor,
  RightFrontCenterSensor,
  RightFrontCenterRightSensor,
  RightFrontCenterLeftSensor,
  RightBackCenterSensor,
  RightBackCenterRightSensor,
  RightBackCenterLeftSensor,
  LeftFrontCenterSensor,
  LeftFrontCenterRightSensor,
  LeftFrontCenterLeftSensor,
  LeftBackCenterSensor,
  LeftBackCenterRightSensor,
  LeftBackCenterLeftSensor,
  BackCenterSensor,
  BackCenterRightSensor,
  BackCenterLeftSensor
}

enum ObjectTypes
{
  car_01,
  car_02,
  car_03,
  car_04,
  car_05,
  car_06,
  car_07,
  car_08,
  car_09,
  bicycle_1,
  bicycle_2,
  bicycle_3,
  bicycle_4,
  bicycle_5,
  bicycle_6,
  bicycle_7
}

class TruckSensorsManager : MonoBehaviour
{

  [Header("Cars")]  
  public GameObject car_01; 
  public GameObject car_02;
  public GameObject car_03;
  public GameObject car_04;
  public GameObject car_05;
  public GameObject car_06;
  public GameObject car_07;
  public GameObject car_08;
  public GameObject car_09;
  
  [Header("Bicycles")]
  public GameObject bicycle_1;
  public GameObject bicycle_2;
  public GameObject bicycle_3;
  public GameObject bicycle_4;
  public GameObject bicycle_5;
  public GameObject bicycle_6;
  public GameObject bicycle_7;  

  public Material objectHighlightedMaterial;        
//Run through each gameobject's gameobjects
  private Dictionary<SensorsTypes, List<string>> truckSensors = new Dictionary<SensorsTypes, List<string>>();  
  private Dictionary<ObjectTypes, List<UnityEngine.Material>> objectMaterials = new Dictionary<ObjectTypes, List<UnityEngine.Material>>();
  static private TruckSensorsManager __Current;

  void OnEnable()
  {
    __Current = this;
  }

  void FixedUpdate()
  {

    foreach(KeyValuePair<SensorsTypes, List<string>> sensor in truckSensors)
    {

        foreach(string obj in sensor.Value)
        {
          if(obj == ObjectTypes.bicycle_1.ToString())
          {
            changeBicycleMaterial(bicycle_1, ObjectTypes.bicycle_1);
          }
          else if(obj == ObjectTypes.bicycle_2.ToString())
          {
            changeBicycleMaterial(bicycle_2, ObjectTypes.bicycle_2);
          }
          else if(obj == ObjectTypes.bicycle_3.ToString())
          {

            changeBicycleMaterial(bicycle_3, ObjectTypes.bicycle_3);
          }
          else if(obj == ObjectTypes.bicycle_4.ToString())
          {

            changeBicycleMaterial(bicycle_4, ObjectTypes.bicycle_4);
          }
          else if(obj == ObjectTypes.bicycle_5.ToString())
          {

            changeBicycleMaterial(bicycle_5, ObjectTypes.bicycle_5);
          }
          else if(obj == ObjectTypes.bicycle_6.ToString())
          {

            changeBicycleMaterial(bicycle_6, ObjectTypes.bicycle_6);
          }
          else if(obj == ObjectTypes.bicycle_7.ToString())
          {
            changeBicycleMaterial(bicycle_7, ObjectTypes.bicycle_7);
          }
          else if(obj == ObjectTypes.car_01.ToString())
          {
            changeCarMeterial(car_01, ObjectTypes.car_01);
          }
          else if(obj == ObjectTypes.car_02.ToString())
          {
            changeCarMeterial(car_02, ObjectTypes.car_02);
          }
          else if(obj == ObjectTypes.car_03.ToString())
          {
            changeCarMeterial(car_03, ObjectTypes.car_03);
          }
          else if(obj == ObjectTypes.car_04.ToString())
          {
            changeCarMeterial(car_04, ObjectTypes.car_04);
          }
          else if(obj == ObjectTypes.car_05.ToString())
          {
            changeCarMeterial(car_05, ObjectTypes.car_05);
          }
          else if(obj == ObjectTypes.car_06.ToString())
          {
            changeCarMeterial(car_06, ObjectTypes.car_06);
          }
          else if(obj == ObjectTypes.car_07.ToString())
          {
            changeCarMeterial(car_07, ObjectTypes.car_07);
          }
          else if(obj == ObjectTypes.car_08.ToString())
          {
            changeCarMeterial(car_08, ObjectTypes.car_08);
          }
          else if(obj == ObjectTypes.car_09.ToString())
          {
            changeCarMeterial(car_09, ObjectTypes.car_09);
          }

        }
    }

  }  

  private void changeBicycleMaterial(GameObject bicycle, ObjectTypes objectType)
  {
    //Create a dictionary to store each material so that 
    //every object's material can be reset to its 
    //original material when bicycle no longer
    //obstructs sensors

    if(!objectMaterials.ContainsKey(objectType))
    {
      objectMaterials.Add(objectType, new List<UnityEngine.Material>());
    }
    foreach(Transform child in bicycle.transform)
    {
        objectMaterials[objectType].Add(child.gameObject.GetComponent<MeshRenderer>().material);
        
        child.gameObject.GetComponent<MeshRenderer>().material = objectHighlightedMaterial;
        Debug.Log(child.gameObject.GetComponent<MeshRenderer>().material.GetType());

    }

  }  

  private void changeBicycleMaterialToOrginal(GameObject bicycle, ObjectTypes objectType)
  {
    int i =0;

    foreach(Transform child in bicycle.transform)
    {
      child.gameObject.GetComponent<MeshRenderer>().material = objectMaterials[objectType][i];
      i++;
    }
  }

  private void changeCarMeterial(GameObject car, ObjectTypes objectType)
  {
    if(!objectMaterials.ContainsKey(objectType))
    {
      objectMaterials.Add(objectType, new List<UnityEngine.Material>());
    }

    objectMaterials[objectType].Add(car.GetComponent<MeshRenderer>().material);
    car.GetComponent<MeshRenderer>().material = objectHighlightedMaterial;
  }

  private void changeCarMaterialToOrginal(GameObject car, ObjectTypes objectType)
  {

    car.GetComponent<MeshRenderer>().material = objectMaterials[objectType][0];
    
  }

  static public TruckSensorsManager Current {

    get
    {

        if(__Current==null)
        {
            __Current = GameObject.FindObjectOfType<TruckSensorsManager>();
        }
        return __Current;
    }

  }

  public void reportDetection(SensorsTypes sensorsType, string obstacleTag)
  {
    if(!truckSensors.ContainsKey(sensorsType))
    {
        truckSensors.Add(sensorsType, new List<string>());
    }

    if(!truckSensors[sensorsType].Contains(obstacleTag))
    {
      if(obstacleTag!="Untagged")
      {
        
        truckSensors[sensorsType].Add(obstacleTag);
     
      }
      
    }
    
  }  

  public void unreportDetection(SensorsTypes sensor)
  {
    
    foreach(string obj in truckSensors[sensor])
    {
          if(obj == ObjectTypes.bicycle_1.ToString())
          {
            changeBicycleMaterialToOrginal(bicycle_1, ObjectTypes.bicycle_1);
          }
          else if(obj == ObjectTypes.bicycle_2.ToString())
          {
            changeBicycleMaterialToOrginal(bicycle_2, ObjectTypes.bicycle_2);
          }
          else if(obj == ObjectTypes.bicycle_3.ToString())
          {

            changeBicycleMaterialToOrginal(bicycle_3, ObjectTypes.bicycle_3);
          }
          else if(obj == ObjectTypes.bicycle_4.ToString())
          {

            changeBicycleMaterialToOrginal(bicycle_4, ObjectTypes.bicycle_4);
          }
          else if(obj == ObjectTypes.bicycle_5.ToString())
          {

            changeBicycleMaterialToOrginal(bicycle_5, ObjectTypes.bicycle_5);
          }
          else if(obj == ObjectTypes.bicycle_6.ToString())
          {

            changeBicycleMaterialToOrginal(bicycle_6, ObjectTypes.bicycle_6);
          }
          else if(obj == ObjectTypes.bicycle_7.ToString())
          {
            changeBicycleMaterialToOrginal(bicycle_7, ObjectTypes.bicycle_7);
          }
          else if(obj == ObjectTypes.car_01.ToString())
          {
            changeCarMaterialToOrginal(car_01, ObjectTypes.car_01);
          }
          else if(obj == ObjectTypes.car_02.ToString())
          {
            changeCarMaterialToOrginal(car_02, ObjectTypes.car_02);
            
          }
          else if(obj == ObjectTypes.car_03.ToString())
          {
            changeCarMaterialToOrginal(car_03, ObjectTypes.car_03);
            
          }
          else if(obj == ObjectTypes.car_04.ToString())
          {
            changeCarMaterialToOrginal(car_04, ObjectTypes.car_04);
            
          }
          else if(obj == ObjectTypes.car_05.ToString())
          {
            changeCarMaterialToOrginal(car_05, ObjectTypes.car_05);
            
          }
          else if(obj == ObjectTypes.car_06.ToString())
          {
            changeCarMaterialToOrginal(car_06, ObjectTypes.car_06);
            
          }
          else if(obj == ObjectTypes.car_07.ToString())
          {
            changeCarMaterialToOrginal(car_07, ObjectTypes.car_07);
            
          }
          else if(obj == ObjectTypes.car_08.ToString())
          {
            changeCarMaterialToOrginal(car_08, ObjectTypes.car_08);
            
          }
          else if(obj == ObjectTypes.car_09.ToString())
          {            
            changeCarMaterialToOrginal(car_09, ObjectTypes.car_09);
          }
    }
    truckSensors[sensor].Clear();
  }
  public bool isReported(SensorsTypes sensor)
  {
    if(truckSensors.ContainsKey(sensor) && truckSensors[sensor].Count>0)
    {
        return true;
    }

    return false;
  }

}