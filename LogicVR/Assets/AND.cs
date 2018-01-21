using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
public class AND : Gate, Tickable {

	// Use this for initialization
	void Start () {
		Node[] childNodes = transform.GetComponentsInChildren<Node>();
		Debug.Log("AND gate found " + childNodes.Length + " child nodes");
		foreach(Node n in childNodes){
			nodes.Add(n);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void Tick(){
		ushort bit = 1;
		foreach(Node n in nodes){
			if(n.GetNodeType()==NodeIdentifier.Input){
				if(n.GetCurrentBit() == 0){
					bit = 0;
				}
			}
		}
		foreach(Node n in nodes){
			if(n.GetNodeType()==NodeIdentifier.Output){
				n.SetBit(bit);
			}
		}

	}
}
