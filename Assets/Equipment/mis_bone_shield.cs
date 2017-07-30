using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_bone_shield : Missile
{
    private Vector3 vspeed;
    public const int limitDistance = 15;

    public Vector3 origenPlayerPosition;
    public Vector3 mousePosition;
    public bool boom = false;
    public GameObject missilePraf2;
    GameObject newone;

    // Use this for initialization
    void Start()
    {
        
    }

    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
