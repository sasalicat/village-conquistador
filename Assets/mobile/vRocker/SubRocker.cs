using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubRocker : MonoBehaviour {
    RectTransform rect;
    RectTransform parentRect;
    public void onBaseDrag(Vector2 v)
    {
        rect.localPosition = v * (((float)parentRect.rect.width)/ 2);
    }
    public void onBaseDragBegin(Vector2 v)
    {
        rect.localPosition = v * (((float)parentRect.rect.width) / 2);
    }
    private void onBaseDragEnd(Vector2 v)
    {
        rect.localPosition = Vector2.zero;
    }
	// Use this for initialization
	void Start () {
        rect = GetComponent<RectTransform>();
        parentRect = transform.parent.GetComponent<RectTransform>();
        transform.parent.GetComponent<Rocker>().onRockerDrag += onBaseDrag;
        transform.parent.GetComponent<Rocker>().onRockerDragEnd += onBaseDragEnd;
        transform.parent.GetComponent<Rocker>().onRockerDragBegin += onBaseDragBegin;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
