using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loading : MonoBehaviour {
    public GameObject[] items=new GameObject[6];
    private dataRegister regist;
    public List<sbyte> finishLine = new List<sbyte>();
    public Sprite readyIcon;
    private bool[] finishTable = new bool[NetManager.MAX_NUM];
    // Use this for initialization
    private bool checkFinish()//如果所有玩家都完成加载回传true.用于储存其他client(包括自己)是否加载完成
    {
        bool allFinish = true;//旗标,只要有一个玩家没有准备就会被设置为false
        for (int i = 0; i < NetManager.MAX_NUM; i++)
        {
            if (regist.PlayerInWar[i] != null)
            {
                if (!finishTable[i])
                {
                    allFinish = false;
                    break;
                }
            }
        }
        return allFinish;
    }
    void Start () {

        for(int i = 0; i < NetManager.MAX_NUM; i++)
        {
            finishTable[i] = false;
        }
        regist = GameObject.Find("client").GetComponent<dataRegister>();
        for(int i = 0; i < regist.PlayerInWar.Length; i++)
        {
            if (regist.PlayerInWar[i] != null)
            {
                items[i].transform.Find("name").GetComponent<Text>().text = regist.PlayerInWar[i].name;
                items[i].SetActive(true);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        while (finishLine.Count > 0)
        {
            items[finishLine[0]].transform.Find("readyLabel").GetComponent<Image>().sprite = readyIcon;
            finishTable[finishLine[0]] = true;
            finishLine.RemoveAt(0);
        }
        if (checkFinish())//如果所有玩家都准备好了,则隐藏加载页面
        {
            gameObject.SetActive(false);
        }
	}
}
