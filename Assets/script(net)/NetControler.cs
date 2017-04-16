using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;

public class NetControler : MonoBehaviour,Controler {
    Entity entity;
    void Update()
    {
        if (entity != null)
        {
            transform.position = entity.position;
           
        }
        
    }

}
