using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineCreater : MonoBehaviour {
    public Texture2D simple;
    public Material temp;
    private LineRenderer line;
	// Use this for initialization
	void Start () {
        line = GetComponent<LineRenderer>();
        createLine(new Vector3(10, 10, 0));

    }
	public void createLine(Vector3 endPoint)
    {
        Vector3[] points = new Vector3[2];
        points[0] = transform.position;
        points[0].z = 1;
        points[1] = endPoint;
        points[1].z = 1;
        line.SetPositions(points);
        Vector3 ori = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 end = Camera.main.WorldToScreenPoint(endPoint);
        int length =(int) (ori - end).magnitude;
        Texture2D tempT = new Texture2D(simple.width,length);
        for(int y = 0; y < length; y++)
        {
            for(int x = 0; x < simple.width; x++)
            {
                tempT.SetPixel(x,y,simple.GetPixel(x, y % simple.height));
            }
        }
  
        temp.SetTexture("_MainTex", tempT);

    }
	// Update is called once per frame
	void Update () {
		
	}
}
