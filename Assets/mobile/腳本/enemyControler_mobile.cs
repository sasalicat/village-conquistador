using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyControler_mobile : enemyControler {
    public Vector2 direction = Vector2.zero;
    public bool wantMove=false;
    public override void move(float interval)
    {
        //Debug.Log("+++++state:"+this.state.nowStateNo+" "+state.canMove);
        if (state.canMove && wantMove)
        {
            // transform.position = (Vector2)transform.position + direction.normalized * state.RealSpeed * interval;
            transform.Translate(new Vector3(-state.RealSpeed * interval, 0));
        }
    }
}
