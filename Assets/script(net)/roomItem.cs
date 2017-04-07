using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomItem : MonoBehaviour {
    //用於記錄房間id和房間按鈕被按下通知hallmanager
    public int roomId;
    public HallManager manager;
    public void OnRoomClick()
    {
        manager.enterRoom(this.roomId);

    }
}
