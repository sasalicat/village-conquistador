using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobile_fsyn_manager_local : fsynManager_local {

    private void sameAsPrafeb(GameObject newone,GameObject ItsPrafeb)
    {
        newone.transform.localPosition = ItsPrafeb.transform.position;
        newone.transform.localScale = ItsPrafeb.transform.localScale;
        newone.transform.localRotation = ItsPrafeb.transform.localRotation;

    }
    public virtual void createMainRole(skinRecord skin,List<int> eList,Vector2 pos,bool mainrole)
    {
        part_Table pTable = part_Table.main;
        Debug.Log("創建了主要角色");
        Debug.Log("pTable:" + pTable);
        GameObject newRole = Instantiate(pTable.roleBase, pos, transform.rotation);
        GameObject hand_left= Instantiate(pTable.leftHands[skin.leftHandNo],newRole.transform);
        sameAsPrafeb(hand_left, pTable.leftHands[skin.leftHandNo]);

        GameObject right_hand = Instantiate(pTable.rightHands[skin.rightHandNo],newRole.transform);
        sameAsPrafeb(right_hand, pTable.rightHands[skin.rightHandNo]);

        GameObject face = Instantiate(pTable.faces[skin.faceNo], newRole.transform);
        sameAsPrafeb(face, pTable.faces[skin.faceNo]);

        GameObject eyes = Instantiate(pTable.eyes[skin.eyesNo], face.transform);
        sameAsPrafeb(eyes, pTable.eyes[skin.eyesNo]);

        GameObject mouse = Instantiate(pTable.mouses[skin.mouseNo], face.transform);
        Debug.Log("嘴預製體位置為:" + pTable.mouses[skin.mouseNo].transform.position);
        sameAsPrafeb(mouse, pTable.mouses[skin.mouseNo]);
    }
    /*public override void createMainRole(int rno, int roleKind, List<int> eList, Vector2 pos, bool mainrole)
    {
        Debug.Log("~~~~~~~~~~~~~~~~~~~~~呼叫了moblie的 createmainrole~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        GameObject nowRole = Instantiate(prabTable.table[roleKind], pos, transform.rotation);
        controlList[rno] = nowRole.AddComponent<mobile_pControler>();
        controlList[rno].Index = rno;
        var state = nowRole.AddComponent<NetRoleState>();
        nowRole.SetActive(true);
        var equiplist = nowRole.GetComponent<EquipmentList>();
        equiplist.controler = controlList[rno];
        equiplist.Start();
        hpBarCreater.CreateHpBar(nowRole, rno, "你", "超威藍貓");
        foreach (int index in eList)
        {
            equiplist.addByNo(index);
        }
        objList[rno] = nowRole;
        if (mainrole)
        {
            mobileListener.main.state = state;
            mobileListener.main.controler = controlList[rno];
            mobileListener.main.eList = equiplist;
        }
        playerPoors.Add(new OrderPoor(rno));
        Debug.Log("添加playerPoors后length為:" + playerPoors.Count);
    }*/
    protected override void handleOrder(fsynControler controler, Dictionary<string, object> order)
    {
        sbyte code = (sbyte)order["code"];
        switch (code)
        {
            case CodeTable.ADD_BUFF:
                {
                    controler.realAddBuff((sbyte)order["buffNo"]);
                    break;
                }
            case CodeTable.CONTORTION:
                {
                    controler.realControtions((int)order["distortionNo"]);
                    break;
                }
            case CodeTable.TAKE_DAMAGE:
                {
                    controler.realTakeDamage(order);
                    break;
                }
            case CodeTable.BEEN_TREAT:
                {
                    controler.realBeTreat(order);
                    break;
                }
            case CodeTable.INTERVAL:
                {
                    controler.takeInterval(order);
                    break;
                }
            case CodeTable.FRAME_END:
                {
                    controler.move(SINGLE_FRAME_TIME);
                    break;
                }
            case CodeTable.SET_MOUSE_POS:
                {
                    controler.setDirection((Vector2)order["mousePosition"]);
                    break;
                }
            case CodeTable.SET_MOVE_STATE:
                {
                    ((mobile_pControler)controler).MoveWillingness=((bool)order["state"]);
                    break;
                }
            case CodeTable.MOUSE_LEFT_DOWN:
            case CodeTable.MOUSE_RIGHT_DOWN:
            case CodeTable.KEY1_DOWN:
            case CodeTable.KEY2_DOWN:
            case CodeTable.KEY3_DOWN:
            case CodeTable.KEY4_DOWN:
            case CodeTable.KEY5_DOWN:
                {
                    controler.onSkillButtom(code, order);
                    break;
                }
        }
    }
}
