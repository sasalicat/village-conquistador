using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class register : MonoBehaviour {
    public Text account;
    public Text password;
    public Text passward_confirm;
    public GameObject notice;
    // Use this for initialization
	void Start () {
        KBEngine.Event.registerOut("onCreateAccountResult", this, "onCreateAccountResult");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void justDoIt()
    {
        if (password.text == passward_confirm.text)
        {
            KBEngine.Event.fireIn("createAccount", account.text, password.text, System.Text.Encoding.UTF8.GetBytes("guass"));
        }
    }
    public void itCancel()
    {
        gameObject.SetActive(false);
    }
    public void itStart()
    {
        gameObject.SetActive(true);
    }
    public void onCreateAccountResult(UInt16 retcode, byte[] datas)
    {
        notice.SetActive(true);
        Text label = notice.transform.Find("Text").GetComponent<Text>();
        if (retcode == 0)
        {
            label.text = "恭喜你註冊成功,然後呢?";
        }
        else
        {
            label.text = "註冊失敗,錯誤代碼:" + retcode;
        }
    }
}
