using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_ls_atk : MonoBehaviour,CDEquipment{

    public const float CD = 3f;//0.5f;
    public const int BaseDamage = 50;
    public const float BaseStiff = 0;

    private Vector3 localPos = new Vector2(0, -2.5f);
    public float CDTime = 0;
    public sbyte index;
    const short selfMissileNo = 46;
    private GameObject missilePraf;//暫存總missileTable內得到的預設體
    private RoleState selfState;
    private AnimatorTable animator;
    public float speed = 20;
    private float rushLeft = 0;
    private float waitLeft = 0;
    private bool rushing = false;
    public float rushSpeed = 20;
    private Vector2 rushDirection;
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
        CDTime -= time;//減少CD時間
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
    public bool CanUse
    {
        get
        {
            //Debug.Log(" in can use CDTime is" + CDTime);
            return (CDTime <= 0);//如果CDTime小於0代表技能可以使用
        }
    }
    public uint Consumption
    {
        get
        {
            return 0;
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
    public void waitForRush(float time)
    {
        waitLeft -= time;
        if (waitLeft <= 0)
        {
            Timer.main.loginOutTimer(waitForRush);
            Timer.main.logInTimer(rush);
            GameObject area= Instantiate(missilePraf, transform.position, transform.rotation, transform);
            area.transform.localPosition = localPos;
            var missile = area.GetComponent<Missile>();
            missile.Creater = gameObject;
            missile.Damage = new damage(1, 50, 0);
            rushLeft = 0.3f;
        }
    }
    public void rush(float time)
    {
        //Debug.Log("rushLeft 剩餘" + rushLeft);
        if (rushLeft <= time)
        {

            transform.position += (Vector3)(rushDirection * rushLeft*speed);
            rushLeft = 0;
            Timer.main.loginOutTimer(rush);
            selfState.canAction = true;
            selfState.canMove = true;
            selfState.canRota = true;
            selfState.canBeStiff = true;
            rushing = false;
        }
        else
        {
            Debug.Log("rush中canRota:" + selfState.canRota);
            transform.position +=(Vector3)(rushDirection * time*speed);
        }
        rushLeft -= time;
    }

    public void trigger(Dictionary<string, object> args)
    {
        Debug.Log("老鼠的攻擊被觸發");
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
        animator.AttackStart();

        selfState.canAction = false;
        selfState.canMove = false;
        selfState.canRota = false;
        selfState.canBeStiff = false;
        Debug.Log("在ls_atk中設置完成後canstiff:"+selfState.canBeStiff+"camRota:"+selfState.canRota);
        rushing = true;
        waitLeft = 0.5f;
        rushDirection = (mousePosition - transform.position).normalized;
        Timer.main.logInTimer(waitForRush);
        CDTime = CD;//技能冷卻
        //Debug.Log("in trigger CDTime is" + CDTime);
        animator.AttackStart();


    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("呼叫了OnCollisionEnter2D");
        if (rushing && rushLeft>0)
        {
            rushLeft = 0;
            Timer.main.loginOutTimer(rush);
            selfState.canAction = true;
            selfState.canMove = true;
            selfState.canRota = true;
            selfState.canBeStiff = true;
            rushing = false;
        }
    }

    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        //初始化赋值
        missilePraf = table.MissileList[46];
        this.selfState = state;
        this.animator = anim;
    }
}
