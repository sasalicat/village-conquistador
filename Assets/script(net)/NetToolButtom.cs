using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetToolButtom : ToolButtonListener {
    public GameObject leaveTabel;
    // Use this for initialization

    void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(keys.keySetting["ESC"]))
        {
            leaveTabel.SetActive(true);
        }
	}
}
