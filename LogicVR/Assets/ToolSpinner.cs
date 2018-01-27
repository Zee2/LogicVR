using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.XR.WSA.Input;
public class ToolSpinner : MonoBehaviour {
    public Transform targetRotation;
    float time;

    
    public void SpinRight()
    {
        time = Time.time;
        targetRotation.Rotate(120, 0, 0, Space.Self);
    }
    public void SpinLeft()
    {
        time = Time.time;
        targetRotation.Rotate(-120, 0, 0, Space.Self);
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation.localRotation, Time.time - time);
        
	}
}
