using UnityEngine;
using System.Collections;

public class unit : MonoBehaviour {
    public const float STAND_CONVESLY_TIME = 1;//标准倒地时间
    public const int STAND_HP = 1000;
    public const float STAND_MP = 100;
    public const float STAND_MP_RECOVER = 4;
    public const float STAND_SPEED = 5;
    //基本屬性
    public int power;//力量 每1點加成1%物理攻擊
    public int skill;//技巧 每1點加成1%特殊傷害
    public int physique;//體質
    public int energyRecover;//蓄能 每1點增加1%每秒能量恢復
                             //public int damageReduce;//減傷 
                             //public int skillReduce;//能抗
    public int accelerate;//加速 加成移動速度
    //public float attackInterval;//攻擊間隔 這項屬性改成技能自己決定例如mg_atk的CD
    //-----------------------------------------------
    //隱藏屬性
    public int stiffable;//造成硬直加成
    public int stiffReduce;//硬直減免
    public int rechargeLifeRecover;//道具生命恢復加成
    public int rechargeEnergyRecover;//道具能量恢復加成
    //public int attackSpeed;//攻擊速度加成
    public int damageReduce;//物理減傷比例
    public int energyReduce;//技能減傷比例
    //-----------------------------------------------

    //-----------------------------------------------
    //實時屬性
    //public int maxHP;
    //public int nowHP;
    //public float maxMP;
    //public float nowMP;//上限100%
    //public float speed;
    //public float energyRecover_Second;//每秒能量恢復
   // public float nowDamageReduce;//物理減傷比例
    //public float nowenergyRecover;//技能減傷比例
    //-----------------------------------------------
    //實時狀態控制
    public float stiffTime = 0;
    public float converselyTime = 0;
    public int action = 1;
    public int actionBefore = 1;
    //public float nextAttack = 0;//剩餘攻擊冷卻時間
    //public float nextSkill = 0;//剩餘技能冷卻時間
    //public float nextRecover = 0;//到下次每秒恢復能量剩餘時間
    //陣營-------------------------------------------
    public int No;//該單位編號,此編號由unitManager生成,等於其 objArray的對應索引值
    public int team;//隊伍號,單機模式下1為玩家,2為電腦
    public bool captain;//是否為主角
    public GameObject master;//若為副主角,則此變數用來記錄主角是哪個
    public int commendMode;//若為副主角,則次變數用來記錄現在的行為模式編號
    //1.跟隨模式
    //2.攻擊模式
    //3.前往模式
    //4.駐守模式
    // Use this for initialization
    void Start () {
        /*maxHP = maxHP + maxHP * (physique / 100);
        nowHP = maxHP;
        nowMP = 0;
        energyRecover_Second += energyRecover_Second * (((float)energyRecover) / 100);
        speed = speed + speed * (((float)accelerate) /100);*/
        //attackInterval -= attackInterval * (((float)attackSpeed)/100);

    }
	
	// Update is called once per frame
	void Update () {
       
        
	}
    /*
    public void beenTreat(int num,GameObject treater,bool fromtools)//治療通過呼叫此方法達成,num為治療量,treater為發動治療的物件,fromtool為true時表示治療者是道具
    {
        if (canBetreat&&!exiled&&alive)
        {
            if (fromtools)//如果治癒者是道具
            {
                num += num * (rechargeLifeRecover / 100);
            }
            if (nowHP + num <= maxHP)
            {
                nowHP += num;
            }
            else
            {
                nowHP = maxHP;
            }
        }
    }
    public void beenRecoverMP(int num,GameObject giver,bool fromtools)//被恢復MP通過呼叫此方法達成,num為增加量,giver為賦予者,fromtools為標記是否赋予者是否為物品
    {
        Debug.Log(gameObject.transform.parent.name + "been recover MP");
        if (canBetreat && !exiled && alive)
        {
            if (fromtools)//如果治癒者是道具
            {
                num += num * (rechargeEnergyRecover / 100);
            }
            if (nowMP + num <= maxMP)
            {
                nowMP += num;
            }
            else
            {
                nowMP = maxMP;
            }
        }
    }*/
}
