using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obs1 : ObstacleState
{
    public damage damage;
    void OnTriggerEnter2D(Collider2D other)
    {
        callMethodNull();
    }
    public override void methodNull()
    {
        Debug.Log("methodnull被呼叫!!!");
    }
}
