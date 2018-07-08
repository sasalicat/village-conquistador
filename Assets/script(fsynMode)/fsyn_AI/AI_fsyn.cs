using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AI_fsyn {
    GameObject traget
    {
        set;
        get;
    }
     void onUpdate();
     void onInit(Controler control);
}
