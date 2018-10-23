using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class part_Table : MonoBehaviour {
    public static part_Table main;
    public GameObject roleBase;
    public List<GameObject> mouses;
    public List<GameObject> faces;
    public List<GameObject> eyes;
    public List<GameObject> leftHands;
    public List<GameObject> rightHands;
    public List<GameObject> weapons;

    void OnEnable()
    {
        if (main == null)
        {
            main = this;
        }
        else
        {
            Destroy(this);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
