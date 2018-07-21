using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_fsyn_tk_skill : addibleMissile {

	// Use this for initialization
	void Start () {
        canBeRefected = false;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("觸發了盾牌的onTriggerEnter2D");
        var mis = other.transform.GetComponent<addibleMissile>();
        if (mis != null)
        {
            //Debug.Log("自身的Creater為" + Creater);
            mis.BeReflected(-other.transform.up, Creater);
        }
    }
}
