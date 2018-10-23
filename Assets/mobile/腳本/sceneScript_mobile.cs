using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneScript_mobile : sceneScript {
    skinRecord mainSkin = new skinRecord(0,0,0,0,0,0);
    public override void onStart(fsynManager_local manager)
    {
        Debug.Log("sceneScript_mobile onstart 被呼叫");
        //base.onStart(manager);
        ((mobile_fsyn_manager_local)manager).createMainRole(0,mainSkin,new List<int>{},Vector2.zero,true);
    }
}
