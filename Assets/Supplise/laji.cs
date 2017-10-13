using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laji : MonoBehaviour,supply {
    beused usedit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles += new Vector3(0, 0, 90 * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RoleState role = other.GetComponent<RoleState>();
            role.BeenTreat(null, (int)(role.maxHp*0.1f));
            other.GetComponent<Controler>().addBuffByNo(7);
            if (beusedCB != null && other.GetComponent<NetPlayerControler>())
            {
                beusedCB(this);
            }
            //other.GetComponent<Controler>().addBuffByNo(7);
        }
    }

    public beused beusedCB
    {
        set
        {
            usedit = value;
        }
        get
        {
            return usedit;
        }
    }
}
