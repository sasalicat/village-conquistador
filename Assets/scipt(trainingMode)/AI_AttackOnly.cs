using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_AttackOnly : MonoBehaviour {
    Controler control;
    float nextAttack = 1;
	// Use this for initialization
	void Start () {
        control = GetComponent<Controler>();
	}
	
	// Update is called once per frame
	void Update () {
        nextAttack -= Time.deltaTime;
        if (nextAttack <= 0)
        {
            Vector3 pos = transform.position;
            pos.y += 10;
            (control.get_on_key1_down())(pos,EquipmentList.ATK);
            nextAttack = 1;
        }
	}
}
