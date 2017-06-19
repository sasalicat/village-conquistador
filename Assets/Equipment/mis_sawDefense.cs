using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_sawDefense : MonoBehaviour, Missile
{
    private GameObject creater;
    private float speed;
    private damage damage;
    private float existTime = 0;
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

    // Use this for initialization
    void Start()
    {
        transform.parent = creater.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (existTime > 0.75f)
        {
            Destroy(gameObject);
        }
        existTime += Time.deltaTime;//之后要改成在manger通知

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != creater)
        {
            RoleState role = other.gameObject.GetComponent<RoleState>();
            if(role!=null)
                role.TakeDamage(Damage);
        }
    }
}