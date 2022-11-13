using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Main_Scene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CarScene()
    {
    	SceneManager.LoadScene("CarScene");
    }
    
    public void TruckScene()
    {
    	SceneManager.LoadScene("TruckScene");
    }
}
