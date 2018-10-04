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
    private System.Random random;
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
            skBut.onButtomClick += onSkillButtomDown;
        }
        random = new System.Random();
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
        Debug.Log("Skill Buttom:"+buttomCode);
        Dictionary<string, object> order = new Dictionary<string, object>();
        Vector3 temp = rocker.LastRelativePos;
        temp.y=-temp.y;
        temp =  Quaternion.Euler(0,0,180)*temp;
        //temp.x = -temp.x;
        order["MousePosition"] = (controler.transform.position+ temp);
        order["PlayerPosition"] = controler.transform.position;
        order["randomPoint"] = random.Next(0,99);
        order["code"] = (sbyte)buttomCode;
        Sender.main.addOrder(order);
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            onSkillButtomDown(CodeTable.MOUSE_LEFT_DOWN);
        }	
	}
}
