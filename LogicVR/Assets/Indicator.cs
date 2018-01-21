using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour, Tickable {
	public Node input;
	Material mat;
	// Use this for initialization
	void Start () {
		Node n = transform.GetComponentInChildren<Node>();
		if(n != null){
			input = n;
		}
		MeshRenderer m = transform.GetComponentInChildren<MeshRenderer>();
		if(m != null){
			mat = m.material;
		}
	}
	
	public void Tick(){
		if(input.GetCurrentBit() == 1){
			mat.color = Color.green;
		}else{
			mat.color = Color.white;
		}
	}
}
