using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookFlicker : MonoBehaviour {
    public float circle_time=1f;
    private SpriteRenderer render;
    private Color nowColor = new Color(255, 255, 255, 255);
    private float totalTime = 0;
    // Use this for initialization
    void Start () {
        render = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        totalTime += Time.deltaTime;
        nowColor.a = 1-totalTime % circle_time;
        render.color = nowColor;
	}
}
