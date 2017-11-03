using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_forTask : MonoBehaviour {
    public Manager manager;
    Controler control;
    public const float ATTACK_CYCLE=0.5f;
    private float nextAttack=ATTACK_CYCLE;
    public EquipmentList elist;
    public dataRegister register;
    private int playerNum = 0;
    private GameObject[] objList;
	// Use this for initialization
	void Start () {
        control = GetComponent<Controler>();
        elist = GetComponent<EquipmentList>();
        register = GameObject.Find("client").GetComponent<dataRegister>();
        objList=manager.getGameObjectList();
    }
	
	// Update is called once per frame
	void Update () {
        if (playerNum <= 0)//第一次update
        {
            for(int i = 0; i < objList.Length; i++)
            {
                if (objList[i] != null)
                {
                    playerNum++;
                }
                else
                {
                    break;
                }
            }
            Debug.Log("初始化:playerNum=" + playerNum);
        }

        nextAttack -= Time.deltaTime;
        
        if (nextAttack <= 0)
        {

            sbyte ebuttom = 0;//記錄選擇的按鈕編號
            List<CDEquipment> e = elist.nowHarness.passiveEquipments;
            for (sbyte i = 0; i < e.Count; i++)//選擇技能
            {
                if (e[i].CanUse)//選擇第一個可以用的裝備
                {
                    ebuttom = i;
                    break;
                }
            }
            Debug.Log("在AI中 playerNum=" + playerNum);
            if (playerNum <= 1)//就自己一個人了還打個鬼
            {
                return;
            }
            int tragetno = Random.Range(0, playerNum - 1);//選擇攻擊目標
            
            if (tragetno!= control.Index)
            {
                if (objList[tragetno] == null)
                {
                    return;
                }
                Vector3 mosPos = objList[tragetno].transform.position;
                control.get_on_key1_down()(mosPos, ebuttom);
                        
                
            }
            Debug.Log("AI触发结束");
            nextAttack = ATTACK_CYCLE;
        }
	}
}
