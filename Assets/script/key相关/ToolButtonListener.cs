using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolButtonListener : MonoBehaviour {
    public KeyRegister keys;
 
	// Use this for initialization
	protected void Start () {
        keys = GameObject.Find("keyTabel").GetComponent<KeyRegister>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
