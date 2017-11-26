using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyCenter : MonoBehaviour {
    public delegate void  moveEnd();
    public moveEnd onMoveEnd;
    class moveProcess
    {
        public Vector3 speed;
        public float timeLeft=0;
        public moveProcess(Vector3 speed,float time)
        {
            this.speed = speed;
            timeLeft = time;
        }

    }
    private moveProcess process;
    public void startprocess(Vector3 speed,float time)
    {
        if (process != null)//如果還有之前一個行程
        {
            if (onMoveEnd != null)
            {
                onMoveEnd();
            }
            process = new moveProcess(speed, time);
        }
    }
    public void processCancel()
    {
        if (process.timeLeft <= 0)//時間結束
        {
            if (onMoveEnd != null)
            {
                onMoveEnd();
            }
            process = null;
        }
    }
	// Update is called once per frame
	protected void Update () {
        process.timeLeft -= Time.deltaTime;
        transform.position += process.speed * Time.deltaTime;
        processCancel();
	}
    void OnCollisionEnter(Collision collision)
    {
        processCancel();
    }
}
