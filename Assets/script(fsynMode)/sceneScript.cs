using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneScript : MonoBehaviour {
    public int[] enemyIndexs;
	// Use this for initialization
    public void onStart(fsynManager_local manager)
    {
        manager.createMainRole(0,0,new List<int>{ 0,50 },Vector2.zero);
        manager.createEnemy(5, new List<int> { 61 },new Vector2(15,15), "normal_warrior");
        manager.enemyList[0].GetComponent<AI_fsyn>().traget = manager.objList[0];
        manager.createEnemy(5, new List<int> { 61 }, new Vector2(-10, -10), "normal_warrior");
        manager.enemyList[1].GetComponent<AI_fsyn>().traget = manager.objList[0];
        manager.createEnemy(5, new List<int> { 61 }, new Vector2(0, -20), "normal_warrior");
        manager.enemyList[2].GetComponent<AI_fsyn>().traget = manager.objList[0];
    }
}
