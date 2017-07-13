using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_lr_skill : Missile {
    public SpriteRenderer s;
    public bool local;

    // Use this for initialization
    void Start()
    {
        s = gameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        if (local = Creater.GetComponent<NetRoleState>().islocal)
        {
            s.color = new Vector4(1, 1, 1,0.5f);
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
