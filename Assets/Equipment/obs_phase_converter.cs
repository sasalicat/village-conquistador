using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obs_phase_converter : ObstacleState
{
    float timer_f = 0f;
    int timer_i = 0;

    protected void Update()
    {
        
    }

    public override void methodNull()
    {
        Debug.Log("methodnull被呼叫!!!");
    }
}
