using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;
using System;
using UnityEngine.UI;


public class EquipmentList : MonoBehaviour {

    private delegate void reduce(float time);


    //主動裝備編號---------------------------------
    public const sbyte ATK = 0;
    public const sbyte SKILL = 1;
    public const sbyte PASSIVE1 = 2;
    public const sbyte PASSIVE2 = 3;
    public const sbyte PASSIVE3 = 4;
    public const sbyte PASSIVE4 = 5;
    public const sbyte PASSIVE5 = 6;

    public List<Equipment> equipments=new List<Equipment>();
    public List<CDEquipment> passiveEquipments = new List<CDEquipment>();
    private reduce reduceLine;
    public EquipmentTable table;
    public dataRegister register;
    public KBControler controler;
    //public Text text;
    private bool InitTime = true;

   void Start()
    {
        table = GameObject.Find("keyTabel").GetComponent<EquipmentTable>();
        register = GameObject.Find("client").GetComponent<dataRegister>();
        //text = GameObject.Find("Canvas/Text").GetComponent<Text>();
        AddEquipments();
        //text.text = "完成start";
        //Debug.Log("完成start");

    }
    void Update()
    {
        MissileTable table = GameObject.Find("keyTabel").GetComponent<MissileTable>();
        RoleState state = GetComponent<RoleState>();
        AnimatorTable anim=GetComponent<AnimatorTable>();
        if (InitTime)
        {
            for(int i = 0; i < equipments.Count; i++)
            {
                equipments[i].onInit(table,state,anim);
            }
            InitTime = false;
        }
    }
    public void AddEquipments()//在NetManager裏面被呼叫以至於賦值順序不混亂,主要是controler.Entity
    {
        Debug.Log("ADDing-----");
        //text.text = "add";
        //text.text = "Player is:"+ controler.Entity.ToString();
        int selfNo = ((Player)controler.Entity).roomNo;
        List<sbyte> eList = register.PlayerInWar[selfNo].role.equipmentIdList;
        while (eList.Count > 0)
        {
            Debug.Log("elist do");
            addByNo(eList[0]);
            eList.RemoveAt(0);
        }
        //目前版本不再註冊控制器,直接由controler觸發
        /*for (int i = 0; i < passiveEquipments.Count; i++)
        {//註冊主動裝備到控制器
            switch (i){
                case 0:
                    {
                        _on_left_down temp = controler.get_on_left_down();
                        //temp += passiveEquipments[i].passiveSkill;
                        break;
                    }
                case 1:
                    {
                        _on_right_down temp = controler.get_on_right_down();
                        //temp += passiveEquipments[i].passiveSkill;
                        break;
                    }
                case 2:
                    {
                        _on_key1_down temp = controler.get_on_key1_down();
                        //temp += passiveEquipments[i].passiveSkill;
                        break;
                    }
                case 3:
                    {
                        _on_key2_down temp = controler.get_on_key2_down();
                        //temp += passiveEquipments[i].passiveSkill;
                        break;
                    }
                case 4:
                    {
                        _on_key2_down temp = controler.get_on_key2_down();
                        //temp += passiveEquipments[i].passiveSkill;
                        break;
                    }
                case 5:
                    {
                        _on_key3_down temp = controler.get_on_key3_down();
                        //temp += passiveEquipments[i].passiveSkill;
                        break;
                    }
                case 6:
                    {
                        _on_key4_down temp = controler.get_on_key4_down();
                        //temp += passiveEquipments[i].passiveSkill;
                        break;
                    }
                case 7:
                    {
                        _on_key5_down temp = controler.get_on_key5_down();
                        //temp += passiveEquipments[i].passiveSkill;
                        break;
                    }
            }
            
        }*/
       
        //text.text += "+1";
    }
    private void addByNo(int EquipmentNo)
    {
        string typeName = table.equipmentNameList[EquipmentNo];

        Component newone= gameObject.AddComponent(System.Type.GetType(typeName));
        if (table.passiveList[EquipmentNo])//主動道具
        {
            passiveEquipments.Add((CDEquipment)newone);
        }
        //[判断是不是CDEquiment,如果是加入技能冷却列表
        Type[] inters = newone.GetType().GetInterfaces();
        Debug.Log("type length"+inters.Length);

        bool isCDE=false;
        for(int i = 0; i < inters.Length; i++)
        {
            Debug.Log("i type:" + inters[i] + "type:" + Type.GetType("CDEquipment"));
            if (inters[i] == Type.GetType("CDEquipment"))
            {
                isCDE = true;
                break;
            }
        }
        if (isCDE)
        {
            Debug.Log(newone.name+"isCDE");
            reduceLine += ((CDEquipment)newone).setTime;
        }

        equipments.Add((Equipment)newone);

        ((Equipment)newone).selfIndex = ((sbyte)(equipments.Count - 1));

        switch (((Equipment)newone).Kind) {
            case EquipmentTable.ON_TAKE_DAMAGE:
                {
                    //_on_take_damage t= controler.get_on_take_damage();
                    controler.On_Take_Damage+= ((Equipment)newone).trigger;
                    //Debug.Log("t have" + t.GetInvocationList().Length);
                    break;
                }
            case EquipmentTable.ON_INTERVAL:
                {
                    controler.On_Interval+= ((Equipment)newone).trigger;
                    break;
                }
            case EquipmentTable.ON_BEEN_TREAT:
                {
                    controler.On_Been_Treat+= ((Equipment)newone).trigger;
                    break;
                }
            case EquipmentTable.ON_HP_CHANGE:
                {
                    controler.On_Hp_Change += ((Equipment)newone).trigger;
                    break;
                }
        }

    }
    public void allReduceCD(float time)
    {
        reduceLine(time);
    }
}
