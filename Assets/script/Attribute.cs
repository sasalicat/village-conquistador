﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute{
    public static int GetAttackDamageNum(int baseNum,int power)
    {
        return baseNum + ((int)((float)power / 100) * baseNum);
    }
    public static int GetSpecialDamageNum(int baseNum,int skill)
    {
        return baseNum + ((int)((float)skill / 100) * baseNum);
    }
    public static int getMaxHp(int standHp,int physique)
    {
        return standHp + ((int)((float)physique / 100) * standHp);
    }
    public static int ReduceAttackDamageNum(int num,int reduce)
    {
        return num + ((int)((float)reduce / 100) * num);
    }
}
