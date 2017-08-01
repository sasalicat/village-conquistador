using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_tk_atk : Missile {
    public double jishiqi = 0.4f;
	// Use this for initialization
	void Start () {
        Speed = 3;
	}
	
	// Update is called once per frame
	void Update () {
        jishiqi -= Time.deltaTime;
        if(jishiqi <= 0)
        {
            Destroy(this.gameObject);
        }
        transform.Translate(0, -Speed * Time.deltaTime, 0);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != Creater)
        {
            //Debug.Log(other.name);
            //Debug.Log(" on trigger enter 2D");
            RoleState role = other.gameObject.GetComponent<RoleState>();
            if (role != null)
            {
                role.TakeDamage(Damage);
            }

        }
    }
}
