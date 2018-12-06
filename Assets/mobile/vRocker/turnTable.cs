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
    // Use this for initialization
    void Start () {
        Vector2 rightDown = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0));
        Debug.Log("左下角坐標:" + rightDown);
        transform.position = rightDown + offsetVector * transform.localScale.x;
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
