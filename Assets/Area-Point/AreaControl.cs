using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;

public class AreaControl : MonoBehaviour {
    Entity e;
    void OnCollisionEnter2D(Collision2D other)
    {
        if (e != null)
        {
            e.baseCall("EnterArea", new object[] { other.gameObject.GetComponent<NetRoleState>().roomNo});
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (e != null)
        {
            e.baseCall("ExitArea", new object[] { other.gameObject.GetComponent<NetRoleState>().roomNo });
        }
    }



}
