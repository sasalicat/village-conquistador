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
    _on_left_down on_left_down;
    _on_right_down on_right_down;
    _on_middle_down on_middle_down;
    _on_key1_down on_key1_down;
    _on_key2_down on_key2_down;
    _on_key3_down on_key3_down;
    _on_key4_down on_key4_down;
    _on_key5_down on_key5_down;
    _on_keyup_down on_keyup_down;
    _on_keyup_ing on_keyup_ing;
    _on_keyup_up on_keyup_up;
    _on_keyleft_down on_keyleft_down;
    _on_keyleft_ing on_keyleft_ing;
    _on_keyleft_up on_keyleft_up;
    _on_keydown_down on_keydown_down;
    _on_keydown_ing on_keydown_ing;
    _on_keydown_up on_keydown_up;
    _on_keyright_down on_keyright_down;
    _on_keyright_ing on_keyright_ing;
    _on_keyright_up on_keyright_up;

    public _on_key1_down get_on_key1_down()
    {
        return on_key1_down;
    }
    public _on_key2_down get_on_key2_down()
    {
        return on_key2_down;
    }
    public _on_key3_down get_on_key3_down()
    {
        return on_key3_down;
    }
    public _on_key4_down get_on_key4_down()
    {
        return on_key4_down;
    }
    public _on_key5_down get_on_key5_down()
    {
        return on_key5_down;
    }
    public _on_keydown_down get_on_keydown_down()
    {
        return on_keydown_down;
    }
    public _on_keydown_ing get_on_keydown_ing()
    {
        return on_keydown_ing;
    }
    public _on_keydown_up get_on_keydown_up()
    {
        return on_keydown_up;
    }
    public _on_keyleft_down get_on_keyleft_down()
    {
        return on_keyleft_down;
    }
    public _on_keyleft_ing get_on_keyleft_ing()
    {
        return on_keyleft_ing;
    }
    public _on_keyleft_up get_on_keyleft_up()
    {
        return on_keyleft_up;
    }
    public _on_keyright_down get_on_keyright_down()
    {
        return on_keyright_down;
    }
    public _on_keyright_ing get_on_keyright_ing()
    {
        return on_keyright_ing;
    }
    public _on_keyright_up get_on_keyright_up()
    {
        return on_keyright_up;
    }
    public _on_keyup_down get_on_keyup_down()
    {
        return on_keyup_down;
    }
    public _on_keyup_ing get_on_keyup_ing()
    {
        return on_keyup_ing;
    }
    public _on_keyup_up get_on_keyup_up()
    {
        return on_keyup_up;
    }
    public _on_left_down get_on_left_down()
    {
        return on_left_down;
    }
    public _on_middle_down get_on_middle_down()
    {
        return on_middle_down;
    }
    public _on_right_down get_on_right_down()
    {
        return on_right_down;
    }
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
