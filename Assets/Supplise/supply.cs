using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void beused(supply self);

public interface supply {
    beused beusedCB
    {
        set;
        get;
    }
}
