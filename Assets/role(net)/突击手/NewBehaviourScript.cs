using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {

    public SpriteRenderer s;
	// Use this for initialization
	void Start () {
        s = gameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        s.color = new Color(255, 255, 255, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
