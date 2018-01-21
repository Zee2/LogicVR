using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class Node : MonoBehaviour, Tickable {

	List<Node> connectedNodes = new List<Node>();

	public NodeIdentifier type;
	ushort lastBit;
	public ushort currentBit;

	public NodeIdentifier GetNodeType(){
		return type;
	}

	public ushort GetLastBit(){ //useful for rising/falling edge detectino
		return lastBit;
	}

	public ushort GetCurrentBit(){
		return currentBit;
	}

	public void SetBit(ushort bit){
		lastBit = currentBit;
		currentBit = bit;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Tick(){
		foreach(Node n in connectedNodes){
			n.SetBit(currentBit);
		}
	}
	
}
