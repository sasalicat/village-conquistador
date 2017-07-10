using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_aaaa : Missile {
    private Vector3 vspeed;
    // Use this for initialization
    void Start () {
        vspeed = new Vector3(0, Speed, 0);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(vspeed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != Creater)
        {
            RoleState role = other.gameObject.GetComponent<RoleState>();
            if (role != null)
                role.TakeDamage(Damage);
        }
    }

}
