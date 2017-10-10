namespace KBEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;

    public class Point : Entity
    {
        public void destoryself(supply s)
        {
            cellCall("notifyAbate", new object[] { });
        }

    }
}
