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
    void addEvent(sbyte code, Dictionary<string, object> args);
    //給netrolestate使用的方法-----------------
    void Role_onTakeDamage(damage damage);
   
    void onAttack();

}
