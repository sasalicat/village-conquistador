using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xianyu : MonoBehaviour,supply {
    beused usedit;
    public beused beusedCB
    {
        get
        {
            return usedit;
        }

        set
        {
            usedit = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, 0, 90 * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("鹹魚的觸發!!!");
        if (other.tag == "Player")
        {
            RoleState role = other.GetComponent<RoleState>();
            role.recoverMP(25);
            if (beusedCB != null && other.GetComponent<NetPlayerControler>())
            {
                beusedCB(this);
            }
            //other.GetComponent<Controler>().addBuffByNo(7);
        }
    }
}
