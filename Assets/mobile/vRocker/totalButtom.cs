using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class totalButtom : MonoBehaviour {
    //public Text text; 
    delegate void emptyFun();
    private readonly Vector2 width_height = new Vector2(3.4f, 4.8f);
    private readonly Vector2 offsetVector = new Vector2(1.7f, 2.4f);
    emptyFun onPointerEnter;
    emptyFun onPointerExit;
    public bool clicking=false;
    public Text enterB;
    public int enterNum = 0;
    public Text LeaveB;
    public int leaveNum = 0;
    public GameObject AlignmentPraf;//手動拉取,準線
    private GameObject realAlig=null;
    public void onDown()
    {
        Debug.Log("按鈕被按下");
    }
    public void onUp()
    {
        Debug.Log("按鈕被鬆開");
    }
    public void onSubExit()
    {
        if (clicking)
        {

        }
        else
        {
            Dictionary<string, object> arg = new Dictionary<string, object>();
            arg["code"] = CodeTable.SET_MOVE_STATE;
            arg["state"] = true;
            Sender.main.addOrder(arg);
            leaveNum++;
            LeaveB.text = leaveNum + "";
            clicking = false;
            if (realAlig != null)
            {
                realAlig.SetActive(false);
            }
        }
    }
    public void onEnter()
    {
        Debug.Log("點進入");
        //text.text += "main";
        //text.text = "手指進入";
        if (!clicking)
        {
            Dictionary<string, object> arg = new Dictionary<string, object>();
            arg["code"] = CodeTable.SET_MOVE_STATE;
            arg["state"] = false;
            Sender.main.addOrder(arg);
            enterNum++;
            enterB.text = enterNum + "";
            clicking = true;
            GameObject role = ((mobile_fsyn_manager_local)mobile_fsyn_manager_local.main).mainRole;
            if (realAlig == null)
            {
                realAlig = Instantiate(AlignmentPraf, role.transform.position, role.transform.rotation, role.transform);
                realAlig.transform.Rotate(0, 0, -90);
                realAlig.transform.localScale = new Vector3(2, 3, 1);
            }
            else
            {
                realAlig.SetActive(true);
            }
        }
    }
    public void onExit()
    {
        if (clicking) {
            Debug.Log("點離開");
            //text.text = "手指離開";
            Dictionary<string, object> arg = new Dictionary<string, object>();
            arg["code"] = CodeTable.SET_MOVE_STATE;
            arg["state"] = true;
            Sender.main.addOrder(arg);
            leaveNum++;
            LeaveB.text = leaveNum + "";
            clicking = false;
            if (realAlig != null)
            {
                realAlig.SetActive(false);
            }
        }
    }
    public void onClick()
    {
        Debug.Log("大按鈕被按下"+ Camera.main.worldToCameraMatrix);


        //Camera.main.worldToCameraMatrix;
    }
	// Use this for initialization
	void Start () {
        Vector2 rightDown = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,0));
        Debug.Log("左下角坐標:"+rightDown);
        transform.position = rightDown + offsetVector*transform.localScale.x;
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
