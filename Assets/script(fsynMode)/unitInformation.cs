using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;


public class unitInformation : MonoBehaviour {
    public GameObject[] unit;
    void OnEnable()
    {
        XmlDocument XmlDoc = new XmlDocument();
        //XmlDoc.Load(url);
        XmlNodeList XMllist = XmlDoc.GetElementsByTagName("skill");
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
