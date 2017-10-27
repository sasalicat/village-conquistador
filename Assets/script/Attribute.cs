using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute{
    public static int GetAttackDamageNum(int baseNum,int power)
    {
        return baseNum + (int)((power / 100f) * baseNum);
    }
    public static int GetSpecialDamageNum(int baseNum,int skill)
    {
        return baseNum + (int)((skill / 100f) * baseNum);
    }
    public static int getMaxHp(int standHp,int physique)
    {
        return standHp + (int)((physique / 100f) * standHp);
    }
    public static int ReduceAttackDamageNum(int num,int reduce)
    {
        return num + (int)((reduce / 100f) * num);
    }
    public static float GetMpRecover(float num,int energyRecover)
    {
        return num + ((float)energyRecover / 100) * num;
    }
    public static float getSpeedAfter(float num,int accelerate)
    {
        return num + ((float)accelerate / 100) * num;
    }
    public static float getRealStiff(float num, int stiffable)
    {
        return num + ((float)stiffable / 100) * num;
    }
}
