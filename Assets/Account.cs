
namespace KBEngine
{
    
    using UnityEngine;
    using System.Collections;
   

    public class Account : Entity
    {

       public override void __init__()
        {
            KBEngine.Event.fireOut("onLoginSuccessfully",new object[] {KBEngineApp.app.entity_uuid, id, this });
            KBEngine.Event.fireOut("taskEvent",new object[] {10});
          
        }
        public void reqHello()
        {
            baseCall("reqHello");
        }
        public void onHello(string data)
        {
            Event.fireOut("onHello", new object[] { data });
        }
    }
}