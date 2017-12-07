using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile:MonoBehaviour  {
    private GameObject creater;
    private damage damage;

    public const int STAND_FLY_SPEED=40;
    private float speed = STAND_FLY_SPEED;
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
