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
                if (movestateCount == 1)//剛好等於1說明之前等於0,也就是之前處於不能移動的狀態
                {
                    anim.moveStart();
                }
                Debug.Log("移動意願為正 moveStateCount:" + movestateCount);
            }
            else
            {
                movestateCount -= 1;
                if (movestateCount == 0)//上面的註解逆推就能知道
                {
                    anim.moveEnd();
                }
                Debug.Log("移動意願為負 moveStateCount:" + movestateCount);
            }

        }
        get
        {
            return movestateCount > 0;
        }
    }
    public override void move(float interval)
    {
        if(MoveWillingness)
            transform.Translate(new Vector2(0, -1) * state.RealSpeed* interval);
    }
}
