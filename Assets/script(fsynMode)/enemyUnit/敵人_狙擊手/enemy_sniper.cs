using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_sniper : skill_accum
{
    public const int BaseDamage = 50;
    public const float BaseStiff = 0;

    public sbyte index;
    const short selfMissileNo = 0;
    private GameObject missilePraf;//暫存總missileTable內得到的預設體

    //實做Equipment介面-------------------------------------------------------
    public override sbyte No
    {
        get
        {
            return 0;
        }
    }


    public override sbyte Kind//本技能属于主动技能所以kind为 PASSIVE_SKILL
    {
        get
        {
            return EquipmentTable.PASSIVE_SKILL;
            //return EquipmentTable.ON_TAKE_DAMAGE;
        }
    }
    //實做CDEquipment介面----------------------------------------------

    public override uint Consumption
    {
        get
        {
            return 0;
        }
    }

    public override bool Designated
    {
        get
        {
            return false;
        }
    }

    public override float CD
    {
        get
        {
            return 3;
        }
    }

    public override float AccumTime
    {
        get
        {
            return 1f;
        }
    }



    //----------------------------------------------------------------------

    public override void action(Dictionary<string, object> args)
    {
        base.action(args);
        getVector getVector = GameObject.Find("keyTabel").GetComponent<getVector>();
        Vector3 origenPlayerPosition = (Vector3)args["PlayerPosition"];//施放技能時玩家位置
        Vector3 mousePosition = (Vector3)args["MousePosition"];//施放技能時鼠標點擊位置
        //使用getOriginalInitPoint得到技能在client端创建物件的正确位置
        Vector3 tragetPos = getVector.getOriginalInitPoint(origenPlayerPosition, mousePosition, new Vector3(0, -1, 0));//獲得相對座標
        //制造子弹物件
        Vector3 direction = mousePosition - origenPlayerPosition;
        //GameObject newone = Instantiate(missilePraf, tragetPos, this.transform.rotation);
        //missilePraf.transform.forward = direction;
        //missilePraf.transform.eulerAngles = new Vector3(0, 0, missilePraf.transform.eulerAngles.z);

        GameObject newone = Instantiate(missilePraf, tragetPos, Quaternion.Euler(-direction));
        newone.transform.up = direction;
        //修改子弹物件携带的子弹脚本
        Missile missile = newone.GetComponent<Missile>();
        missile.Creater = gameObject;
        //创建伤害物件
        unit u = this.GetComponent<unit>();
        int num = Attribute.GetSpecialDamageNum(BaseDamage, u.skill);
        float stiff = Attribute.getRealStiff(BaseStiff, u.stiffable);
        missile.Damage = new damage(2, num, stiff, false, false, gameObject);

        //Debug.Log("in trigger CDTime is" + CDTime);



    }
    public override void trigger(Dictionary<string, object> args)
    {
        base.trigger(args);
        anim.AttackStart();
    }

    public override void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        //初始化赋值
        base.onInit(table,state,anim);
        missilePraf = table.MissileList[47];
    }
}
