using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obs1 : ObstacleState {
    void OnTriggerEnter2D(Collider2D other)
    {
        callMethodNull();
    }
    public override void methodNull()
    {
        Debug.Log("methodnull");
    }
}
