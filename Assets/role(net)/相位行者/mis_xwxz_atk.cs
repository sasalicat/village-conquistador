using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_xwxz_atk : Missile
{

    public float m_liveTime = 0.5f;    // Use this for initialization
    void Start()
    {

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
        if (other.gameObject != Creater)
        {
            RoleState role = other.gameObject.GetComponent<RoleState>();
                role.TakeDamage(Damage);

        }
    }
}
