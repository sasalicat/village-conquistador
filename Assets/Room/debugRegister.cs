using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class debugRegister : MonoBehaviour {
    public Text text;
    public List<Text> texts;
	// Use this for initialization
	void  OnEnable() {
        while (texts.Count>0)
        {
            Debug.Log("刪除:"+texts[0].text);
            Destroy(texts[0]);
            texts.RemoveAt(0);
        }
        dataRegister register = GameObject.Find("client").GetComponent<dataRegister>();
        for (int i=0;i< register.PlayerInWar.Length;i++)
        {
            if (register.PlayerInWar[i] != null) {
                Text newone = (Text)Instantiate(text, this.transform);
                newone.text = i + ": " + register.PlayerInWar[i].name;
                Debug.Log("添加:" + newone.text);
                texts.Add(newone);
            }

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
