using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nq_buff : MonoBehaviour, CDEquipment
{
    public const float CD = 5f;
    public float CDTime = 0;
    const short selfMissileNo = 0;
    private GameObject missilePraf;
    private GameObject missilePraf2;//暫存總missileTable內得到的預設體
    private RoleState selfState;
    private AnimatorTable animator;

    sbyte index;
    public bool CanUse
    {
        get
        {
            return CDTime <=0;
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
            return true;
        }
    }

    public sbyte Kind
    {
        get
        {
            return EquipmentTable.PASSIVE_SKILL;
        }
    }

    public sbyte No
    {
        get
        {
            return 33;
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


    public void setTime(float time)
    {
        CDTime -= time;
    }

    public void trigger(Dictionary<string, object> args)
    {
        ((GameObject)args["Traget"]).GetComponent<Controler>().addBuffByNo(1);

        //getVector getVector = GameObject.Find("keyTabel").GetComponent<getVector>();
        //Vector3 origenPlayerPosition = (Vector3)args["PlayerPosition"];//施放技能時玩家位置
        //Vector3 mousePosition = (Vector3)args["MousePosition"];//施放技能時鼠標點擊位置
        //使用getOriginalInitPoint得到技能在client端创建物件的正确位置
        //Vector3 tragetPos = getVector.getOriginalInitPoint(origenPlayerPosition, transform.position, new Vector3(-3, 0, 0));//獲得相對座標
        //Vector3 tragetPos2 = getVector.getOriginalInitPoint(origenPlayerPosition, transform.position, new Vector3(3, 0, 0));
        //制造子弹物件
        //Vector3 direction = mousePosition - origenPlayerPosition;
        //GameObject newone = Instantiate(missilePraf, tragetPos, this.transform.rotation);
        //missilePraf.transform.forward = direction;
        //missilePraf.transform.eulerAngles = new Vector3(0, 0, missilePraf.transform.eulerAngles.z);

        /*GameObject newone = Instantiate(missilePraf, tragetPos, transform.rotation);
        
        GameObject newone2 = Instantiate(missilePraf2, tragetPos2, transform.rotation);
        newone2.transform.eulerAngles = new Vector3(0, 0, newone2.transform.eulerAngles.z + 180);

        //newone.transform.up = -direction;
        //修改子弹物件携带的子弹脚本
        Missile missile = newone.GetComponent<Missile>();
        Missile missile2 = newone2.GetComponent<Missile>();

        newone.transform.parent = this.transform;
        newone2.transform.parent = this.transform;
        //missile.Creater = gameObject;
        //创建伤害物件
        */

        CDTime = CD;//技能冷卻
        //Debug.Log("in trigger CDTime is" + CDTime);
        animator.AttackStart();

    }

    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        //初始化赋值
        this.selfState = state;
        this.animator = anim;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}