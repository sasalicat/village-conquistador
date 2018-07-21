using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackSpeedTest : MonoBehaviour {
    public Animator anim;
	// Use this for initialization
	void Start () {
        anim.SetFloat("attackSpeed", 0.35f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
