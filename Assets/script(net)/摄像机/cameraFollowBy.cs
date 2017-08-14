using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollowBy : MonoBehaviour {
    public GameObject MainRole
    {
        set
        {
            mainRole = value;
            roleTrans = mainRole.transform;
        }
    }
    private GameObject mainRole;
    private Transform roleTrans;
    public const float CAMERA_Z = 33;
	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
      
	}
    void FixedUpdate()
    {
        if (mainRole != null)
        {

            transform.position = new Vector3(roleTrans.position.x, roleTrans.position.y, CAMERA_Z);
        }
    }
}
