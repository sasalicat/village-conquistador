using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;

public class NetPlayerControler : MonoBehaviour,Controler {

    Entity entity;
    void Update()
    {
        if (entity != null)
        {
            transform.position = entity.position;
            Debug.Log("up is"+KeyCode.UpArrow);
            if (Input.GetKeyDown("w"))
            {
                entity.position.y += 1 * Time.deltaTime;
            }
        }

    }
}
