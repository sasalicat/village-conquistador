using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getVector : MonoBehaviour {
    GameObject staticPraf;
    public GameObject prefab;
    // Use this for initialization

    
    public Vector3 getOriginalInitPoint(Vector3 oriRolePos, Vector3 mousePos, Vector3 localMissitlePos)
    {
        Debug.Log("in get oripos is" + oriRolePos + "mouse is" + mousePos + "localpos is" + localMissitlePos); 
        if (staticPraf == null)
        {
            staticPraf = Instantiate(prefab, new Vector3(50, 50, 0), Quaternion.EulerAngles(0, 0, 0));
        }
        staticPraf.transform.up = -(mousePos-oriRolePos);
        //Vector3 local=transform.TransformPoint(new Vector3(0,-1,0))-transform.position;
        //Debug.Log("local is" +local+"transfromdirection is"+ transform.TransformPoint(new Vector3(0, -1, 0)));

        //return oriRolePos + local;
        Debug.Log("local Vector is"+ staticPraf.transform.TransformVector(localMissitlePos));
        return oriRolePos + staticPraf.transform.TransformVector(localMissitlePos);
     }
}
