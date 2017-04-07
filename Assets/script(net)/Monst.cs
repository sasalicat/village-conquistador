namespace KBEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Monst : Entity
    {
        public override void __init__()
        {
            Debug.Log("Monst init!!! id is:" + id);
        }
        public override void set_position(object old)
        {
            Debug.Log("on setposition");
            if (renderObj != null)
            {
                base.set_position(old);
                ((EntityControl)renderObj).updatePosition(new Vector3());
            }
        }
    }
}