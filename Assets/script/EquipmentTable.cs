using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentTable : MonoBehaviour {
    public string[] equipmentNameList;
    public bool[] passiveList;
    public string[] buffNameList;
    public const sbyte PASSIVE_SKILL = 1;
    public const sbyte ON_TAKE_DAMAGE = 2;
    public const sbyte ON_INTERVAL = 3;
    public const sbyte ON_BEEN_TREAT = 4;
    public const sbyte ON_HP_CHANGE = 5;
    public const sbyte ON_CAUSE_DAMAGE = 6;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
