namespace KBEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Player : Entity
    {
        public NetManager manager;
        public override void __init__()
        {
            Debug.Log("KB: player init!!!");
            KBEngine.Event.fireOut("PlayerInit", new object[] {  this });
        }

    }
}
