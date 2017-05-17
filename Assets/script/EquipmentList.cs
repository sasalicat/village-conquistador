using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;
using UnityEngine.UI;


public class EquipmentList : MonoBehaviour {
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
    public EquipmentTable table;
    public dataRegister register;
    public KBControler controler;
    public Text text;

   void Start()
    {
        table = GameObject.Find("keyTabel").GetComponent<EquipmentTable>();
        register = GameObject.Find("client").GetComponent<dataRegister>();
        text = GameObject.Find("Canvas/Text").GetComponent<Text>();
        text.text = "完成start";
        Debug.Log("完成start");

    }
    public void AddEquipments()//在NetManager裏面被呼叫以至於賦值順序不混亂,主要是controler.Entity
    {
        Debug.Log("ADDing-----");
        text.text = "add";
        int selfNo = ((Player)controler.Entity).roomNo;
        List<sbyte> eList = register.PlayerInWar[selfNo].role.equipmentIdList;
        while (eList.Count > 0)
        {
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
       
        text.text += "+1";
    }
    private void addByNo(int EquipmentNo)
    {
        string typeName = table.equipmentNameList[EquipmentNo];
        Component newone= gameObject.AddComponent(System.Type.GetType(typeName));
        if (table.passiveList[EquipmentNo])//主動道具
        {
            passiveEquipments.Add((CDEquipment)newone);
        }
        equipments.Add((Equipment)newone);
    }
}
