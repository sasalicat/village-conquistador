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
    //給netrolestate使用的方法-----------------
    void Role_onTakeDamage(damage damage);
    void onTakeDamage(Vector3 selfPos, Vector3 selfEuler, Vector3 damagerPos,sbyte damagerNo, damage damage,sbyte randomInt);
    void onAttack();

}
