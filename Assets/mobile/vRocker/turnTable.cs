using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class turnTable : MonoBehaviour {
    private readonly Vector2 offsetVector = new Vector2(1.7f, 2.4f);
    private bool lastFrameClicking=false;
    private bool clicking=false;
    private Vector2 mouseOffset;
    public GameObject AlignmentPraf;//手動拉取,準線
    private GameObject realAlig = null;
    public delegate void onSkillTrigger(Dictionary<string,Object> args);
    // Use this for initialization
    public const float centerRadiu = 5;
    public readonly Vector2 StartAnix = new Vector2(Mathf.Cos(112.5f*Mathf.Deg2Rad),Mathf.Sin(112.5f*Mathf.Deg2Rad));
    public  readonly List<Vector2> buttomBorder = new List<Vector2>() {new Vector2(0,45f),new Vector2(45,90),new Vector2(90,135),new Vector2(135,180),new Vector2(180,225) };
    public static turnTable main=null;

    public NetRoleState state;
    public fsynControler controler;
    public EquipmentList eList;
    private readonly List<int> BUTTOM_CODE = new List<int>() {CodeTable.MOUSE_LEFT_DOWN,CodeTable.MOUSE_RIGHT_DOWN,CodeTable.KEY1_DOWN,CodeTable.KEY2_DOWN,CodeTable.KEY3_DOWN};
    //public List<onSkillTrigger> skillLog;
    void Start () {
        Vector2 rightDown = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0));
        Debug.Log("左下角坐標:" + rightDown);
        transform.position = rightDown + offsetVector * transform.localScale.x;
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
	// Update is called once per frame
	void Update () {
        if (clicking)
        {
            if (!lastFrameClicking)//按下
            {
                Dictionary<string, object> arg = new Dictionary<string, object>();
                arg["code"] = CodeTable.SET_MOVE_STATE;
                arg["state"] = false;
                Sender.main.addOrder(arg);
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
                // Debug.Log("相對位置:"+(Input.mousePosition - transform.position));
            }
            Vector2 mouseOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition )- transform.position;
            float range = mouseOffset.magnitude;
            float angle = Vector2.Angle(mouseOffset, StartAnix);
            if (range > centerRadiu) {//在中心半徑外
                //Debug.Log("進入範圍 range為:"+range+"centerRadiu"+centerRadiu);
                for (int i=0;i<buttomBorder.Count;i++)
                {
                    Vector2 border = buttomBorder[i];
                    if (angle >= border.x && angle < border.y)
                    {
                        mobileListener.main.onSkillButtomDown(BUTTOM_CODE[i]);
                    }
                }
                //Debug.Log("當前角度為:" + angle);
            }
            

                
        }
        else
        {
            if (lastFrameClicking) {//鬆開
                Debug.Log("點離開");
                //text.text = "手指離開";
                Dictionary<string, object> arg = new Dictionary<string, object>();
                arg["code"] = CodeTable.SET_MOVE_STATE;
                arg["state"] = true;
                Sender.main.addOrder(arg);
                clicking = false;
                if (realAlig != null)
                {
                    realAlig.SetActive(false);
                }
            }
        }
        lastFrameClicking = clicking;


	}
    public void onUp() {
        Debug.Log("轉盤鬆開");
        clicking = false;
    }
    public  void onDown()
    {
        Debug.Log("轉盤被按下");
        clicking = true;
    }
}
