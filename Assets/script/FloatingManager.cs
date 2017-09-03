using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingManager : MonoBehaviour {
    public GameObject FloatingPrab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void createPrab(GameObject HPBar,int num)
    {
        GameObject newone = Instantiate(FloatingPrab, HPBar.transform.position, HPBar.transform.rotation);
        newone.transform.parent = HPBar.transform;
        newone.GetComponent<Text>().text = num + "";
    }
}
