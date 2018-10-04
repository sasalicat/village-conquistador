using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rocker : MonoBehaviour {
    public delegate void withVector2(Vector2 V2);
    RectTransform rect;
    public withVector2 onRockerDrag;
    public withVector2 onRockerDragBegin;
    public withVector2 onRockerDragEnd;
    private Vector2 lastRelaPos;
    public Vector2 LastRelativePos
    {
        get
        {
            return lastRelaPos;
        }
    }
	// Use this for initialization
	void Start () {
        rect = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private Vector2 getRelativeVector(Vector2 ori)
    {
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect,ori, null, out localPos);

        int radiu = (int)rect.rect.width / 2;
        Vector2 relativeVector = localPos.normalized;
        float relativeLength = localPos.magnitude / (float)radiu;
        if (relativeLength > 1)
        {
            relativeLength = 1;
        }
        relativeVector *= relativeLength;
        return relativeVector;
    }
    public void onClicking(BaseEventData data)
    {
        /*Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, ((PointerEventData)data).position,null,out localPos);
        Debug.Log("pos:"+localPos);
        Debug.Log("width=" + rect.rect.width);
        int radiu = (int)rect.rect.width / 2;
        Vector2 relativeVector = localPos.normalized;
        float relativeLength = localPos.magnitude /(float)radiu;
        if (relativeLength > 1)
        {
            relativeLength = 1;
        }
        relativeVector *= relativeLength;*/
        Vector2 relativeVector = getRelativeVector(((PointerEventData)data).position);
        //Debug.Log("ans:(" + relativeVector.x+","+relativeVector.y+")");
        if (onRockerDrag != null)
        {
            onRockerDrag(relativeVector);
        }
        lastRelaPos = relativeVector;
    }
    public void onDragBegin(BaseEventData data)
    {
        Vector2 relativeVector = getRelativeVector(((PointerEventData)data).position);
        //Debug.Log("拖动开始");
        if (onRockerDragBegin != null)
        {
            onRockerDragBegin(relativeVector);
        }
        lastRelaPos = relativeVector;
    }
    public void onDragEnd(BaseEventData data)
    {
        //Debug.Log("拖動結束");
        if (onRockerDragEnd != null)
        {
            onRockerDragEnd(Vector2.zero);
        }
    }
}
