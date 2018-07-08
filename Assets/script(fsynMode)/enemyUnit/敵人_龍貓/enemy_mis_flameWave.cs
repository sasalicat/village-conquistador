using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_mis_flameWave : Missile {
    public const float MAX_DISTANCE = 30;
    float disLeft = MAX_DISTANCE;
    public int Buffno;//击中后会添加的buff编号
    // Use this for initialization
    void Start()
    {
        Speed = Missile.ENEMY_STAND_FLY_SPEED * 0.7f;//標準速度的70%
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, Speed * Time.deltaTime, 0);
        if (disLeft <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("other:" + other.gameObject.name);
        RoleState role = other.gameObject.GetComponent<RoleState>();
        Debug.Log("造成傷害之前 role:" + role + "creater:" + Creater);
        if (role!=null&&role.team != Creater.GetComponent<RoleState>().team)
        {
            role.TakeDamage(Damage);
            if (role.tag == "Player")
            {
                other.GetComponent<Controler>().addBuffByNo((sbyte)Buffno);
            }
        }
    }
}
