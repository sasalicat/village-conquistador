﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_bx_skill : Missile
{

    public float m_liveTime = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
            RoleState rolestate = Creater.GetComponent(typeof(RoleState)) as RoleState;
                if (role != null && other.tag.CompareTo("Player") == 0 && rolestate.team != role.team)
            {
                other.GetComponent<Controler>().distortionByNo(1);

            }

        }
    }
}