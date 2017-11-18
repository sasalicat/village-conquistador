using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugButtom : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Transform debug= transform.Find("DebugLabel");
            debug.gameObject.SetActive(!debug.gameObject.activeSelf);
        }
	}
}
