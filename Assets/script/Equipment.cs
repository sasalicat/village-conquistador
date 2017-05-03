using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Equipment {
    sbyte selfIndex
    {
        set;
        get;
    }
    sbyte No
    {
        get;
    }
    void trigger(Dictionary<sbyte,object> args);

	
}
