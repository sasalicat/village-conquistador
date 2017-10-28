using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_lr_atk : Missile {

    public float m_liveTime = 2;
    public float juli;
    // Use this for initialization
    void Start () {
        Speed = 40;

}
	
	// Update is called once per frame
	void Update () {
        
        transform.Translate(0, -Speed * Time.deltaTime, 0, Space.Self);
        m_liveTime -= Time.deltaTime;
        if(m_liveTime <= 0)
        {
            Destroy(this.gameObject);
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != Creater)
        {
            Debug.Log(other.name);
            Debug.Log(" on trigger enter 2D");
            RoleState role = other.gameObject.GetComponent<RoleState>();
            RoleState rolestate = Creater.GetComponent(typeof(RoleState)) as RoleState;
            if (role != null && rolestate.team != role.team)
            {
                Vector3 fei = this.transform.position;
                Vector3 zhujiao = Creater.transform.position;
                Vector3 direction = fei - zhujiao;
                juli = direction.magnitude;
                unit u = Creater.GetComponent<unit>();
                Damage.num = (int)(Attribute.GetAttackDamageNum((int)(20 + juli * 3), u.power));
                if (Damage.num >= 130)
                    Damage.num = 130;
                role.TakeDamage(Damage);
                Destroy(this.gameObject);
            }

        }
    }
}
