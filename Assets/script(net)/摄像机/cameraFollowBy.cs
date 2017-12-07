using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    public const float FOLLOW_CRICLE = 0.2f;
    public float timeLeft = FOLLOW_CRICLE;
	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        if (mainRole != null)
        {
            Vector3 newV= new Vector3(roleTrans.position.x,roleTrans.position.y, CAMERA_Z);
            
            transform.position = newV;
            //0.5秒為週期的平滑化
            //transform.DOMove(new Vector3(roleTrans.position.x, roleTrans.position.y, CAMERA_Z), FOLLOW_CRICLE);
            //timeLeft = FOLLOW_CRICLE;
        }
	}
    void FixedUpdate()
    {

    }
}
