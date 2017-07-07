﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile:MonoBehaviour  {
    private GameObject creater;
    private damage damage;
    private float speed = 20;

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
}
