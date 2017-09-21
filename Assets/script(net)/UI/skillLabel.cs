using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class skillLabel : MonoBehaviour {
    public Image icon;
    public Text record;
    public IconStorage storage;
    private bool cding=false;//上一幀技能是否是cd狀態
	// Use this for initialization
	void Start () {
       
        storage = GameObject.Find("Icons").GetComponent<IconStorage>();
        Debug.Log("skill Label 執行 start :"+storage);
	}
	public void Init(int no,float timeleft)
    {
        Debug.Log("storage is:" + storage);
        if(storage.skillIcon[no])
            icon.sprite = storage.skillIcon[no];
        if (timeleft > 0)
        {
            cding = true;
        }
        updateTime(timeleft);
    }
    public void updateTime(float timeLeft)
    {
        if (timeLeft > 0)
        {
            if (!cding)
            {
                Debug.Log("setColor");
                icon.color = new Color(0.3f, 0.3f, 0.3f);
                cding = true;
            }
            record.text = timeLeft + "";
            
        }
        else
        {
            if (cding)
            {
                icon.color = Color.white;
                record.text = "";
                cding = false;
            }
           
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
