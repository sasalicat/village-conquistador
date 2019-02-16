using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobileListener : MonoBehaviour {
    public NetRoleState state;
    public fsynControler controler;
    public EquipmentList eList;
    public static mobileListener main;
    public Rocker rocker;//手動拉取
    public buttomTest[] skillButtoms;//手動拉取 
    public bool[] stateList = new bool[5] { false, false, false, false, false };//同一幀不可能施放兩次同個技能,所以需要這個表來確定這一幀是否施放瞭這個技能
    private System.Random random;
    private bool firstInit = true;
 
    readonly Dictionary<int, sbyte> buttom2skillNo = new Dictionary<int, sbyte> { {CodeTable.MOUSE_LEFT_DOWN,EquipmentList.ATK }, {CodeTable.MOUSE_RIGHT_DOWN,EquipmentList.SKILL},
        {CodeTable.KEY1_DOWN,EquipmentList.PASSIVE1}, { CodeTable.KEY2_DOWN,EquipmentList.PASSIVE2}, { CodeTable.KEY3_DOWN,EquipmentList.PASSIVE3}
    };
    public void refresh(float frameTime) {
       // Debug.Log("");
        for(int i = 0; i < stateList.Length; i++)
        {
            stateList[i] = false;
        }
    }
    void OnEnable()
    {
        if (main == null)
        {
            main = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        rocker.onRockerDragBegin += onRockerDragBeg;
        rocker.onRockerDrag += onRockerDraging;
        rocker.onRockerDragEnd += onRockerDragEnd;
        foreach(buttomTest skBut in skillButtoms)
        {
            //skBut.onButtomClick += onSkillButtomDown;
        }
        random = new System.Random();
        if (firstInit)
        {
            fsynManager_local.main.onFrameUpdateEnd += refresh;
            firstInit = false;
        }
    }
    public void onRockerDragBeg(Vector2 relatPos)
    {
        onRockerDraging(relatPos);
        Dictionary<string, object> arg = new Dictionary<string, object>();
        arg["code"] = CodeTable.SET_MOVE_STATE;
        arg["state"] = true;
        Sender.main.addOrder(arg);
    }
    public void onRockerDraging(Vector2 relatPos)
    {
        Dictionary<string, object> arg = new Dictionary<string, object>();
        arg["code"] = CodeTable.SET_MOUSE_POS;
        relatPos.x = -relatPos.x;
        arg["mousePosition"] = (Vector2)controler.transform.position + relatPos;
        Sender.main.addOrder(arg);
    }
    public void onRockerDragEnd(Vector2 relatPos)
    {
        Dictionary<string, object> arg = new Dictionary<string, object>();
        arg["code"] = CodeTable.SET_MOVE_STATE;
        arg["state"] = false;
        Sender.main.addOrder(arg);
    }
    public void onSkillButtomDown(int buttomCode)
    {
        //Debug.Log("Skill Buttom:"+buttomCode);
        if (!stateList[buttom2skillNo[buttomCode]])//判斷這個技能在這一幀是不是被使用過了,如果沒有被使用過才會產生order
        {
            if (state.canAction && controler.equipmentReady(buttom2skillNo[buttomCode]))
            {
                Debug.Log("角色能行動且技能" + buttom2skillNo[buttomCode] + "準備好了");
                Dictionary<string, object> order = new Dictionary<string, object>();
                Vector3 temp = rocker.LastRelativePos;
                temp.y = -temp.y;
                temp = Quaternion.Euler(0, 0, 180) * temp;
                //temp.x = -temp.x;
                order["MousePosition"] = (controler.transform.position + temp);
                order["PlayerPosition"] = controler.transform.position;
                order["randomPoint"] = random.Next(0, 99);
                order["code"] = (sbyte)buttomCode;
                Sender.main.addOrder(order);
            }
            stateList[buttom2skillNo[buttomCode]] = true;//因為同一幀內技能和角色狀態不可能改變,所以一幀之內第二次進入這個if時會和第一次得到一樣的結果(是否addOrder),所以在這裡而不是狀態判斷的if內設成true
        }
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            onSkillButtomDown(CodeTable.MOUSE_LEFT_DOWN);
        }	
	}
}
