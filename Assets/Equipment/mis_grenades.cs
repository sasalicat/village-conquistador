using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_grenades : Missile
{
    private Vector3 vspeed;
    private Animator anim;

    // Use this for initialization
    void Start()
    {

        vspeed = new Vector3(0, Speed, 0);
    }
    void Update()
    {
        transform.Translate(vspeed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        RoleState role = other.gameObject.GetComponent<RoleState>();
        if (role != null)
            role.TakeDamage(Damage);

        anim = GetComponent<Animator>();
        anim.SetBool("boom", true);
    }

    public void getAnimation()
    {
        Debug.Log("12312312323");
        Destroy(gameObject);
    }
}
