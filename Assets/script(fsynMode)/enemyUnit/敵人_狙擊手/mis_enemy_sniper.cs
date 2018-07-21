using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_enemy_sniper : addibleMissile {
    private Vector3 vspeed;
    public float livetime = 10f;
    public Animator animator;
    public bool xuanzhuan;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        vspeed = new Vector3(0, Speed, 0);
        Timer.main.logInTimer(onMisUpdate);
        Debug.Log("子彈的創造者為" + Creater);
    }
    public void onMisUpdate(float time)
    {
        livetime -= time;
        if (livetime <= time)
        {
            transform.Translate(vspeed * livetime);
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(vspeed * time);
        }

    }
    protected void OnDestroy()
    {
        Timer.main.loginOutTimer(onMisUpdate);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != Creater)
        {
            Debug.Log(other.name);
            Debug.Log(" on trigger enter 2D "+Creater);
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
