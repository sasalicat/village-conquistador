using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseAddMissile : addibleMissile {
    public Vector3 vspeed;
    public float livetime = 0.5f;
    //public Animator animator;
   // public bool xuanzhuan;

    public virtual void Start()
    {
        //animator = this.GetComponent<Animator>();
        vspeed *= Speed;
        Timer.main.logInTimer(onMisUpdate);
    }
    public virtual void onMisUpdate(float time)
    {
        //Debug.Log("up is:"+transform.up);
        //Debug.Log("onMisUpdate 被呼叫time"+time);
        livetime -= time;
        if (livetime > 0)
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
            Debug.Log(" on trigger enter 2D");
            if (onHit != null)
            {
                onHit(other.gameObject, this);
            }
            RoleState role = other.gameObject.GetComponent<RoleState>();
            RoleState rolestate = Creater.GetComponent(typeof(RoleState)) as RoleState;
            Controler hitter=role.GetComponent<Controler>();
            if (role != null && rolestate.team != role.team)
            {
                role.TakeDamage(Damage);
                AftHit(hitter);
                Destroy(this.gameObject);
            }

        }
    }
    protected virtual void AftHit(Controler hitter)
    {

    }
}
