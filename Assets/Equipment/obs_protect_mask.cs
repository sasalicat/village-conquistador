using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obs_protect_mask : ObstacleState{
    float timer_f = 0f;
    int timer_i = 0;

    protected void Update () {
        base.Update();
        timer_f += Time.deltaTime;
        timer_i = (int)timer_f;
        Debug.Log(timer_i + "秒");
        if (timer_i == 15)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        callMethodNull();
    }
    public override void methodNull()
    {
        Debug.Log("methodnull被呼叫!!!");
    }
}
