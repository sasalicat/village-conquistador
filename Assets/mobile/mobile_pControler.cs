using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobile_pControler : fsynControler {
    private int movestateCount = 0;

    public bool MoveWillingness
    {
        set
        {
            if (value)
            {
                movestateCount += 1;
                if (movestateCount == 1&& state.canMove)//剛好等於1說明之前等於0,也就是之前處於不能移動的狀態
                {
                    ((Anim_Mobile)anim).WalkStart();
                }
                //Debug.Log("移動意願為正 moveStateCount:" + movestateCount);
            }
            else
            {
                movestateCount -= 1;
                if (movestateCount == 0)//上面的註解逆推就能知道
                {
                    ((Anim_Mobile)anim).WalkEnd();
                }
                //Debug.Log("移動意願為負 moveStateCount:" + movestateCount);
            }

        }
        get
        {
            return movestateCount > 0;
        }
    }
    public override void setDirection(Vector2 mousePos)
    {
        if (state.canRota)
        {
            transform.up = -(mousePos - (Vector2)transform.position);
            transform.Rotate(new Vector3(0,0,90));
        }
    }
    public override void move(float interval)
    {
        if(MoveWillingness&&state.canMove)
            transform.Translate(new Vector2(-1, 0) * state.RealSpeed* interval);
    }
}
