using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobile_fsyn_manager_local : fsynManager_local {

    public GameObject mainRole;
    private void sameAsPrafeb(GameObject newone,GameObject ItsPrafeb)
    {
        newone.transform.localPosition = ItsPrafeb.transform.position;
        newone.transform.localScale = ItsPrafeb.transform.localScale;
        newone.transform.localRotation = ItsPrafeb.transform.localRotation;

    }
    public override GameObject createEnemy(int roleKind, List<int> eList, Vector2 pos, string name, string teamname, string AIname, enemyInfo info, int level)
    {
        int rno = enemyRecondNum++;
        GameObject nowRole = Instantiate(prabTable.table[roleKind], pos, transform.rotation);
        var controler = nowRole.AddComponent<enemyControler_mobile>();
        //Debug.Log("添加enemystart完成");
        var state = nowRole.AddComponent<enemyState>();
        nowRole.SetActive(true);

        var equiplist = nowRole.GetComponent<EquipmentList>();
        equiplist.controler = controlList[rno];
        equiplist.Start();
        //hpBarCreater.CreateHpBar(nowRole, rno, name, teamname);
        foreach (int index in eList)
        {
            equiplist.addByNo(index);
        }
        controler.random = new System.Random(rno);
        controler.Index = rno;
        if (AIname != null)
        {
            var ai = nowRole.AddComponent(System.Type.GetType(AIname));
            ((AI_fsyn)ai).onInit(controler);
        }
        nowRole.GetComponent<RoleState>().team = (sbyte)MAX_UNIT_NUM;
        enemyList[rno] = nowRole;
        controler.edata = info;
        controler.level = level;
        controler.onEnemyReady += unitInit;
        Debug.Log(controler + "的onEnemyReady:" + controler.onEnemyReady);
        return nowRole;
    }
    public virtual void createMainRole(int rno, skinRecord skin,List<int> eList,Vector2 pos,bool mainrole)
    {
        part_Table pTable = part_Table.main;
        Debug.Log("創建了主要角色");
        Debug.Log("pTable:" + pTable);
        GameObject newRole = Instantiate(pTable.roleBase, pos, transform.rotation);
        GameObject hand_left= Instantiate(pTable.leftHands[skin.leftHandNo],newRole.transform);
        sameAsPrafeb(hand_left, pTable.leftHands[skin.leftHandNo]);
        hand_left.SetActive(false);
        hand_left.name = "left";

        GameObject right_hand = Instantiate(pTable.rightHands[skin.rightHandNo],newRole.transform);
        sameAsPrafeb(right_hand, pTable.rightHands[skin.rightHandNo]);
        right_hand.SetActive(false);
        right_hand.name = "right";

        GameObject face = Instantiate(pTable.faces[skin.faceNo], newRole.transform);
        sameAsPrafeb(face, pTable.faces[skin.faceNo]);
        face.name = "face";

        GameObject eyes = Instantiate(pTable.eyes[skin.eyesNo], face.transform);
        sameAsPrafeb(eyes, pTable.eyes[skin.eyesNo]);
        eyes.name = "eyes";

        GameObject mouse = Instantiate(pTable.mouses[skin.mouseNo], face.transform);
        Debug.Log("嘴預製體位置為:" + pTable.mouses[skin.mouseNo].transform.position);
        sameAsPrafeb(mouse, pTable.mouses[skin.mouseNo]);
        mouse.name = "mouse";

        GameObject weapon = Instantiate(pTable.weapons[skin.weaponNo], newRole.transform);
        sameAsPrafeb(weapon, pTable.weapons[skin.weaponNo]);
        weapon.name = "weapon";

        Transform w_left = weapon.transform.Find("左手");
        w_left.GetComponent<Animator>().runtimeAnimatorController = pTable.leftHands[skin.leftHandNo].GetComponent<Animator>().runtimeAnimatorController;
        //sameAsPrafeb(w_left)
        //w_left.localScale = pTable.leftHands[skin.leftHandNo].transform.localScale;

        Transform w_right = weapon.transform.Find("右手");
        w_right.GetComponent<Animator>().runtimeAnimatorController = pTable.rightHands[skin.rightHandNo].GetComponent<Animator>().runtimeAnimatorController;
        //w_right.localScale = pTable.rightHands[skin.rightHandNo].transform.localScale;

        newRole.AddComponent<Anim_Mobile>();//等到部件全部安裝完再安裝Anim_Mobile

        fsynControler controler= newRole.AddComponent<mobile_pControler>();
        controlList[rno] = controler;

        controler.Index = rno;
        var state = newRole.AddComponent<NetRoleState>();
        newRole.SetActive(true);
        var equiplist = newRole.GetComponent<EquipmentList>();
        equiplist.controler = controlList[rno];
        equiplist.Start();
        foreach (int index in eList)
        {
            equiplist.addByNo(index);
        }
        objList[rno] = newRole;
        if (mainrole)
        {
            mobileListener.main.state = state;
            mobileListener.main.controler = controlList[rno];
            mobileListener.main.eList = equiplist;
            mainRole = newRole;
        }

        playerPoors.Add(new OrderPoor(rno));
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
