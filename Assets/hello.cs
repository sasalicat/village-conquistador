using KBEngine;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net.NetworkInformation;

public class hello : MonoBehaviour
{
    public Text show;
    // Use this for initialization
    void Start()
    {
        KBEngine.Event.registerOut("onHello",this,"onHello");
        KBEngine.Event.registerOut("onEnterSpace",this,"onEnterSpace");
        KBEngine.Event.registerOut("onEnterWorld", this, "onEnterWorld");
        System.Net.NetworkInformation.NetworkInterface[] nis = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
        string name= nis[0].GetPhysicalAddress().ToString();
        show.text = name;
    }

    // Update is called once per frame
    public void reqHello()
    {
        Account account = (Account)KBEngineApp.app.player();
        if (account != null)
            account.reqHello();
        else
        {
            Debug.Log("account is null");
        }
    }
    public void onHello(string data)
    {
        Debug.Log("onhello "+data);
        show.text = data;
    }
    public void onEnterSpace(Entity newone)
    {
        Debug.Log("onEnterSpace"+newone.className+"id "+newone.id);
    }
    public void onEnterWorld(Entity newone)
    {
        Debug.Log("onEnterWorld" + newone.className + "id " + newone.id);
    }
}