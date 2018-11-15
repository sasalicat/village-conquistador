using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseAtk : add_skill_accum
{
    //public new const float CD = 0.8f;//0.5f;
    public const int BaseDamage = 50;
    public const float BaseStiff = 0.25f;

    public int power;
    public sbyte index;
    protected int selfMissileNo = 0;
    private GameObject missilePraf;//暫存總missileTable內得到的預設體
    private RoleState selfState;
    getVector getVector;
    private Anim_Mobile animator;
    public GameObject testObj;
    protected Vector3 misLocalPos = new Vector3(0, -1, 0);
    public override float CD
    {
        get
        {
            return 0.8f;
        }
    }
    //實做Equipment介面-------------------------------------------------------
    public override sbyte No
    {
        get
        {
            return 0;
        }
    }

    public override sbyte selfIndex
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

    public override sbyte Kind//本技能属于主动技能所以kind为 PASSIVE_SKILL
    {
        get
        {
            return EquipmentTable.PASSIVE_SKILL;
            //return EquipmentTable.ON_TAKE_DAMAGE;
        }
    }
    //實做CDEquipment介面----------------------------------------------

    public override bool CanUse
    {
        get
        {
            //Debug.Log(" in can use CDTime is" + CDTime);
            return (TimeLeft <= 0);//如果CDTime小於0代表技能可以使用
            //return true;
        }
    }
    public override uint Consumption
    {
        get
        {
            return 0;//因為是攻擊所以無消耗
        }
    }

    public override bool Designated
    {
        get
        {
            return false;
        }
    }


    public override float AccumTime
    {
        get
        {
            return 0;
        }
    }

    //----------------------------------------------------------------------


    public override void trigger(Dictionary<string, object> args)
    {

        getVector = GameObject.Find("keyTabel").GetComponent<getVector>();
        Vector3 origenPlayerPosition = (Vector3)args["PlayerPosition"];//施放技能時玩家位置
        Vector3 mousePosition = (Vector3)args["MousePosition"];//施放技能時鼠標點擊位置
        //Instantiate(testObj, mousePosition,transform.rotation);
        Vector3 relative = mousePosition - origenPlayerPosition;
        Debug.Log("In fsyn_trigger relative:(" + relative.x + "," + relative.y + ")");
        //使用getOriginalInitPoint得到技能在client端创建物件的正确位置
        //Vector3 tragetPos = getVector.getOriginalInitPoint(origenPlayerPosition, mousePosition, new Vector3(0, -1, 0));//獲得相對座標
        float angle = Vector3.Angle(Vector3.up,relative);
        Vector3 tragetPos = Quaternion.Euler(0, 0, angle) * misLocalPos+origenPlayerPosition;

        //制造子弹物件

        GameObject newone = Instantiate(missilePraf, tragetPos, Quaternion.Euler(relative));
        // Debug.Log("direction 為"+direction);
        newone.transform.up = -relative.normalized;
        //修改子弹物件携带的子弹脚本
        Missile missile = newone.GetComponent<Missile>();
        if (beUsed != null)
            beUsed(mousePosition);
        if (onCreateMissile != null)
            onCreateMissile((addibleMissile)missile, true);
        missile.Creater = gameObject;
        //创建伤害物件
        unit u = this.GetComponent<unit>();

        int num = Attribute.GetAttackDamageNum(50, u.power);
        float stiff = Attribute.getRealStiff(0.3f, u.stiffable);
        missile.Damage = new damage(1, num, stiff, false, false, gameObject, selfIndex);

        TimeLeft = CD;//技能冷卻

        animator.AttackStart();
    }


    public override void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        //初始化赋值
// Debug.Log("魔术师扑克牌初始化 state:" + state);
        missilePraf = table.MissileList[selfMissileNo];
        this.selfState = state;
        this.animator = (Anim_Mobile)anim;

    }
}

