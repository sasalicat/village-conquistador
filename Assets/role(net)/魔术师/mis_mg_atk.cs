using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_mg_atk :Missile{
    private Vector3 vspeed;
    public float livetime = 1;
    public Animator animator;
    public bool xuanzhuan;
   
    void Start()
    {
        animator = this.GetComponent<Animator>();
        vspeed = new Vector3(0, -Speed, 0);
    }
    void Update()
    {
        livetime -= Time.deltaTime;
        if (livetime > 0)
        {
            transform.Translate(vspeed * Time.deltaTime);
        }
        if(livetime <= 0)
        {
            animator.SetBool("xuanzhuan", true);
        }
        if(livetime <= -5)
        {
            
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject!=Creater) {
            Debug.Log(other.name);
            Debug.Log(" on trigger enter 2D");
            RoleState role = other.gameObject.GetComponent<RoleState>();
            if (role != null)
            {
                role.TakeDamage(Damage);
                Destroy(this.gameObject);
            }
            
        }
    }
}
