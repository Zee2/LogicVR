using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class WireSegment : MonoBehaviour, ISleepable {
    
	public WireJoint startpoint;
	public WireJoint endpoint;
	public Transform model;
	public Transform container;
    public Transform backupStart;
    public Transform backupEnd;

    public AnimationCurve spawn;
    public AnimationCurve collapseCurve;
    AnimationCurve currentCurve;

    public float smoothTime = 0.001f;

    Vector3 vec = new Vector3();
    Vector3 vel1 = new Vector3();
    Vector3 vel2 = new Vector3();


    bool shouldCollapse = false;
    bool shouldSpawn = true;
    float collapseTime = 0;
    float spawnTime = 0;
    Vector3 collapsePoint = new Vector3();

    Vector3 adjustedStart = new Vector3();
    Vector3 adjustedEnd = new Vector3();
	// Use this for initialization
	void Start () {
        spawnTime = Time.time;
	}


    void ISleepable.Sleep()
    {
        enabled = false;
    }
    void ISleepable.Wake()
    {
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (shouldSpawn)
        {
            adjustedStart = startpoint.transform.position;
            adjustedEnd = Vector3.Lerp(startpoint.transform.position, endpoint.transform.position, spawn.Evaluate(Time.time - spawnTime));
        }

        if (shouldCollapse)
        {
            adjustedStart = Vector3.Lerp(startpoint.transform.position, collapsePoint, collapseCurve.Evaluate(Time.time - collapseTime));
            adjustedEnd = Vector3.Lerp(endpoint.transform.position, collapsePoint, collapseCurve.Evaluate(Time.time - collapseTime));
        }

        vec = adjustedEnd - adjustedStart;
        model.localScale = new Vector3(0.02f, vec.magnitude * 0.5f, 0.02f);
        container.position = adjustedStart + 0.5f * vec;
        if(vec.magnitude > 0.001f)
            container.rotation = Quaternion.LookRotation(vec);




    }

    public void Collapse(Vector3 point)
    {
        shouldCollapse = true;
        collapseTime = Time.time;
        
        
        collapsePoint = point;
    }
}
