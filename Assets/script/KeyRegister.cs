using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRegister : MonoBehaviour {
    public Dictionary<string, KeyCode> keySetting;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        keySetting = new Dictionary<string,KeyCode>();
        keySetting["up"] = KeyCode.UpArrow;
        keySetting["left"] = KeyCode.LeftArrow;
        keySetting["right"] = KeyCode.RightArrow;
        keySetting["down"] = KeyCode.DownArrow;
        keySetting["key1"] = KeyCode.Q;
        keySetting["key2"] = KeyCode.E;
        keySetting["key3"] = KeyCode.R;
        keySetting["key4"] = KeyCode.F;
        keySetting["key5"] = KeyCode.LeftShift;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
