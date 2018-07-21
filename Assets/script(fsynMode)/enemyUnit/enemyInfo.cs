using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyInfo {
    public virtual int getBaseHp(int level)
    {
        return 1000;
    }
    public virtual int getBaseMapRec(int level)
    {
        return 2;
    }
    public virtual int getSpeedScale(int level)
    {
        return 1;
    }
    public virtual int getStiffable(int level)
    {
        return 0;
    }
    public virtual int getStiffReduce(int level)
    {
        return 0;
    }
    public virtual int getDamageReduce(int level)
    {
        return 0;
    }
    public virtual int getSpecialReduce(int level)
    {
        return 0;
    }
    public virtual void initUnit(RoleState role,int level)
    {
        role.maxHp = getBaseHp(level);
        role.EnergyRecover = getBaseMapRec(level);
        role.SpeedScale = getSpeedScale(level);
        role.Stiffable = getStiffable(level);
        role.stiffReduce = getStiffReduce(level);
        role.damageReduce = getDamageReduce(level);
        role.specialReduce = getDamageReduce(level);
    }
}
