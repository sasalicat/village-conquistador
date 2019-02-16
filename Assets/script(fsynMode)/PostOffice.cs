using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PostOffice:MonoBehaviour  {
    public delegate void Empty();
    public static PostOffice main;
    public float cycleTime=0.03f;
    private float frameTimeLeft=0;
    public abstract void addOrder(Dictionary<string, object> order);
    public Empty beforeFrameEnd;
    private int counter = 0;
    private System.Diagnostics.Stopwatch now_watch = null;
    protected void OnEnable()
    {
        frameTimeLeft = cycleTime;
        if (main == null)
        {
            main = this;
        }
        else
        {
            Destroy(this);
        }
    }

    protected void Update()
    {
        frameTimeLeft -= Time.deltaTime;
        //Debug.Log("deltaTime:" + Time.deltaTime);
        //Debug.Log("in post office update:"+frameTimeLeft);
        if (frameTimeLeft <= 0)
        {
            if (now_watch != null)
            {
                now_watch.Stop();
                //Debug.Log("第"+counter+"幀 實際耗時:"+now_watch.Elapsed.TotalMilliseconds+"毫秒");
            }
            //Debug.Log("send");
            if(beforeFrameEnd!=null)
                beforeFrameEnd();
            updateFrame();
            frameTimeLeft = cycleTime;
            now_watch = new System.Diagnostics.Stopwatch();
            now_watch.Start();
            counter++;
        }
       
    }
     public abstract void updateFrame();
}
