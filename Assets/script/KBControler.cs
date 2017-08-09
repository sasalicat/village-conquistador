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
    void addOrder(Dictionary<string, object> item);
    void addTriggerOrder(sbyte eIndex,Dictionary<string,object> args);
    void addEvent(sbyte code, Dictionary<string, object> args);
    //給netrolestate使用的方法-----------------
    void Role_onTakeDamage(damage damage);
    void Role_onBeenTreat(GameObject treater, int num);
    void onAttack();
    void synchroPos();

}
