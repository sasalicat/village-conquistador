using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_anubalake : Missile
{
    private Vector3 vspeed;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        RoleState role = other.gameObject.GetComponent<RoleState>();
        if (role != null)
            role.TakeDamage(Damage);

    }

    public void getAnimation()
    {
        Destroy(gameObject);
    }
}
