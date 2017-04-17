using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;

public class NetPlayerControler : MonoBehaviour,Controler {

    public Entity entity;
    private Dictionary<string, KeyCode> keySetting;
    void Start()
    {
        GameObject temp = GameObject.Find("keyTabel");
        keySetting =temp.GetComponent<KeyRegister>().keySetting;
    }
    void Update()
    {
        if (entity != null)
        {
             entity.position=transform.position;
            entity.direction = transform.eulerAngles;
            //Debug.Log("up is"+KeyCode.UpArrow);
            Vector3 changeV3 = new Vector3(0, 0, 0);
            if (Input.GetKey(keySetting["up"]))
            {
                changeV3.y += 5 * Time.deltaTime;
            }
            if (Input.GetKey(keySetting["left"]))
            {
                changeV3.x += 5 * Time.deltaTime;
            }
            if (Input.GetKey(keySetting["down"]))
            {
                changeV3.y -= 5 * Time.deltaTime;
            }
            if (Input.GetKey(keySetting["right"]))
            {
                changeV3.x -= 5 * Time.deltaTime;
            }
            transform.position += changeV3;
        }

    }
}
