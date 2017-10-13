using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yao : MonoBehaviour,supply {
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
	void Update () {
        transform.eulerAngles += new Vector3(0, 0, 90 * Time.deltaTime);
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RoleState role = other.GetComponent<RoleState>();
            other.GetComponent<Controler>().addBuffByNo(8);
            if (beusedCB != null&&other.GetComponent<NetPlayerControler>())
            {
                beusedCB(this);
            }
            //other.GetComponent<Controler>().addBuffByNo(7);
        }
    }
}
