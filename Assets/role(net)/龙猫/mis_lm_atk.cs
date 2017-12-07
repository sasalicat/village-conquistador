using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_lm_atk : Missile
{
    public double jishiqi = 1f;
    // Use this for initialization
    void Start()
    {
        Speed = STAND_FLY_SPEED*0.7f;
    }

    // Update is called once per frame
    void Update()
    {
        jishiqi -= Time.deltaTime;
        if (jishiqi <= 0)
        {
            Destroy(this.gameObject);
        }
        transform.Translate(0, Speed * Time.deltaTime, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != Creater)
        {
            //Debug.Log(other.name);
            //Debug.Log(" on trigger enter 2D");
            RoleState role = other.gameObject.GetComponent<RoleState>();
            RoleState rolestate = Creater.GetComponent(typeof(RoleState)) as RoleState;
            if (role != null && rolestate.team != role.team)
            {
                role.TakeDamage(Damage);
                Destroy(this.gameObject);

            }

        }
    }
}