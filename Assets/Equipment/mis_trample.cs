using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_trample : Missile
{
    public const float MAX_DISTANCE = 40;
    float disLeft = MAX_DISTANCE;
    unit u;
    public float conti = 0.5f;
    
    // Update is called once per frame
    void Update()
    {
        conti -= Time.deltaTime;
        if (conti <= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        RoleState role = other.gameObject.GetComponent<RoleState>();
        if (role.tag=="Player" && role.team != Creater.GetComponent<RoleState>().team)
        {
          role.TakeDamage(Damage);


        }
    }
}

