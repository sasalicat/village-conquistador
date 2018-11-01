using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class buttomTest : MonoBehaviour {
    public delegate void withInt(int arg);
    public withInt onButtomClick;
   // public Text text;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void onclick(int buttomCode)
    {
        Debug.Log(name + "被點擊:"+ onButtomClick);
        //   text.text += name;
        if(onButtomClick != null)
            onButtomClick(buttomCode);

    }

}
