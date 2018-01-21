using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireSegment : MonoBehaviour {

	List<Transform> currentSegments = new List<Transform>();
	private Transform startpoint;
	public Transform endpoint;
	public Transform model;
	public GameObject attachPointPrefab;
	public Transform container;
	public Node associatedNode;
	// Use this for initialization
	void Start () {
		startpoint = transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vec = endpoint.position - startpoint.position;
		model.localScale = new Vector3(0.02f, vec.magnitude * 0.5f, 0.02f);
		container.position = startpoint.position + 0.5f * vec;
		container.rotation = Quaternion.LookRotation(vec);
		if(Input.GetKeyDown(KeyCode.Space)){
			Attach(endpoint.position);
		}
	}

	void Attach(Vector3 point){ //Attaches to point in worldspace without parenting to larger wire
		endpoint.position = point;
		Instantiate(attachPointPrefab, endpoint.position, container.rotation, container);
	}

	void Attach(Vector3 point, Wire wire){ //Attach to point in worldspace and become child of larger wire, called from tool script
		this.transform.SetParent(wire.transform);
		Attach(point);
		wire.RefreshNodes();
	}
}
