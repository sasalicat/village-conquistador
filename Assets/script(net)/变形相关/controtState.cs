using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controtState : MonoBehaviour {
    public sbyte nowNo;
    public float TimeLeft;
    public bool needRecord = false;
    public AnimatorTable anim;
    public EquipmentList eList;
	// Use this for initialization
	void Start () {
        anim = GetComponent<AnimatorTable>();
        eList = GetComponent<EquipmentList>();
	}
	
	// Update is called once per frame
	void Update () {
        if (needRecord)
        {
            TimeLeft -= Time.deltaTime;
            if (TimeLeft <= 0)
            {
                eList.restoreArmedHarness();
                anim.restoreAnimator();
                Destroy(this);
            }
        }
	}
}
