using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_tjs_atk : Missile{

    public float m_liveTime;
    public float juli;
    public float zhuangtai;
    void Start()
    {

    }
    void Update()
    {
        Vector3 fei = this.transform.position;
        Vector3 zhujiao = Creater.transform.position;
        Vector3 direction = fei - zhujiao;
        //juli = (float)System.Math.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.z, 2));
        /*if (zhuangtai == 1)
        {
            Speed = m_liveTime * 15;
            m_liveTime -= Time.deltaTime;

            transform.Translate(0, Speed * Time.deltaTime, 0);
        }

        if (m_liveTime <= 0)
        {
            zhuangtai = 2;
        }

        if (zhuangtai == 2)
        {
            Speed = Speed + 1;
            this.transform.forward = -direction;
            this.transform.eulerAngles = new Vector3(0, 0, this.transform.eulerAngles.z);
            transform.Translate(0, Speed * Time.deltaTime, 0, Space.Self);
        }*/
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != Creater)
        {
            //Debug.Log(other.name);
            //Debug.Log(" on trigger enter 2D");
            RoleState role = other.gameObject.GetComponent<RoleState>();
            if (role != null)
                role.TakeDamage(Damage);

        }
    }
}
