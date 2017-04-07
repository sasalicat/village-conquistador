using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;
using UnityEngine.UI;

public class RenameButton : MonoBehaviour {
    public HallManager hm;
    public InputField inputf;
    public GameObject nameLabel;
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
        nameLabel.SetActive(false);
    }
}
