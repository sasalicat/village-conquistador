using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hyy2_atk : Missile{

    public float jishiqi = 0.5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        jishiqi -= Time.deltaTime;
        if(jishiqi <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != Creater && this.transform.position.z <= 0)
        {
            RoleState role = other.gameObject.GetComponent<RoleState>();
            role.TakeDamage(Damage);

        }
    }
}
