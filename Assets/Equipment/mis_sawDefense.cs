using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_sawDefense : MonoBehaviour,Missile {
    private GameObject creater;
    private float speed;
    private damage damage;
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
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
