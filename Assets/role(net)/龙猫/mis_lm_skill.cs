﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_lm_skill : Missile
{
    public float hy_liveTime = 0.1f;
    public float m_liveTime = 5;
    public MissileTable missletable;
    // Use this for initialization
    void Start()
    {
        this.transform.eulerAngles = new Vector3(this.transform.rotation.x, 180, 0);
        Random.seed = 5;
        GameObject keytable = GameObject.Find("keyTabel");
        missletable = keytable.GetComponent(typeof(MissileTable)) as MissileTable;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 weizhi = this.transform.position;
        hy_liveTime -= Time.deltaTime;
        if (hy_liveTime <= 0) {
            hy_liveTime = 0.1f;
            GameObject newone = Instantiate(missletable.MissileList[15], Random.insideUnitCircle * 12 + weizhi, this.transform.rotation);
            Missile missile = newone.GetComponent<Missile>();
            missile.Creater = this.Creater;
            missile.Damage = this.Damage;
        }
        
        m_liveTime -= Time.deltaTime;
        if (m_liveTime <= 0)
        {
            Destroy(this.gameObject);
        }

    }
}