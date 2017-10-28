﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_kniftShot : Missile
{

    public float m_liveTime = 2;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(-Speed * Time.deltaTime,0 , 0, Space.Self);
        m_liveTime -= Time.deltaTime;
        if (m_liveTime <= 0)
        {
            Destroy(this.gameObject);
        }

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
                Destroy(this.gameObject);
            }

        }
    }
}