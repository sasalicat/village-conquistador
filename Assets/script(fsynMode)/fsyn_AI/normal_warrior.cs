using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normal_warrior : MonoBehaviour,AI_fsyn {
    private enemyControler control;
    public float range=20;
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
    private bool uping=false;
    private bool righting = false;
    private bool downing = false;
    private bool lefting = false;
    private Dictionary<string, object> virtualArg = new Dictionary<string, object>();
    public void onInit(Controler control)
    {
        this.control = (enemyControler)control;
        this.elist = this.control.GetComponent<EquipmentList>();
    }

    public void onUpdate()
    {
        if (traget)
        {
            control.setDirection(traget.transform.position);
            transform.up = -(traget.transform.position - control.transform.position);
            if (((Vector2)traget.transform.position - (Vector2)transform.position).magnitude <= range)
            {
                if (downing)
                {
                    control.get_on_keydown_up()();
                    downing = false;
                }
                if (lefting)
                {
                    control.get_on_keyleft_up()();
                    lefting = false;
                }
                if (uping)
                {
                    control.get_on_keyup_up()();
                    uping = false;
                }
                if (righting)
                {
                    control.get_on_keyright_up()();
                    righting = false;
                }
                virtualArg["MousePosition"] = traget.transform.position;
                virtualArg["PlayerPosition"] = control.transform.position;
                virtualArg["randomPoint"] = control.random.Next(0, 99);
                virtualArg["Traget"] = traget;
                virtualArg["TragetPosition"] = traget.transform.position;
                foreach (CDEquipment e in elist.nowHarness.passiveEquipments)
                {
                    if(e.CanUse)
                        e.trigger(virtualArg);
                }
            }
            else
            {
                var to = traget.transform.position;
                var from = transform.position;
                var angleY = Vector2.Angle(Vector2.up, to - from);
                if (angleY <= 22.5)//是否方向由+y管
                {//鬆開其他鍵按下上鍵

                    if (downing)
                    {
                        control.get_on_keydown_up()();
                        downing = false;
                    }
                    if (lefting)
                    {
                        control.get_on_keyleft_up()();
                        lefting = false;
                    }
                    if (righting)
                    {
                        control.get_on_keyright_up()();
                        righting = false;
                    }
                    if (!uping)
                    {
                        control.get_on_keyup_down()();
                        uping = true;
                    }
                }
                else if (angleY > 157.5)//是否方向由+y管
                {//鬆開其他鍵按下上鍵
                    if (uping)
                    {
                        control.get_on_keyup_up()();
                        uping = false;
                    }
                    if (lefting)
                    {
                        control.get_on_keyleft_up()();
                        lefting = false;
                    }
                    if (righting)
                    {
                        control.get_on_keyright_up()();
                        righting = false;
                    }
                    if (!downing)
                    {
                        control.get_on_keydown_down()();
                        downing = true;
                    }
                }
                else
                {
                    var angleX = Vector2.Angle(Vector2.left, to - from);
                    if (angleX <= 22.5)
                    {
                        Debug.Log("進入+x");
                        Debug.Log("狀態 up:" + uping + "left:" + lefting + "down:" + downing + "right:" + righting);
                        if (uping)
                        {
                            control.get_on_keyup_up()();
                            uping = false;
                        }
                        if (lefting)
                        {
                            control.get_on_keyleft_up()();
                            lefting = false;
                        }
                        if (!righting)
                        {
                            Debug.Log("按下右");
                            control.get_on_keyright_down()();
                            righting = true;
                        }
                        if (downing)
                        {
                            control.get_on_keydown_up()();
                            downing = false;
                        }
                    }
                    else if (angleX > 157.5)
                    {
                        Debug.Log("進入-x");
                        if (uping)
                        {
                            control.get_on_keyup_up()();
                            uping = false;
                        }
                        if (!lefting)
                        {
                            control.get_on_keyleft_down()();
                            lefting = true;
                        }
                        if (righting)
                        {
                            control.get_on_keyright_up()();
                            righting = false;
                        }
                        if (downing)
                        {
                            control.get_on_keydown_up()();
                            downing = false;
                        }
                    }
                    else
                    {
                        Vector2 xy = new Vector2(-1, 1);
                        var angleXY = Vector2.Angle(xy, to - from);
                        if (angleXY <= 22.5)
                        {
                            if (!uping)
                            {
                                control.get_on_keyup_down()();
                                uping = true;
                            }
                            if (lefting)
                            {
                                control.get_on_keyleft_up()();
                                lefting = false;
                            }
                            if (!righting)
                            {
                                control.get_on_keyright_down()();
                                righting = true;
                            }
                            if (downing)
                            {
                                control.get_on_keydown_up()();
                                downing = false;
                            }
                        }
                        else if (angleXY > 157.5)
                        {
                            if (uping)
                            {
                                control.get_on_keyup_up()();
                                uping = false;
                            }
                            if (!lefting)
                            {
                                control.get_on_keyleft_down()();
                                lefting = true;
                            }
                            if (righting)
                            {
                                control.get_on_keyright_up()();
                                righting = false;
                            }
                            if (!downing)
                            {
                                control.get_on_keydown_down()();
                                downing = true;
                            }
                        }
                        else
                        {
                            Vector2 yx = new Vector2(-1,-1);
                            var angleYX = Vector2.Angle(yx, to - from);
                            if (angleYX <= 22.5)
                            {
                                if (uping)
                                {
                                    control.get_on_keyup_up()();
                                    uping = false;
                                }
                                if (lefting)
                                {
                                    control.get_on_keyleft_up()();
                                    lefting = false;
                                }
                                if (!righting)
                                {
                                    control.get_on_keyright_down()();
                                    righting = true;
                                }
                                if (!downing)
                                {
                                    control.get_on_keydown_down()();
                                    downing = true;
                                }
                            }
                            else if (angleYX > 157.5)
                            {
                                //Debug.Log("進入左上");
                                if (!uping)
                                {
                                    control.get_on_keyup_down()();
                                    uping = true;
                                }
                                if (!lefting)
                                {
                                    control.get_on_keyleft_down()();
                                    lefting = true;
                                }
                                if (righting)
                                {
                                    control.get_on_keyright_up()();
                                    righting = false;
                                }
                                if (downing)
                                {
                                    control.get_on_keydown_up()();
                                    downing = false;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
