using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
public class ClickTest : MonoBehaviour, IInputClickHandler{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void IInputClickHandler.OnInputClicked(InputClickedEventData eventData)
    {
        Debug.LogWarning(eventData.PressType);
    }
}
