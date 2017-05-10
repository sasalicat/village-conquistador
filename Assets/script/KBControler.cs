using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;

public interface KBControler : Controler{
    Entity Entity
    {
        set;
        get;
    }
    void addTriggerOrder(sbyte eIndex,Dictionary<string,object> args);

}
