using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;
using UnityEngine.UI;

public class RenameButton : MonoBehaviour {
    public HallManager hm;
    public InputField inputf;
    public GameObject nameLabel;
    public Text name;
	// Use this for initialization
	void Start () {
        hm.showNameLabel = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void onButton()
    {
        ((Account)KBEngine.KBEngineApp.app.player()).baseCall("changeNickName",new object[] {inputf.text});
        name.text = inputf.text;//因為沒有反饋就用本地設置名字裝一下
        nameLabel.SetActive(false);
    }
}
