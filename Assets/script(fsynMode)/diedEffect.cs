using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diedEffect : MonoBehaviour {
    public float totalTime;
    public float timeleft;
    private SpriteRenderer render;
	// Use this for initialization
	void Start () {
        render = GetComponent<SpriteRenderer>();
	}
	public void onInit(float time)
    {
        totalTime = time;
        timeleft = time;
    }
	// Update is called once per frame
	void Update () {
        timeleft -= Time.deltaTime;
        render.color =new Color(1,1,1,timeleft / totalTime);
        if (timeleft <= 0)
        {
            Destroy(gameObject);
        }
    }
}
