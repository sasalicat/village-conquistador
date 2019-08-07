using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_flameWave : Missile
{
    public const float MAX_DISTANCE = 30;
    float disLeft = MAX_DISTANCE;
    public int Buffno;//击中后会添加的buff编号
    // Use this for initialization
    void Start()
    {
        Speed = Missile.STAND_FLY_SPEED * 0.7f;//標準速度的70%
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
        RoleState role = other.gameObject.GetComponent<RoleState>();
        Debug.LogWarning("name:" + other.name + "role:" + role + " creater name:"+Creater+"creater:"+ Creater.GetComponent<RoleState>());
        if (role.team != Creater.GetComponent<RoleState>().team)
        {
            role.TakeDamage(Damage);
            if (role.tag == "Player")
            {
                other.GetComponent<Controler>().addBuffByNo((sbyte)Buffno);
            }
        }
    }
}
