using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadingShow : MonoBehaviour {
    public GameObject[] showItem = new GameObject[6];
    public Sprite[] readyIcon;
    public List<int> readyHandle=new List<int>();//有要改變readyLabel時把索引值加入這裡
    private dataRegister register;
	// Use this for initialization
	void Start () {
        register=GameObject.Find("client").GetComponent<dataRegister>();
        for(int i = 0; i < 6; i++)
        {
            if (register.PlayerInWar != null)
            {
                showItem[i].SetActive(true);
            }
            showItem[i].transform.Find("name").GetComponent<Text>().text = register.PlayerInWar[i].name;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (readyHandle.Count > 0)
        {
            showItem[readyHandle[0]].transform.Find("readyLabel").GetComponent<Image>().sprite=readyIcon[1];//設置圖片為加載完成(綠色)
            readyHandle.RemoveAt(0);
        }
	}
}
