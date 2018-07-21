using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class no_range_limit : MonoBehaviour, AI_fsyn
{
    private enemyControler control;
    public const float wanderTime = 1;
    public float timeLeft = 0;
    public float waitTime=0;
    public Vector2 point;
    public System.Random random;
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
    public void reduceTime(float time)
    {
        float before = waitTime;
        waitTime -= time;
        if (waitTime <= 0 )
        {
            if (before > 0)
                timeLeft = wanderTime;
            else
                timeLeft -= time;

        }

    }
    public void onInit(Controler control)
    {
        this.control = (enemyControler)control;
        this.elist = this.control.GetComponent<EquipmentList>();
        Timer.main.logInTimer(reduceTime);
        random = new System.Random(control.Index);
    }

    public void onUpdate()
    {
        //Debug.Log("traget:" + traget + "timeLeft:" + timeLeft);
        if (traget && timeLeft <= 0)
        {
            control.setDirection(traget.transform.position);
            transform.up = -(traget.transform.position - control.transform.position);
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
            if (!elist.nowHarness.passiveEquipments[0].CanUse)
            {
                return;
            }
            virtualArg["MousePosition"] = traget.transform.position;
            virtualArg["PlayerPosition"] = control.transform.position;
            virtualArg["randomPoint"] = control.random.Next(0, 99);
            virtualArg["Traget"] = traget;
            virtualArg["TragetPosition"] = traget.transform.position;

            elist.nowHarness.passiveEquipments[0].trigger(virtualArg);
            waitTime=((skill_accum)elist.nowHarness.passiveEquipments[0]).AccumTime;
            float angle = random.Next(0, 361);
            point = Quaternion.Euler(0,0,angle)*Vector2.up;

        }
            else
            {
                var angleY = Vector2.Angle(Vector2.up, point);
                if (angleY <= 22.5)//是否方向由+y管
                {//鬆開其他鍵按下上鍵
                    transform.up = -Vector2.up;
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
                    transform.up = Vector2.up;
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
                    
                    var angleX = Vector2.Angle(Vector2.left, point);
                    if (angleX <= 22.5)
                    {
                        transform.up = -Vector2.left;
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
                        transform.up = Vector2.left;
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
                        var angleXY = Vector2.Angle(xy,point);
                        if (angleXY <= 22.5)
                        {
                            transform.up = -xy;
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
                        transform.up = xy;
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
                            Vector2 yx = new Vector2(-1, -1);
                            var angleYX = Vector2.Angle(yx,point);
                            if (angleYX <= 22.5)
                            {
                                transform.up = -yx;
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
                                transform.up = yx;
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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    protected void OnDestroy()
    {
        Timer.main.loginOutTimer(reduceTime);
    }  
}
