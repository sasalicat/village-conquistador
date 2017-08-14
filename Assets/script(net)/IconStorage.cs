using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconStorage : MonoBehaviour {
    
    public  Sprite[] headIcon=new Sprite[22];
    public Sprite[] skillIcon = new Sprite[100];
    public Sprite[] readyIcon = new Sprite[2];
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
