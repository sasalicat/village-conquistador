﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obs_lr_skill : ObstacleState
{
    public Animator animator;

    public SpriteRenderer s;
    public bool local;
    public damage damage;
    public double jishiqi = 0.5f;
    public int qibiao = 1;
    override protected void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        s = gameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        if (local = Creater.GetComponent<NetRoleState>().islocal)
        {
            s.color = new Vector4(1, 1, 1, 0.5f);
        }
    }

    void Update()
    {
        base.Update();
        jishiqi -= Time.deltaTime;
        if(!local && jishiqi <= 0 && qibiao == 1)
        {
            s.color = new Vector4(1, 1, 1, 0);
        }
        if(jishiqi <= -14.5)
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != Creater)
        {
            qibiao = 2;
            callMethodNull();
            ChangeColor();
            RoleState rolestate = Creater.GetComponent(typeof(RoleState)) as RoleState;
            RoleState role = other.gameObject.GetComponent<RoleState>();
            if (rolestate.team != role.team)
            {
                role.TakeDamage(damage);
                other.transform.GetComponent<Controler>().addBuffByNo(5);
            }

        }
    }
    public override void methodNull()
    {
        animator.SetBool("attack", true);
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public void ChangeColor()
    {
        s.color = new Vector4(1, 1, 1, 1);
    }

}