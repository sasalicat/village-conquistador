using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerItem : MonoBehaviour {//用来记录本地玩家准备按钮状态的脚本
    public bool ready=false;//在update
    public RoomManager manager;
    public void onReadyClick()
    {
        manager.setReady(!ready);//设置ready为现在的相反值
    }
}
