using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadIcon : MonoBehaviour {
    public dataRegister register;
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
        register = GameObject.Find("keyTabel").GetComponent<dataRegister>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
