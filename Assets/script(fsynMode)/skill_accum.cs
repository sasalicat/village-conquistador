using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class skill_accum : MonoBehaviour, CDEquipment
{
    private sbyte index;
    private float timeLeft;
    protected MissileTable table;
    protected RoleState state;
    protected AnimatorTable anim;
    protected float accumLeft=0;
    private Dictionary<string, object> args;
    public abstract float CD
    {
        get;
    }
    public abstract float AccumTime
    {
        get;
    }
    public virtual bool CanUse
    {
        get
        {
            //Debug.Log("accum canUse timeLeft" + timeLeft + "accumTime" + AccumTime);
            return timeLeft <= 0 && accumLeft <= 0;
        }
    }

    public abstract uint Consumption
    {
        get;
    }

    public abstract bool Designated
    {
        get;
    }

    public abstract sbyte Kind
    {
        get;
    }

    public abstract sbyte No
    {
        get;
    }

    public virtual sbyte selfIndex
    {
        get
        {
            return index;
        }

        set
        {
            index = value;
        }
    }

    public virtual float TimeLeft
    {
        get
        {
            return timeLeft;
        }

        set
        {
            timeLeft = value;
        }
    }

    public virtual void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        this.table = table;
        this.state = state;
        this.anim = anim;
        GetComponent<Controler>().Be_Interrupt += beInterrupt;
        Debug.Log("skill_accum中 onInit"+ GetComponent<Controler>().Be_Interrupt);
    }

    public void setTime(float time)
    {
        timeLeft -= time;
// Debug.Log("設置時間之後timeLeft為:" + timeLeft);
    }
    public virtual void onAccum(float time)
    {
        accumLeft -= time;
        if (accumLeft <= 0)
        {
            Timer.main.loginOutTimer(onAccum);
            action(args);
        }
    }

    public virtual void trigger(Dictionary<string, object> args)
    {
        Timer.main.logInTimer(onAccum);
        accumLeft = AccumTime;
        this.args = args;
    }
    public virtual void action(Dictionary<string, object> args)
    {
        timeLeft = CD;
    }
    public virtual void beInterrupt(Dictionary<string,object> nothing)
    {
        if (accumLeft >= 0)
        {
            Timer.main.loginOutTimer(onAccum);
            accumLeft =0;
        }
    }
}
