using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class probiliticFireBall : MonoBehaviour, Equipment
{

    public const int BaseDamage = 50;
    public const float BaseStiff = 0.25f;

    public float CDTime = 0;
    public sbyte index;
    const short selfMissileNo = 0;
    private GameObject missilePraf;//暫存總missileTable內得到的預設體
    private RoleState selfState;
    private AnimatorTable animator;

    //實做Equipment介面-------------------------------------------------------
    public sbyte No
    {
        get
        {
            return 60;
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
            return EquipmentTable.ON_ACTIVE_SKILL;
            //return EquipmentTable.ON_TAKE_DAMAGE;
        }
    }
    //實做CDEquipment介面----------------------------------------------
    public void setTime(float time)
    {
        CDTime -= time;//減少CD時間
    }
    //----------------------------------------------------------------------


    public void trigger(Dictionary<string, object> args)
    {
        int point = (sbyte)args["randomPoint"];
        if (point > 20 && point <= 45)
        {
            getVector getVector = GameObject.Find("keyTabel").GetComponent<getVector>();
            Vector3 origenPlayerPosition = (Vector3)args["PlayerPosition"];//施放技能時玩家位置
            object mpos;
            Vector3 mousePosition;
            if (args.TryGetValue("MousePosition", out mpos))
            {
                mousePosition = (Vector3)mpos;//施放技能時鼠標點擊位置
            }
            else
            {
                mousePosition = (Vector3)args["TragetPosition"];
            }
            Vector3 tragetPos = getVector.getOriginalInitPoint(origenPlayerPosition, mousePosition, new Vector3(0, -1, 0));//獲得相對座標
                                                                                                                           //制造子弹物件
            Vector3 direction = mousePosition - origenPlayerPosition;
            //GameObject newone = Instantiate(missilePraf, tragetPos, this.transform.rotation);
            //missilePraf.transform.forward = direction;
            //missilePraf.transform.eulerAngles = new Vector3(0, 0, missilePraf.transform.eulerAngles.z);

            GameObject newone = Instantiate(missilePraf, tragetPos, Quaternion.Euler(direction));
            newone.transform.up = direction;
            //修改子弹物件携带的子弹脚本
            Missile missile = newone.GetComponent<Missile>();
            missile.Creater = gameObject;
            //创建伤害物件
            unit u = this.GetComponent<unit>();
            int num = Attribute.GetAttackDamageNum(BaseDamage, u.power);
            float stiff = Attribute.getRealStiff(BaseStiff, u.stiffable);
            missile.Damage = new damage(2, num, stiff, false, false, gameObject);

                        //Debug.Log("in trigger CDTime is" + CDTime);
            animator.AttackStart();
        }

    }


    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        //初始化赋值
        missilePraf = table.MissileList[42];
        this.selfState = state;
        this.animator = anim;
    }
}

