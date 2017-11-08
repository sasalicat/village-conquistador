using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class probiliticKniftShot : MonoBehaviour, Equipment
{
    public const int kniftIntervalAngle = 30;
    public GameObject kniftPraf;
    private float cd = 0;
    private RoleState state;
    private AnimatorTable anim;
    getVector getVector;
    private sbyte index;



    public sbyte Kind
    {
        get
        {
            return EquipmentTable.ON_ACTIVE_SKILL;
        }
    }

    public sbyte No
    {
        get
        {
            return 59;
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


    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        getVector = GameObject.Find("keyTabel").GetComponent<getVector>();
        kniftPraf = table.MissileList[31];
        this.state = state;
        this.anim = anim;
    }

    public void setTime(float time)
    {
        cd -= time;
    }

    public void trigger(Dictionary<string, object> args)
    {
        int point=(sbyte)args["randomPoint"];
        if (point>50&&point<=60) {
            Vector3 origenPlayerPosition = (Vector3)args["PlayerPosition"];//施放技能時玩家位置
            object mpos;
            Vector3 mousePosition;
            if (args.TryGetValue("MousePosition",out mpos)) {
                 mousePosition= (Vector3)mpos;//施放技能時鼠標點擊位置
            }
            else
            {
                mousePosition = (Vector3)args["TragetPosition"];
            }
            int nowAngle = -2 * kniftIntervalAngle;
            Debug.Log("angle to v1 is" + Vector3.Angle(mousePosition - origenPlayerPosition, Vector3.down) + "v2 is" + Vector3.Angle(mousePosition - origenPlayerPosition, Vector3.right));

            float roleRotaZ = Vector3.Angle(mousePosition - origenPlayerPosition, Vector3.up);
            if (Vector3.Angle(mousePosition - origenPlayerPosition, Vector3.left) > 90)
            {
                roleRotaZ = -roleRotaZ;
            }

            for (int i = 0; i < 5; i++)
            {
                float realangle = ((float)nowAngle + 180) / 180 * Mathf.PI;//用来计算的真正弧度角,要+180的原因是因为角色的朝向和eulerAngle的指向正好相反
                Vector3 pos = getVector.getOriginalInitPoint(origenPlayerPosition, mousePosition, new Vector3(Mathf.Sin(realangle), Mathf.Cos(realangle), 0));
                GameObject newone = Instantiate(kniftPraf, pos, transform.rotation);

                newone.transform.eulerAngles = new Vector3(0, 0, -nowAngle + roleRotaZ + 270);//和realAngle无关,+270是因为mouse和角色面向相反所以要+180但是飞刀图片自带90度转向
                Missile missile = newone.GetComponent<Missile>();
                missile.Creater = gameObject;
                int num = Attribute.GetAttackDamageNum(25, state.Power);
                float stiff = Attribute.getRealStiff(0.3f, state.Stiffable);
                missile.Damage = new damage(1, num, stiff, false, false, gameObject);
                nowAngle += kniftIntervalAngle;

                anim.AttackStart();
            }
        }

    }

}

