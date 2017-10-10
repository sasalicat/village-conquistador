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
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RoleState role = other.GetComponent<RoleState>();
            if (beusedCB != null)
            {
                beusedCB(this);
            }
            //other.GetComponent<Controler>().addBuffByNo(7);
        }
    }
}
