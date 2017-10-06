using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bx_skill : MonoBehaviour, CDEquipment
{
    public float jishiqi = 0f;
    public const float CD = 5f;//0.5f;
    public const int BaseDamage = 50;
    public const float BaseStiff = 0.25f;

    public float CDTime = 0;
    public sbyte index;
    const short selfMissileNo = 0;
    private GameObject missilePraf;//暫存總missileTable內得到的預設體
    private RoleState selfState;
    private AnimatorTable animator;

    Vector3 realdirection = new Vector3(0, 0, 55);

    //實做Equipment介面-------------------------------------------------------
    public sbyte No
    {
        get
        {
            return 0;
        }
    }

    public sbyte selfIndex
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

    public sbyte Kind//本技能属于主动技能所以kind为 PASSIVE_SKILL
    {
        get
        {
            return EquipmentTable.PASSIVE_SKILL;
            //return EquipmentTable.ON_TAKE_DAMAGE;
        }
    }
    //實做CDEquipment介面----------------------------------------------
    public void setTime(float time)
    {
        bool chengli = false;
        if (jishiqi > 0)
        {
            chengli = true;
        }
        jishiqi -= time;


        if (chengli && jishiqi <= 0)
        {
            selfState.canAction = true;
            selfState.canMove = true;
            selfState.canRota = true;

            Debug.Log(selfState.canAction + "转向2");
        }
        CDTime -= time;//減少CD時間
    }
    public bool CanUse
    {
        get
        {
            Debug.Log(" in can use CDTime is" + CDTime);
            return (CDTime <= 0);//如果CDTime小於0代表技能可以使用
        }
    }
    public float TimeLeft
    {
        get
        {
            return CDTime;
        }

        set
        {
            CDTime = value;
        }
    }
    public uint Consumption
    {
        get
        {
            return 0;//因為是攻擊所以無消耗
        }
    }

    public bool Designated
    {
        get
        {
            return false;
        }
    }



    //----------------------------------------------------------------------


    public void trigger(Dictionary<string, object> args)
    {

        getVector getVector = GameObject.Find("keyTabel").GetComponent<getVector>();
        Vector3 origenPlayerPosition = (Vector3)args["PlayerPosition"];//施放技能時玩家位置
        Vector3 mousePosition = (Vector3)args["MousePosition"];//施放技能時鼠標點擊位置
        //使用getOriginalInitPoint得到技能在client端创建物件的正确位置
        Vector3 tragetPos = getVector.getOriginalInitPoint(origenPlayerPosition, mousePosition, new Vector3(0, -10, 0));//獲得相對座標
        //制造子弹物件
        Vector3 direction = mousePosition - origenPlayerPosition;
        //GameObject newone = Instantiate(missilePraf, tragetPos, this.transform.rotation);
        //missilePraf.transform.forward = direction;
        //missilePraf.transform.eulerAngles = new Vector3(0, 0, missilePraf.transform.eulerAngles.z);

        GameObject newone = Instantiate(missilePraf, tragetPos, this.transform.rotation);
        Debug.Log("now`s up is" + newone.transform.up+"right is"+newone.transform.right+"forward is"+newone.transform.forward+"z is"+ newone.transform.forward.z);
        newone.transform.eulerAngles = transform.eulerAngles + realdirection;

        //修改子弹物件携带的子弹脚本
        Missile missile = newone.GetComponent<Missile>();
        missile.Creater = gameObject;
        //沒有傷害所以不要再複製了

        missile.Damage = new damage(2, 0,0, false, false, gameObject);


        CDTime = CD;//技能冷卻
        //Debug.Log("in trigger CDTime is" + CDTime);
        animator.SkillStart();

        selfState.canAction = false;
        selfState.canMove = false;
        selfState.canRota = false;
        jishiqi = 0.5f;
    }


    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        //初始化赋值
        missilePraf = table.MissileList[21];
        this.selfState = state;
        this.animator = anim;
    }
}