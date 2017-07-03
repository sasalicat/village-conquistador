using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class musicControl : MonoBehaviour {
    public Text bottumText;
    public AudioSource source;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void onMusicClick()
    {
        if (source.enabled)
        {
            bottumText.text = "静音";
            source.enabled = false;
        }
        else
        {
            bottumText.text = "开启音乐";
            source.enabled = true;
        }
    }
}
