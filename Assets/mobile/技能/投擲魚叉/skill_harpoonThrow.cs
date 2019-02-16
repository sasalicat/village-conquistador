using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill_harpoonThrow : add_skill_accum
{
    public const int BaseDamage = 50;
    public const float BaseStiff = 0.3f;

    public int power;

    protected int selfMissileNo = 2;
    private GameObject missilePraf;//暫存總missileTable內得到的預設體
    private RoleState selfState;
    getVector getVector;
    float time = 0;
    protected Vector3 misLocalPos = new Vector3(0, -1, 0);

    private System.Diagnostics.Stopwatch now_watch = null;
    public override float CD
    {
        get
        {
            return 4f;
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
            //Debug.Log(">in can use CDTime is" + TimeLeft);
            return (TimeLeft <= 0 && accumLeft <= 0);//如果CDTime小於0代表技能可以使用
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
            return 1f;
        }
    }
    public override void onAccum(float time)
    {
        this.time += time;
        base.onAccum(time);
        print("time pass:"+this.time);
    }
    private GameObject phantom = null;//存在施放技能時角色手上產生的魚叉;
    //----------------------------------------------------------------------
    public override void beInterrupt(Dictionary<string, object> nothing)
    {
        base.beInterrupt(nothing);
        Destroy(phantom);
        phantom = null;
    }
    public override void action(Dictionary<string, object> args)
    {
        //now_watch.Stop();
        //print("skill_haepoonThrow 被觸發 ~~~~~~~~~~~ 歷經:"+now_watch.Elapsed.TotalMilliseconds+" 毫秒");
        base.action(args);
        this.time = 0;

        Destroy(phantom);
        phantom = null;
        getVector = GameObject.Find("keyTabel").GetComponent<getVector>();
        Vector3 origenPlayerPosition = (Vector3)args["PlayerPosition"];//施放技能時玩家位置
        Vector3 mousePosition = (Vector3)args["MousePosition"];//施放技能時鼠標點擊位置
        //Instantiate(testObj, mousePosition,transform.rotation);
        Vector3 relative = mousePosition - origenPlayerPosition;
        //使用getOriginalInitPoint得到技能在client端创建物件的正确位置
        //Vector3 tragetPos = getVector.getOriginalInitPoint(origenPlayerPosition, mousePosition, new Vector3(0, -1, 0));//獲得相對座標
        float angle = Vector3.Angle(Vector3.up, relative);
        Vector3 tragetPos = Quaternion.Euler(0, 0, angle) * misLocalPos + origenPlayerPosition;

        //制造子弹物件

        GameObject newone = Instantiate(missilePraf, tragetPos, Quaternion.Euler(relative));
        // Debug.Log("direction 為"+direction);
        newone.transform.up = relative.normalized;
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
        float stiff = Attribute.getRealStiff(0.5f, u.stiffable);
        missile.Damage = new damage(1, num, stiff, false, false, gameObject, selfIndex);
    }

    public override void trigger(Dictionary<string, object> args)
    {
        now_watch = new System.Diagnostics.Stopwatch();

        base.trigger(args);
        Debug.Log("魚叉投擲被觸發");
        //((Anim_Mobile)anim).switchWeaponFalse();
        now_watch.Start();
        ((Anim_Mobile)anim).action(2);
        getVector = GameObject.Find("keyTabel").GetComponent<getVector>();
        Vector3 origenPlayerPosition = (Vector3)args["PlayerPosition"];//施放技能時玩家位置
        Vector3 mousePosition = (Vector3)args["MousePosition"];//施放技能時鼠標點擊位置
        //Instantiate(testObj, mousePosition,transform.rotation);
        Vector3 relative = mousePosition - origenPlayerPosition;
        Transform rightHand = ((Anim_Mobile)anim).righthand.transform;
        GameObject newone = Instantiate(missilePraf, Vector2.zero, Quaternion.Euler(relative), rightHand);
        newone.GetComponent<Missile>().enabled = false;
        newone.GetComponent<Collider2D>().enabled = false;
        newone.transform.rotation = rightHand.rotation;
        newone.transform.localPosition = Vector2.zero;
        newone.transform.Rotate(new Vector3(0,0, 90));
        newone.transform.localScale = new Vector2(1.2f, 1.2f);
        phantom = newone;
    }

    public override void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        //初始化赋值
        // Debug.Log("魔术师扑克牌初始化 state:" + state);
        base.onInit(table, state, anim);
        missilePraf = table.MissileList[selfMissileNo];

    }
}
