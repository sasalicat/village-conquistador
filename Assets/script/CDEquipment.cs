﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CDEquipment:Equipment{//主動裝備都要繼承這個介面
    void getTime(float time);//減少CD值,float為減少的時間
    uint Consumption//使用一次消耗的能量
    {
        get;
    }
    bool CanUse//主動裝備是否冷卻完畢,因為動畫控制要確認
    {
        get;
    }
	
}