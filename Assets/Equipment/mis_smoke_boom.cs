using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_smoke_boom : Missile {
    float timer_f = 0f;
    int timer_i = 0;

    // Use this for initialization
    void Start () {
        NetRoleState netRoleState = Creater.GetComponent<NetRoleState>();
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        if (netRoleState.islocal)
        {
            renderer.color = new Color(1,1,1,0.5f);
        }
    }
	
	// Update is called once per frame
	void Update () {
        timer_f += Time.deltaTime;
        timer_i = (int)timer_f;
        Debug.Log(timer_i + "秒");
        if (timer_i == 4)
        {
            Destroy(this.gameObject);
        }
    }
}
