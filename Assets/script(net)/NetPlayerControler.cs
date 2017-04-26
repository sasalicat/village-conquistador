using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;

public class NetPlayerControler : MonoBehaviour,Controler {
    public const float UPDATE_Z_INTERVAL = 0.1f;
    public Entity entity;
    private int lastZ=0;
    private float timeInterval = 0;
    private Dictionary<string, KeyCode> keySetting;
    void Start()
    {
        GameObject temp = GameObject.Find("keyTabel");
        keySetting =temp.GetComponent<KeyRegister>().keySetting;
    }
    void OnEnable()
    {
        transform.position = entity.position;
    }
    void Update()
    {
        timeInterval += Time.deltaTime;
        int nowz = (int)transform.eulerAngles.z;
        Vector3 mousePos = Input.mousePosition;
        Debug.Log(mousePos);
        mousePos.z = 19;
        mousePos= Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;
        transform.up = mousePos - transform.position;
        if (nowz != lastZ && timeInterval >= UPDATE_Z_INTERVAL)
        {
           
            short ans = (short)(nowz);
            Debug.Log("Z change nowz is"+nowz+" ans is" + ans);
            ((Player)entity).baseCall("updateZ", new object[] {ans});
            timeInterval = 0;
            lastZ = nowz;
        }
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
