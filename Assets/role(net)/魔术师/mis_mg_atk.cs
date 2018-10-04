using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_mg_atk :addibleMissile{
    private Vector3 vspeed;
    public float livetime = 0.5f;
    public Animator animator;
    public bool xuanzhuan;
   
    void Start()
    {
        animator = this.GetComponent<Animator>();
        vspeed = new Vector3(0, -Speed, 0);
        Timer.main.logInTimer(onMisUpdate);
    }
    public void onMisUpdate(float time)
    {
        //Debug.Log("up is:"+transform.up);
        //Debug.Log("onMisUpdate 被呼叫time"+time);
        livetime -= time;
        if (livetime > 0)
        {
            transform.Translate(vspeed * time);
        }
        if (livetime <= 0)
        {
            animator.SetBool("xuanzhuan", true);
        }
        if (livetime <= -8)
        {
            Destroy(this.gameObject);
        }

    }
    protected void OnDestroy()
    {
        Timer.main.loginOutTimer(onMisUpdate);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject!=Creater) {
            Debug.Log(other.name);
            Debug.Log(" on trigger enter 2D");
            if (onHit!=null)
            {
                onHit(other.gameObject,this);
            }
            RoleState role = other.gameObject.GetComponent<RoleState>();
            RoleState rolestate = Creater.GetComponent(typeof(RoleState)) as RoleState;
            if (role != null && rolestate.team != role.team)
            {
                role.TakeDamage(Damage);
                Destroy(this.gameObject);
            }
            
        }
    }
}
