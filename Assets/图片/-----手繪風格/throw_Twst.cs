using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throw_Twst : MonoBehaviour {
    public Anim_Mobile anim;
    public GameObject sprite;
    private GameObject nowMissile;
    public int[][] list;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Anim_Mobile>();
	}
	void onfinish()
    {
        nowMissile.transform.parent = null;

    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            nowMissile= Instantiate(sprite, anim.righthand.transform.position, transform.rotation, anim.righthand.transform);
            anim.action(1,onfinish);
            anim.expression(1);
        }
	}
   
}
