using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class WireJoint : MonoBehaviour, ISleepable {

    Transform weldTarget;
    Transform thisTransform;


    public void Awake()
    {
        thisTransform = transform;
        
    }

    public void Weld(Transform t)
    {
        weldTarget = t;
    }

    public void UnWeld()
    {
        weldTarget = thisTransform; 
    }

    void ISleepable.Sleep()
    {
        enabled = false;
    }

    void ISleepable.Wake()
    {
        enabled = true;
    }

    private void Update()
    {
        if(weldTarget != null)
            thisTransform.position = weldTarget.position;
    }
}
