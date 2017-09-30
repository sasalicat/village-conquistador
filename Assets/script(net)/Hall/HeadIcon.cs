using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadIcon : MonoBehaviour {
    public dataRegister register;
    public IconStorage storage;
    public Image head;
    public Text name;
    public Text Level; 
    public GameObject roleTable;

    public void onHeadClick()
    {
        roleTable.SetActive(true);
    }

	// Use this for initialization
	void Start () {
        register = GameObject.Find("client").GetComponent<dataRegister>();
        storage = GameObject.Find("Icons").GetComponent<IconStorage>();
        //Debug.Log("head:" + head + "storage:" + storage+"headIcon:"+storage.headIcon+"Icon:"+ storage.headIcon[register.roleNo]);
        head.sprite = storage.headIcon[register.roleNo];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
