using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyCenter : MonoBehaviour {
    public delegate void  moveEnd(Dictionary<string,object> endInf);//endInf包含是撞到什麼使物體停下來,和總共進行了多久
    public moveEnd onMoveEnd;
    protected class moveProcess
    {
        public Vector3 speed;
        public float timeLeft=0;
        public float timeTotal = 0;
        public moveProcess(Vector3 speed,float time)
        {
            this.speed = speed;
            timeLeft = time;
            timeTotal = time;
        }

    }
    protected moveProcess process;
    public void startprocess(Vector3 speed,float time, moveEnd callback)
    {
        if (process != null)//如果還有之前一個行程
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args["totalTime"] = process.timeTotal - process.timeLeft;
            args["hitter"] = null;
            processCancel(args);
        }
        process = new moveProcess(speed, time);
        onMoveEnd = callback;
    }
    public void startprocess(Vector3 speed, float time)
    {
        if (process != null)//如果還有之前一個行程
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args["totalTime"] = process.timeTotal - process.timeLeft;
            args["hitter"] = null;
            processCancel(args);
        }
        process = new moveProcess(speed, time);
        onMoveEnd = null;
    }
    public void processCancel(Dictionary<string,object> args)
    {

            if (onMoveEnd != null)
            {
                onMoveEnd(args);
            }
            process = null;
        
    }
	// Update is called once per frame
	protected void Update () {
        //Debug.Log("in phycenter update");
        if (process != null)
        {
            Debug.Log("process not null");
            process.timeLeft -= Time.deltaTime;
            transform.position += process.speed * Time.deltaTime;
             if (process.timeLeft <= 0)//時間結束
            {
                Dictionary<string, object> args = new Dictionary<string, object>();
                args["totalTime"] = process.timeTotal;
                args["hitter"] = null;
                processCancel(args);
            }
        }
       
	}
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (process != null)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args["totalTime"] = process.timeTotal - process.timeLeft;
            args["hitter"] = collision.gameObject;
            processCancel(args);
        }
    }
}
