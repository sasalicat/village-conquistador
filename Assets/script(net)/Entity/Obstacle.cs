namespace KBEngine
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class Obstacle :Entity
    {
        public ObstacleState state;
        public void set_Hp(short Hp)
        {
            if(state!=null)//在EnterWorld之前被呼叫会出问题
                state.nowHp = Hp;
        }
    }
}
