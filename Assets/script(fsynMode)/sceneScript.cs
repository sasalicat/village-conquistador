using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneScript : MonoBehaviour {
    public int[] enemyIndexs;
	// Use this for initialization
    public void onStart(fsynManager_local manager)
    {
        manager.createMainRole(0,11,new List<int>{ 65,50,64,66,67,70},Vector2.zero);
        /*manager.createEnemy(enemyInfoList.main.enemyInfos[0], new Vector2(15,15));
        manager.enemyList[0].GetComponent<AI_fsyn>().traget = manager.objList[0];
        manager.createEnemy(enemyInfoList.main.enemyInfos[0], new Vector2(-10, -10));
        manager.enemyList[1].GetComponent<AI_fsyn>().traget = manager.objList[0];
        manager.createEnemy(enemyInfoList.main.enemyInfos[0], new Vector2(0, -20));
        manager.enemyList[2].GetComponent<AI_fsyn>().traget = manager.objList[0];*/
        /*var enemy= manager.createEnemy(enemyInfoList.main.enemyInfos[1], new Vector2(10, 10));
        enemy.GetComponent<AI_fsyn>().traget = manager.objList[0];

        manager.enemyList[3].GetComponent<AI_fsyn>().traget = manager.objList[0];*/

        // var enemy = manager.createEnemy(enemyInfoList.main.enemyInfos[2], new Vector2(10, -10));
        //enemy.GetComponent<AI_fsyn>().traget = manager.objList[0];
        //((normal_warrior)enemy.GetComponent<AI_fsyn>()).range = 50;
    }
}
