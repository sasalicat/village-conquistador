using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneScript : MonoBehaviour {
    public int[] enemyIndexs;
	// Use this for initialization
    public void onStart(fsynManager_local manager)
    {
        manager.createMainRole(0,0,new List<int>{  2 },Vector2.zero);
        manager.createEnemy(5, new List<int> { 61 },new Vector2(5,5), "normal_warrior");
        manager.enemyList[0].GetComponent<AI_fsyn>().traget = manager.objList[0];
    }
}
