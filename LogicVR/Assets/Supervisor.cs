using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supervisor : MonoBehaviour {

	public float tickInterval = 0.05f;
	public bool shouldTick = true;
	public List<Tickable> tickables = new List<Tickable>();
	// Use this for initialization
	void Start () {
		Debug.Log("Supervisor startup");
		tickables.AddRange(FindObjectsOfType(typeof(Indicator)) as Tickable[]);
		tickables.AddRange(FindObjectsOfType(typeof(Node)) as Tickable[]);
		tickables.AddRange(FindObjectsOfType(typeof(Gate)) as Tickable[]);
		Debug.Log(tickables.Count + " tickables found");
		StartCoroutine(TickScheduler());
	}

	void Update(){
	}
	
	IEnumerator TickScheduler(){
		while(true){
			yield return new WaitForSeconds(tickInterval);
			if(shouldTick){
				foreach(Tickable t in tickables){
					t.Tick();
				}
			}
		}
	}
}
