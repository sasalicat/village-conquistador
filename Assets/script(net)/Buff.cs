using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  Buff :MonoBehaviour {
    public float TimeLeft
    {
        get
        {
            return timeLeft;
        }
    }
    public  float timeLeft=1;
    public RoleState roleState;
    public abstract float Duration
    {
        get;
    }
    public int index;//位于buff阵列的索引
    public abstract bool onInit(RoleState role, Buff[] Repetitive,MissileTable misTable,Dictionary<string,object> args);//如果角色身上已经有相同buff存在了则Repetitive将不为null,Repetitive会回传所有相同的buff
    //onInit的回传值为是否添加,如果回传false,该buff会被删掉而不触发onRemove
    public virtual void onIntarvel(RoleState role, float timeBetween)
    {
        if (Duration > 0)
        {
            timeLeft -= timeBetween;
            if (timeLeft <= 0)
            {
                deleteSelf(role);
            }
        }
    }
    public abstract void onRemove(RoleState role);
    public void deleteSelf(RoleState role)
    {
        Debug.Log("deleteSelf");
        this.onRemove(role);
        Destroy(this);
    }
    void Start()
    {
        if (Duration > 0)
        {
            timeLeft = Duration;
        }
    }
}
