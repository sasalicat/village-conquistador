using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hyy_atk : Missile
{

    public float m_liveTime = 0.5f;
    // Use this for initialization
    void Start()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 30);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z-60*Time.deltaTime);
        m_liveTime -= Time.deltaTime;
        if(this.transform.position.z <= -1)
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
