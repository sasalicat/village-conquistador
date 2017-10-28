using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonChange : MonoBehaviour {
    public Sprite anotherIcon;
    private Sprite origenIcon;
    public Image selfImage;
	// Use this for initialization
	void Start () {
        selfImage = GetComponent<Image>();
        origenIcon = selfImage.sprite;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void changeStart()
    {
        selfImage.sprite = anotherIcon;
    }
    public void undoChange()
    {
        selfImage.sprite = origenIcon;
    }
}
