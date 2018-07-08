using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class clientApp :KBEMain {
    public Text ipField;
    public void changeIp()
    {
        if (!(ipField.text == ""))
        {
            Debug.Log("ip is " + ipField.text);
            this.ip = ipField.text;
        }

    }

}
