using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class additiondele {
    public delegate void withPos(Vector2 pos);
    public delegate void withaddMis(addibleMissile traget,bool original);
    public delegate void withTraget(GameObject traget,addibleMissile self);
    public delegate void withDamage(damage damage,addibleMissile self);
    public delegate void withDictionary(Dictionary<string, object> arg);
}
