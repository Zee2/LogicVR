using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
public class Wire : Gate, Tickable {


    public GameObject segmentPrefab;
    public GameObject jointPrefab;
    
    public HashSet<WireSegment> segments = new HashSet<WireSegment>();
    public HashSet<WireJoint> joints = new HashSet<WireJoint>();
	// Use this for initialization
	void Start () {
	}
	
	public WireSegment ExtendAtJoint(WireJoint j)
    {
        if (joints.Contains(j))
        {
            WireSegment w = CreateSegment(j, null);
            return w;
        }
        else
        {
            Debug.LogError("ExtendAtJoint: unknown joint");
            return null;
        }
    }
    
    WireSegment CreateSegment(WireJoint j1, WireJoint j2)
    {
        WireSegment w = (Instantiate(segmentPrefab, transform) as GameObject).GetComponent<WireSegment>();
        if(j1 == null)
        {
            j1 = (Instantiate(jointPrefab, transform) as GameObject).GetComponent<WireJoint>();
            if(j2 != null)
            {
                j1.transform.position = j2.transform.position;
            }
        }
        if (j2 == null)
        {
            j2 = (Instantiate(jointPrefab, transform) as GameObject).GetComponent<WireJoint>();
            if (j1 != null)
            {
                j2.transform.position = j1.transform.position;
            }
        }
        joints.Add(j1); //HashSet.Add() automatically checks for duplicates
        joints.Add(j2);
        w.startpoint = j1;
        w.endpoint = j2;
        segments.Add(w);
        return w;
    }

    

    public WireSegment CreateSegmentFresh(Vector3 point)
    {
        if(segments.Count > 0)
        {
            return null;
        }

        WireSegment ws = CreateSegment(null, null);
        ws.startpoint.transform.position = point;
        ws.endpoint.transform.position = point;
        return ws;
    }

    public WireSegment ExtendAtPoint(Vector3 point, WireSegment segment) //Slices existing segment into two, and extrudes from the new joint
    {
        if (segments.Contains(segment))
        {
            WireJoint j1 = segment.startpoint.gameObject.GetComponent<WireJoint>();
            if(j1 == null)
            {
                Debug.LogError("ExtendAtPoint: startpoint retrieval error");
            }
            WireJoint j2 = segment.endpoint.gameObject.GetComponent<WireJoint>();
            if (j2 == null)
            {
                Debug.LogError("ExtendAtPoint: endpoint retrieval error");
            }
            segments.Remove(segment);
            segment.smoothTime = 0.2f;
            segment.Collapse(point);
            Destroy(segment.gameObject, 1f);
            
            WireSegment wire1 = ExtendAtJoint(j1); //Creates new joint as endpoint for wire1
            wire1.smoothTime = 0.8f;
            wire1.endpoint.transform.position = point;
            WireSegment wire2 = CreateSegment(j2, wire1.endpoint); //Creates new segment connecting 
            wire2.smoothTime = 0.8f;
            
            

            return ExtendAtJoint(wire1.endpoint);
        }
        else
        {
            return null;
        }
    }

    public WireSegment ExtrudeFromNode(Node n)
    {
        WireSegment ws = CreateSegmentFresh(n.transform.position);
        return ws;
    }

    public void ConnectToNode(Node n)
    {
        nodes.Add(n); //Doesn't necessarily add the physical indication of connecting.... Use
    }


    //Takes existing joint and welds it to the 
    public void ConnectToNode(WireJoint joint, Transform weldTarget, Node n)
    {
        joint.Weld(weldTarget);
        ConnectToNode(n);
    }

	public override void Tick(){
		
		ushort bit = 0;
		foreach(Node n in nodes){
			if(n.GetNodeType() == NodeIdentifier.Output){
				if(n.GetCurrentBit() == 1){
					bit = 1;
				}
			}
		}

		foreach(Node n in nodes){
			if(n.GetNodeType() == NodeIdentifier.Input){
				n.SetBit(bit);
			}
		}
	}
}


