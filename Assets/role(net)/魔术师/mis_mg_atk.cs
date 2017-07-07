using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_mg_atk :Missile{
     private Vector3 vspeed;
   
    void Start()
    {
            
           vspeed = new Vector3(0, -Speed, 0);
    }
    void Update()
    {
        transform.Translate(vspeed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject!=Creater) {
            Debug.Log(other.name);
            Debug.Log(" on trigger enter 2D");
            RoleState role = other.gameObject.GetComponent<RoleState>();
            if(role!=null)
                role.TakeDamage(Damage);
            
        }
    }
}
