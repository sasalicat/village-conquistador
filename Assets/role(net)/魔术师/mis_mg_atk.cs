﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_mg_atk : MonoBehaviour,Missile {
    GameObject creater;
    damage damage;
    private Vector3 vspeed;
    float speed=5;

    public GameObject Creater
    {
        get
        {
            return creater;
        }

        set
        {
            creater = value;
        }
    }

    public damage Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }
    void Start()
    {
           vspeed = new Vector3(0, speed, 0);
    }
    void Update()
    {
        transform.Translate(vspeed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(" on trigger enter 2D");
        RoleState role= other.gameObject.GetComponent<RoleState>();
        role.TakeDamage(Damage);
        Debug.Log("over"+role);
    }
}
