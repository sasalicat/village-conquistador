using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyListener : MonoBehaviour {
    public static keyListener main;
    public NetRoleState state;
    public fsynControler controler;
    public EquipmentList eList;
    private System.Random random;
    // Use this for initialization
    public void befFrameUpdate()
    {
        Dictionary<string, object> order = new Dictionary<string, object>();
        order["code"] = CodeTable.SET_MOUSE_POS;
        order["mousePosition"] = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Sender.main.addOrder(order);
    }
    void OnEnable()
    {
        if (main == null)
        {
            main = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start () {
        Sender.main.beforeFrameEnd += befFrameUpdate;
        random = new System.Random();
	}
	private void onMoveButtom(sbyte code)
    {
        Dictionary<string, object> order = new Dictionary<string, object>();
        order["code"] = code;
        Sender.main.addOrder(order);
        
    }
	// Update is called once per frame
	void Update () {
        if (state != null)
        {
                if (Input.GetKeyDown(KeyRegister.main.keySetting["up"]))
                {
                    onMoveButtom(CodeTable.KEYUP_DOWN);
                }
                if (Input.GetKeyDown(KeyRegister.main.keySetting["left"]))
                {
                    onMoveButtom(CodeTable.KEYLEFT_DOWN);
                }
                if (Input.GetKeyDown(KeyRegister.main.keySetting["down"]))
                {
                    onMoveButtom(CodeTable.KEYDOWN_DOWN);
                }
                if (Input.GetKeyDown(KeyRegister.main.keySetting["right"]))
                {
                    onMoveButtom(CodeTable.KEYRIGHT_DOWN);
                }
                if (Input.GetKeyUp(KeyRegister.main.keySetting["up"]))
                {
                    onMoveButtom(CodeTable.KEYUP_UP);
                }
                if (Input.GetKeyUp(KeyRegister.main.keySetting["left"]))
                {

                    onMoveButtom(CodeTable.KEYLEFT_UP);
                }
                if (Input.GetKeyUp(KeyRegister.main.keySetting["down"]))
                {

                    onMoveButtom(CodeTable.KEYDOWN_UP);
                }
                if (Input.GetKeyUp(KeyRegister.main.keySetting["right"]))
                {
                    onMoveButtom(CodeTable.KEYRIGHT_UP);
                }
            
            if (state.canAction)
            {
                var limit = controler.limit;
                var mousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                int rpoint = random.Next(0, 99);
                if (controler.equipmentReady(EquipmentList.PASSIVE1))
                {
                    if (Input.GetKeyDown(KeyRegister.main.keySetting["key1"]))
                    {

                        if (eList.nowHarness.passiveEquipments[EquipmentList.PASSIVE1].Designated)
                        {
                            RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, 10);
                            if (hit && (hit.collider.tag == "Player"))
                            {
                                Dictionary<string, object> order = new Dictionary<string, object>();
                                order["MousePosition"] = mousePos;
                                order["PlayerPosition"] = controler.transform.position;
                                order["randomPoint"] = rpoint;
                                order["Traget"] = hit.transform.gameObject;
                                order["TragetPosition"] = hit.transform.position;
                                order["code"] = CodeTable.KEY1_DOWN;
                                Sender.main.addOrder(order);
                            }
                        }
                        else
                        {
                            Dictionary<string, object> order = new Dictionary<string, object>();
                            order["MousePosition"] = mousePos;
                            order["PlayerPosition"] = controler.transform.position;
                            order["randomPoint"] = rpoint;
                            order["code"] = CodeTable.KEY1_DOWN;
                            Sender.main.addOrder(order);
                        }

                    }
                }
                if (controler.equipmentReady(EquipmentList.PASSIVE2))
                {
                    if (Input.GetKeyDown(KeyRegister.main.keySetting["key2"]))
                    {

                        if (eList.nowHarness.passiveEquipments[EquipmentList.PASSIVE2].Designated)
                        {
                            RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, 10);
                            if (hit && (hit.collider.tag == "Player"))
                            {
                                Dictionary<string, object> order = new Dictionary<string, object>();
                                order["MousePosition"] = mousePos;
                                order["PlayerPosition"] = controler.transform.position;
                                order["randomPoint"] = rpoint;
                                order["Traget"] = hit.transform.gameObject;
                                order["TragetPosition"] = hit.transform.position;
                                order["code"] = CodeTable.KEY2_DOWN;
                                Sender.main.addOrder(order);
                            }
                        }
                        else
                        {
                            Dictionary<string, object> order = new Dictionary<string, object>();
                            order["MousePosition"] = mousePos;
                            order["PlayerPosition"] = controler.transform.position;
                            order["randomPoint"] = rpoint;
                            order["code"] = CodeTable.KEY2_DOWN;
                            Sender.main.addOrder(order);
                        }

                    }
                }
                if (controler.equipmentReady(EquipmentList.PASSIVE3))
                {
                    if (Input.GetKeyDown(KeyRegister.main.keySetting["key3"]))
                    {

                        if (eList.nowHarness.passiveEquipments[EquipmentList.PASSIVE3].Designated)
                        {
                            RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, 10);
                            if (hit && (hit.collider.tag == "Player"))
                            {
                                Dictionary<string, object> order = new Dictionary<string, object>();
                                order["MousePosition"] = mousePos;
                                order["PlayerPosition"] = controler.transform.position;
                                order["randomPoint"] = rpoint;
                                order["Traget"] = hit.transform.gameObject;
                                order["TragetPosition"] = hit.transform.position;
                                order["code"] = CodeTable.KEY3_DOWN;
                                Sender.main.addOrder(order);
                            }
                        }
                        else
                        {
                            Dictionary<string, object> order = new Dictionary<string, object>();
                            order["MousePosition"] = mousePos;
                            order["PlayerPosition"] = controler.transform.position;
                            order["randomPoint"] = rpoint;
                            order["code"] = CodeTable.KEY3_DOWN;
                            Sender.main.addOrder(order);
                        }
                    }
                }
                if (controler.equipmentReady(EquipmentList.PASSIVE4))
                {
                    if (Input.GetKeyDown(KeyRegister.main.keySetting["key4"]))
                    {

                        if (eList.nowHarness.passiveEquipments[EquipmentList.PASSIVE4].Designated)
                        {
                            RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, 10);
                            if (hit && (hit.collider.tag == "Player"))
                            {
                                Dictionary<string, object> order = new Dictionary<string, object>();
                                order["MousePosition"] = mousePos;
                                order["PlayerPosition"] = controler.transform.position;
                                order["randomPoint"] = rpoint;
                                order["Traget"] = hit.transform.gameObject;
                                order["TragetPosition"] = hit.transform.position;
                                order["code"] = CodeTable.KEY4_DOWN;
                                Sender.main.addOrder(order);
                            }
                        }
                        else
                        {
                            Dictionary<string, object> order = new Dictionary<string, object>();
                            order["MousePosition"] = mousePos;
                            order["PlayerPosition"] = controler.transform.position;
                            order["randomPoint"] = rpoint;
                            order["code"] = CodeTable.KEY4_DOWN;
                            Sender.main.addOrder(order);
                        }
                    }
                }
                if (controler.equipmentReady(EquipmentList.PASSIVE5))
                {
                    if (Input.GetKeyDown(KeyRegister.main.keySetting["key5"]))
                    {

                        if (eList.nowHarness.passiveEquipments[EquipmentList.PASSIVE5].Designated)
                        {
                            RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, 10);
                            if (hit && (hit.collider.tag == "Player"))
                            {
                                Dictionary<string, object> order = new Dictionary<string, object>();
                                order["MousePosition"] = mousePos;
                                order["PlayerPosition"] = controler.transform.position;
                                order["randomPoint"] = rpoint;
                                order["Traget"] = hit.transform.gameObject;
                                order["TragetPosition"] = hit.transform.position;
                                order["code"] = CodeTable.KEY5_DOWN;
                                Sender.main.addOrder(order);
                            }
                        }
                        else
                        {
                            Dictionary<string, object> order = new Dictionary<string, object>();
                            order["MousePosition"] = mousePos;
                            order["PlayerPosition"] = controler.transform.position;
                            order["randomPoint"] = rpoint;
                            order["code"] = CodeTable.KEY5_DOWN;
                            Sender.main.addOrder(order);
                        }
                    }
                }
                //鼠標
                if (controler.equipmentReady(EquipmentList.ATK))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("鼠標點擊生效!");
                        //Debug.Log("NetPlayerControler:position" + transform.position + "mouse Position" + mousePos);
                       
                        if (eList.nowHarness.passiveEquipments[EquipmentList.ATK].Designated)
                        {
                            RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, 10);
                            if (hit && (hit.collider.tag == "Player"))
                            {
                                Dictionary<string, object> order = new Dictionary<string, object>();
                                order["MousePosition"] = mousePos;
                                order["PlayerPosition"] = controler.transform.position;
                                order["randomPoint"] = rpoint;
                                order["Traget"] = hit.transform.gameObject;
                                order["TragetPosition"] = hit.transform.position;
                                order["code"] = CodeTable.MOUSE_LEFT_DOWN;
                                Sender.main.addOrder(order);
                            }
                        }
                        else
                        {
                            Dictionary<string, object> order = new Dictionary<string, object>();
                            order["MousePosition"] = mousePos;
                            order["PlayerPosition"] = controler.transform.position;
                            order["randomPoint"] = rpoint;
                            order["code"] = CodeTable.MOUSE_LEFT_DOWN;
                            Sender.main.addOrder(order);
                        }
                        //Debug.Log("num" + on_left_down.GetInvocationList().Length + "after mousePos is" + mousePos);
                    }
                }
                if (controler.equipmentReady(EquipmentList.SKILL))
                {
                    if (Input.GetMouseButtonDown(1))
                    {

                        //Debug.Log("NetPlayerControler:position" + transform.position + "mouse Position" + mousePos);

                        if (eList.nowHarness.passiveEquipments[EquipmentList.SKILL].Designated)
                        {
                            RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, 10);
                            if (hit && (hit.collider.tag == "Player"))
                            {
                                Dictionary<string, object> order = new Dictionary<string, object>();
                                order["MousePosition"] = mousePos;
                                order["PlayerPosition"] = controler.transform.position;
                                order["randomPoint"] = rpoint;
                                order["Traget"] = hit.transform.gameObject;
                                order["TragetPosition"] = hit.transform.position;
                                order["code"] = CodeTable.MOUSE_RIGHT_DOWN;
                                Sender.main.addOrder(order);
                            }

                        }
                        else
                        {
                            Dictionary<string, object> order = new Dictionary<string, object>();
                            order["MousePosition"] = mousePos;
                            order["PlayerPosition"] = controler.transform.position;
                            order["randomPoint"] = rpoint;
                            order["code"] = CodeTable.MOUSE_RIGHT_DOWN;
                            Sender.main.addOrder(order);
                        }
                        //Debug.Log("num" + on_left_down.GetInvocationList().Length + "after mousePos is" + mousePos);
                    }
                }
            }
        }
    }
}
