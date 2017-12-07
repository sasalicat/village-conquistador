using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomItem : MonoBehaviour {
    //用於記錄房間id和房間按鈕被按下通知hallmanager
    public int roomId;
    public HallManager manager;
    public bool gaming=false;//用於記錄房間是否在遊戲中
    public int num = 0;
    public void OnRoomClick()
    {
        if(num<6&&!gaming)//只有人數在合理範圍且沒正在進行遊戲的房間才能進入
            manager.enterRoom(this.roomId);

    }
}
