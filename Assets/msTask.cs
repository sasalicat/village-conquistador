using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;
using UnityEngine.UI;

public class msTask : MonoBehaviour {
    public float timeCount = 0;
    public int ReqTime = -1;
    public int oriTime = 0;
    public Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }
	// Update is called once per frame
	void Update () {
        timeCount += Time.deltaTime;
        if (timeCount >= 2)
        {
            timeCount = 0;
            oriTime = System.DateTime.Now.Millisecond;
            KBEngineApp.app.player().baseCall("msTask",new object[]{ });
        }
        if (ReqTime > 0)
        {
            text.text = (ReqTime - oriTime).ToString();
            ReqTime = -1;
        }
	}

}
