using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class additiondele {
    public delegate void withPos(Vector2 pos);
    public delegate void withaddMis(addibleMissile traget,bool original);
    public delegate void withTraget(GameObject traget);
    public delegate void withDamage(damage damage);
    public delegate void withDictionary(Dictionary<string, object> arg);
}
