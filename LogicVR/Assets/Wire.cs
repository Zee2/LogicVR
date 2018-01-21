using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
public class Wire : Gate, Tickable {
	
	public List<GameObject> nodeObjects;

	public bool shouldReacquireNodes = false;
	// Use this for initialization
	void Start () {
		RefreshNodes();
	}
	
	public void RefreshNodes(){ //Should be called whenever any segment is changed (attached, removed, etc)
		nodes.Clear();
		nodeObjects.Clear();
		WireSegment[] segments = GetComponentsInChildren<WireSegment>();
		foreach(WireSegment segment in segments){
			if(segment.associatedNode != null){
				nodes.Add(segment.associatedNode);
			}
		}
		foreach(GameObject g in nodeObjects){
			nodes.AddRange(g.GetComponentsInChildren<Node>());
		}
		
	}

	public override void Tick(){
		if(shouldReacquireNodes){
			RefreshNodes();
			shouldReacquireNodes = false;
		}


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
