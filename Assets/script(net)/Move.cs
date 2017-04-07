using UnityEngine;
using System.Collections;
using KBEngine;

public class Move : MonoBehaviour {
   
	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
        Account player = (Account)KBEngineApp.app.player();
       
        if (player != null) {
            player.direction = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Debug.Log("up down");
                KBEngineApp.app.player().position.y += 5 * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                KBEngineApp.app.player().position.y -= 5 * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                KBEngineApp.app.player().position.x -= 5 * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                KBEngineApp.app.player().position.x += 5 * Time.deltaTime;
            }
        }
        else
        {
            //Debug.Log("NULL");
        }
	}
}
