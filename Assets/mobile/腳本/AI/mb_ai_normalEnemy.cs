using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mb_ai_normalEnemy : MonoBehaviour, AI_fsyn
{
    private enemyControler_mobile control;
    public float range = 10;
    public GameObject traget
    {
        set
        {
            tg = value;
        }
        get
        {
            return tg;
        }
    }
    private GameObject tg;
    public EquipmentList elist;
    private bool uping = false;
    private bool righting = false;
    private bool downing = false;
    private bool lefting = false;
    private Dictionary<string, object> virtualArg = new Dictionary<string, object>();
    public bool moveWilling = false;
    public void onInit(Controler control)
    {
        this.control = (enemyControler_mobile)control;
        this.elist = this.control.GetComponent<EquipmentList>();
    }

    // Use this for initialization

    public void onUpdate()
    {
        if (traget == null)//先這樣湊合測試下
        {//但實際上目標應該由manager分配
            traget = ((mobile_fsyn_manager_local)fsynManager_local.main).mainRole;
        }
        if (traget != null)
        {
            transform.right = -(traget.transform.position - transform.position);
        }
        if (((Vector2)traget.transform.position - (Vector2)transform.position).magnitude > range)
        {
            control.wantMove = true;
        }
        else
        {
            control.wantMove = false;
            Debug.Log("距離達到");
            virtualArg["MousePosition"] = traget.transform.position;
            virtualArg["PlayerPosition"] = control.transform.position;
            virtualArg["randomPoint"] = control.random.Next(0, 99);
            virtualArg["Traget"] = traget;
            virtualArg["TragetPosition"] = traget.transform.position;
            print("e has " + elist.nowHarness.passiveEquipments.Count + "equipment");
            foreach (CDEquipment e in elist.nowHarness.passiveEquipments)
            {

                print("time left" + e.TimeLeft);
                if (e.CanUse)
                {
                    Debug.Log(e+"被觸發!");
                    e.trigger(virtualArg);
                }
            }
        }
    }
}
