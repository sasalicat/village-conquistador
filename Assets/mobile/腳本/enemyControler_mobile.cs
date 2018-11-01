using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyControler_mobile : enemyControler {
    public Vector2 direction = Vector2.zero;
    bool wantMove=false;
    public override void move(float interval)
    {
        if (state.canMove)
        {
            transform.position = (Vector2)transform.position + direction.normalized * state.RealSpeed * interval;
        }
    }
}
