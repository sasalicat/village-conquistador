using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_enemy_ls_atk : Missile {
    public float timeLeft=0.5f;
	// Use this for initialization
	void Start () {
        Timer.main.logInTimer(onMisUpdate);
	}
	
	// Update is called once per frame
	public void onMisUpdate (float time) {
        timeLeft -= time;
        if (timeLeft <= 0)
        {
            Destroy(gameObject);
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
            Debug.Log(" on trigger enter 2D "+other.gameObject.name);
            RoleState role = other.gameObject.GetComponent<RoleState>();
            RoleState rolestate = Creater.GetComponent(typeof(RoleState)) as RoleState;
            if (role != null && rolestate.team != role.team)
            {
                role.TakeDamage(Damage);
            }

        }
    }
}
