using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;

public class AreaControl : MonoBehaviour {
    public Entity e;
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("enter area");
        if (e != null)
        {
            NetRoleState state= other.gameObject.GetComponent<NetRoleState>();
            sbyte roomNo = state.roomNo;
            Debug.Log("e:"+e+" state:"+state.name+" roomNo:"+ state.roomNo);
            e.cellCall("EnterArea", new object[] {roomNo});
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (e != null)
        {
            e.cellCall("ExitArea", new object[] { other.gameObject.GetComponent<NetRoleState>().roomNo });
        }
    }



}
