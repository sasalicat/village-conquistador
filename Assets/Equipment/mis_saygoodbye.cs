using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_saygoodbye : Missile
{
    float timer_f = 0f;
    int timer_i = 0;

    private Vector3 vspeed;
    private Animator anim;
    public Vector3 origenPlayerPosition;
    public Vector3 mousePosition;
    public bool boom = false;
    public GameObject missilePraf2;
    GameObject newone;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        timer_f += Time.deltaTime;
        timer_i = (int)timer_f;
        Debug.Log(timer_i + "秒");
        if (timer_i == 1)
        {
            getSkill2(origenPlayerPosition);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        boom = anim.GetBool("boom");
        if (boom == true)
        {
            RoleState role = other.gameObject.GetComponent<RoleState>();
            if (role != null)
                role.TakeDamage(Damage);
            Debug.Log("Boom actity");
        }
    }

    void getSkill2(Vector3 destination)
    {
        newone = Instantiate(missilePraf2, destination, transform.rotation);
        Missile missile = newone.GetComponent<Missile>();
        missile.Creater = this.Creater;
        missile.Damage = this.Damage;
        newone.transform.localScale = new Vector3(2, 2, 1);
        Destroy(gameObject);
    }

    public void getAnimation()
    {
        Destroy(newone);
    }
}

