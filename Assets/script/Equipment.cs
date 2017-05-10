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
    sbyte Kind
    {
        get;
    }
    void trigger(Dictionary<string,object> args);

	
}
