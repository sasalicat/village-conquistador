using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_nq_atk : Missile
{

    public float m_liveTime = 0.3f;

    public RoleState rolestate;
    // Use this for initialization
    void Start()
    {
        
        rolestate = Creater.GetComponent(typeof(RoleState)) as RoleState;

    }

    // Update is called once per frame
    void Update()
    {
        m_liveTime -= Time.deltaTime;
        if (m_liveTime <= 0)
        {
            Destroy(this.gameObject);
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        RoleState role = other.gameObject.GetComponent<RoleState>();
        if (rolestate.team == role.team)
        {
                role.BeenTreat(Creater, Damage.num);

        }
    }
}

