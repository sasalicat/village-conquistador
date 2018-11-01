using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneScript_mobile : sceneScript {
    skinRecord mainSkin = new skinRecord(0,0,0,0,0,0);
    public override void onStart(fsynManager_local manager)
    {
        Debug.Log("sceneScript_mobile onstart 被呼叫");
        //base.onStart(manager);
        ((mobile_fsyn_manager_local)manager).createMainRole(0,mainSkin,new List<int>{0},Vector2.zero,true);
        ((mobile_fsyn_manager_local)manager).createEnemy(0, new List<int> { 0 }, new Vector2(5, 5), "小混混", "你的末日", "normal_warrior", new enemy_lm_info(), 0);
    }
}
