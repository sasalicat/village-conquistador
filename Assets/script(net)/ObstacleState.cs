using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;

public class ObstacleState : RoleState {//相当于控制器和State结合在同一个脚本
    public Entity entity;
    class obs_normal : state
    {
        private ObstacleState obs;
        public bool canAction
        {
            get
            {
                return false;
            }
        }

        public bool canMove
        {
            get
            {
                return false;
            }
        }

        public bool canRota
        {
            get
            {
                return false;
            }
        }

        public void beenTreat(int num, GameObject from)
        {
            return;
        }

        public void onUpdate()
        {
            
        }

        public void takedamage(damage damage)
        {
            if (damage.damager.GetComponent<NetPlayerControler>() != null)
            {
                Debug.Log("in take damage on Obstacle");
               
                
            }
        }
        public obs_normal(ObstacleState obs)
        {
            this.obs = obs;
        }
    }
    // Use this for initialization
    void Start () {
        base.Start();
        StateTable = new List<state>();
        nowState = new obs_normal(this);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
