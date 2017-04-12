using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleTask : MonoBehaviour {
    on_attack task;
	// Use this for initialization
	void Start () {
        Debug.Log(task);
        task += m;
        task += n;
        Debug.Log(task);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void m()
    {
        Debug.Log("m");
    }
    void n()
    {
        Debug.Log("n");
    }
}
