using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobile_PhyCenter : PhyCenter
{
    public virtual void frameUpdate(float time)//幀同步用.代替Update來達成所有客戶端統一計時
    {
        if (process != null)
        {
            Debug.Log("process not null");
            process.timeLeft -= Time.deltaTime;
            transform.position += process.speed * Time.deltaTime;
            Dictionary<string, object> args = new Dictionary<string, object>();
            args["totalTime"] = process.timeTotal;
            args["hitter"] = null;
            if (process.timeLeft <= 0)//時間結束
            {
                processCancel(args);
            }
        }
    }
	// Use this for initialization
	void Start () {
        Timer.main.logInTimer(frameUpdate);
	}
	
	// Update is called once per frame
	new void  Update () {
		
	}
}
