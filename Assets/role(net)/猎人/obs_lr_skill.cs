using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obs_lr_skill : ObstacleState
{
    public SpriteRenderer s;
    public bool local;
    public damage damage;
    public double jishiqi = 0.5f;
    override protected void Start()
    {
        base.Start();
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
        if(!local && jishiqi <= 0)
        {
            s.color = new Vector4(1, 1, 1, 0);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(Creater+ ";" +other.gameObject);
        if (other.gameObject != Creater)
        {
            callMethodNull();
            RoleState role = other.gameObject.GetComponent<RoleState>();
            role.TakeDamage(damage);
        }
    }
    public override void methodNull()
    {
        Destroy(this.gameObject);
    }

}