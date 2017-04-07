namespace KBEngine
{

    using UnityEngine;
    using System.Collections;


    public class Avatar : Entity
    {
        public Avatar():base()
        {
            
        }
        public override void __init__()
        {
            Debug.Log("Avatar"+id+" S.T.A.R.T!!! 被创造");
        }
    }
}
