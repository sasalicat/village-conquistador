using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRegister : MonoBehaviour {
    public Dictionary<string, KeyCode> keySetting;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        keySetting = new Dictionary<string,KeyCode>();
        keySetting["up"] = KeyCode.W;
        keySetting["left"] = KeyCode.A;
        keySetting["right"] = KeyCode.D;
        keySetting["down"] = KeyCode.S;
        keySetting["key1"] = KeyCode.Q;
        keySetting["key2"] = KeyCode.E;
        keySetting["key3"] = KeyCode.R;
        keySetting["key4"] = KeyCode.F;
        keySetting["key5"] = KeyCode.LeftShift;
        keySetting["ESC"] = KeyCode.Escape;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
