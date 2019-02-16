using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lmbx_skill_buff : Buff
{
    GameObject moonToken;
    public double speed;
    public float recoverleft=1;
    public override float Duration
    {
        get
        {
            return 10;
        }
    }


    public override bool onInit(RoleState role, Buff[] Repetitive, MissileTable mis, Dictionary<string, object> args)
    {
        moonToken = Instantiate(mis.MissileList[43],role.transform);
        moonToken.transform.localPosition = new Vector3(0, 1.25f, 0);
        moonToken.transform.localScale = new Vector3(0.7f, 0.7f, 0);
        Debug.Log("buffForTask onInit");
        if (Repetitive == null)
        {
            Debug.Log("onInit Enter Null");
            role.Accelerate += 30;
        }
        return true;
    }

    public override void onRemove(RoleState role)
    {

        role.Accelerate -= 30;
        Destroy(moonToken);
    }
    public override void onIntarvel(RoleState role, float timeBetween)
    {
        recoverleft -= timeBetween;
        RoleState r = this.GetComponent<RoleState>();
        if (recoverleft <= 0)
        {
            role.BeenTreat(this.gameObject, (int)(r.maxHp * 0.03f));
            recoverleft = 1;
        }
        base.onIntarvel(role, timeBetween);
        //Debug.Log("buff ing");

    }


}