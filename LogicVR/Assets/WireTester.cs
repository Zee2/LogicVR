using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireTester : MonoBehaviour {

    public GameObject wirePrefab;
    public Wire wire;
    WireSegment currentSegment;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.A))
        {
            wire = (Instantiate(wirePrefab) as GameObject).GetComponent<Wire>();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentSegment = wire.CreateSegmentFresh(transform.position);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentSegment = wire.ExtendAtJoint(currentSegment.endpoint);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            currentSegment = null;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 0.05f);
            WireSegment ws = null;
            foreach(Collider c in colliders)
            {
                if(ws == null)
                {
                    ws = c.GetComponentInParent<WireSegment>();
                }
            }

            if(ws != null)
            {
                currentSegment = ws.GetComponentInParent<Wire>().ExtendAtPoint(transform.position, ws);
            }
        }

        if(currentSegment != null)
        {
            currentSegment.endpoint.transform.position = transform.position;
        }
	}
}
