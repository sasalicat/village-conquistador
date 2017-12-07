using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_spear : Missile {
    public const float MAX_DISTANCE = 40;
    float disLeft = MAX_DISTANCE;
	// Use this for initialization
	void Start () {
        Speed = STAND_FLY_SPEED*0.4f;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, Speed * Time.deltaTime, 0, Space.Self);
        disLeft -= Speed * Time.deltaTime;
        if (disLeft <= 0)
        {
            Destroy(this.gameObject);
        }
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        RoleState role = other.gameObject.GetComponent<RoleState>();
        if (role.team != Creater.GetComponent<RoleState>().team) 
        {
            Debug.Log(other.name);
            Debug.Log(" on trigger enter 2D");
            

                
                
                Damage.num = (int)(10 + (MAX_DISTANCE-disLeft) * 5);
                Damage.stiffTime = ((MAX_DISTANCE - disLeft) / MAX_DISTANCE) * 2.5f;
                role.TakeDamage(Damage);
                Destroy(this.gameObject);
            

        }
    }
}
