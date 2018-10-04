using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followGameObject : MonoBehaviour {
    public Vector3 offset = Vector3.zero;
    public GameObject master = null;
	// Use this for initialization

	// Update is called once per frame
	void Update () {
        if (master != null)
        {
            transform.position = master.transform.position + offset;
        }
	}
}
