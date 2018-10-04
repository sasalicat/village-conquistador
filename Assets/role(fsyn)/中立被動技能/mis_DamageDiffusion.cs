using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_DamageDiffusion : Missile
{
    public GameObject oriEnemy;
    private float timeLeft = 0.2f;
    void onTime(float time)
    {
        if (timeLeft <= 0)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Timer.main.logInTimer(onTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        RoleState otherState = other.gameObject.GetComponent<RoleState>();
        if (otherState != null && other!=oriEnemy)
        {
            otherState.TakeDamage(Damage);
        }
    }
    void OnDestroy()
    {
        Timer.main.loginOutTimer(onTime);
    }
}
