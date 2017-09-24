using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controtionTable : MonoBehaviour {
    public List<RuntimeAnimatorController> animators = new List<RuntimeAnimatorController>();
    public List<ContortionData> datas = new List<ContortionData>();
	// Use this for initialization
	void Start () {
        datas.Add(new controtionSample());
        datas.Add(new mis_bx_bx());
        datas.Add(new lang_bx());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
