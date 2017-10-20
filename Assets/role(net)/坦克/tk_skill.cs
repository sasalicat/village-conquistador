using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tk_skill : MonoBehaviour, CDEquipment
{
    public GameObject dunpai;
    public const float CD = 0.5f;//0.5f;
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
    public bool CanUse
    {
        get
        {
            Debug.Log(" in can use CDTime is" + CDTime);
            return (CDTime <= 0 && Consumption < selfState.nowMp);//如果CDTime小於0代表技能可以使用
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
            return 20;//因為是攻擊所以無消耗
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
        Vector3 tragetPos = getVector.getOriginalInitPoint(origenPlayerPosition, mousePosition, new Vector3(0, -4, 0));//獲得相對座標
        //制造子弹物件
        Vector3 direction = mousePosition - origenPlayerPosition;
        //GameObject newone = Instantiate(missilePraf, tragetPos, this.transform.rotation);
        //missilePraf.transform.forward = direction;
        //missilePraf.transform.eulerAngles = new Vector3(0, 0, missilePraf.transform.eulerAngles.z);

        Controler controler = this.gameObject.GetComponent<Controler>();
        if (dunpai == null) {
            NetManager.createObstacle(gameObject, transform.position, 4);
            animator.SkillStart();
            controler.skillLimit = new List<sbyte> { index };
        }
        else
        {
            controler.skillLimit = null;
            Destroy(dunpai);
            animator.SkillEnd();    
        }

        CDTime = CD;//技能冷卻
        //Debug.Log("in trigger CDTime is" + CDTime);
        


    }


    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        //初始化赋值
        this.selfState = state;
        this.animator = anim;
    }

    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        //  if（local != Creater.GetComponent<NetRoleState>().islocal &&
    }

    public void onCreateFinish(ObstacleState obstacle)
    {
        if (obstacle.Kind == 4)
        {
            dunpai = obstacle.gameObject;
            obstacle.transform.parent = this.transform;
            obstacle.transform.localEulerAngles = new Vector3(0, 0, 0);
            obstacle.transform.localPosition = new Vector3(0, -2, 0);
        }
    }

}
