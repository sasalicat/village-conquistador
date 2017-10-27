using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_sawAttack : Missile
{

    private float existTime = 0;



    // Use this for initialization
    void Start()
    {
        transform.parent = Creater.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (existTime > 8f)
        {
            Destroy(gameObject);
        }
        existTime += Time.deltaTime;//之后要改成在manger通知

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
         RoleState role = other.gameObject.GetComponent<RoleState>();
         if(role.team != Creater.GetComponent<RoleState>().team)
        {
                if (role != null)
                    role.TakeDamage(Damage);
            }
        }
    }
}
