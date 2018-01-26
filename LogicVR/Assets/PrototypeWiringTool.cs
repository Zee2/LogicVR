using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using HoloToolkit.Unity.InputModule;

public class PrototypeWiringTool : AttachToController {

    public Transform manipulationPoint;
    public GameObject wirePrefab;
    WireJoint currentJoint = null;
    enum WiringMode
    {
        NotEngaged,
        Dragging
    }

    protected override void OnAttachToController()
    {
        InteractionManager.InteractionSourcePressed += InteractionManager_InteractionSourcePressed;
        base.OnAttachToController();
    }

    protected override void OnDetachFromController()
    {
        InteractionManager.InteractionSourcePressed -= InteractionManager_InteractionSourcePressed;
        base.OnDetachFromController();
    }

    private void InteractionManager_InteractionSourcePressed(InteractionSourcePressedEventArgs obj)
    {
        Debug.LogWarning("Pressed!");
        if(obj.state.source.handedness == handedness && obj.pressType == InteractionSourcePressType.Select)
        {
            Debug.Log("Correct input type.");
            if(currentJoint == null)
            {
                Debug.Log("No joint current selected.");
                Collider[] colls = Physics.OverlapSphere(manipulationPoint.position, 0.2f);
                Debug.Log("Found " + colls.Length + " colliders");
                bool foundJoint = false;
                foreach(Collider c in colls)
                {
                    WireJoint wj = c.GetComponent<WireJoint>();
                    if(wj != null)
                    {
                        WireSegment ws = wj.GetComponentInParent<Wire>().ExtendAtJoint(wj);
                        currentJoint = ws.endpoint;
                        currentJoint.Weld(manipulationPoint);
                        foundJoint = true;
                        break;
                    }
                    
                }
                if (!foundJoint)
                {
                    Wire wire = (Instantiate(wirePrefab, manipulationPoint.position, Quaternion.identity, null) as GameObject).GetComponent<Wire>();
                    WireSegment newSegment = wire.CreateSegmentFresh(manipulationPoint.position);
                    currentJoint = newSegment.endpoint;
                    currentJoint.Weld(manipulationPoint);
                }
                
            }
            else
            {
                Debug.Log("Current joint detected");
                currentJoint.UnWeld();
                currentJoint = null;
            }
        }
        else
        {
            Debug.Log("Wrong input");
        }
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
