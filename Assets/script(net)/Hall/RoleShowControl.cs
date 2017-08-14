﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleShowControl : MonoBehaviour {
    public HallManager hall;
    public GameObject roleLabelItem;
    public GameObject skillIcon;
    public GameObject MainLabel;
    public IconStorage storage;
    public dataRegister register;
    private List<GameObject> nowItems=new List<GameObject>();
    private List<RoleData> lastDatas=new List<RoleData>();

    private List<RoleData> nextDatas = null;//用于update更新UI的最新一笔从服务器传过来的资料
	// Use this for initialization
    private void updateRole(List<RoleData> datas)
    {
        foreach(GameObject item in nowItems)
        {
            nowItems.Remove(item);
            Destroy(item);
        }
        foreach(RoleData data in datas)
        {
            GameObject newItem = Instantiate(roleLabelItem);
            newItem.transform.parent = MainLabel.transform;
            newItem.GetComponent<Image>().sprite = storage.headIcon[data.roleKind];
            //Debug用Text 因为没图
            newItem.transform.Find("Text").GetComponent<Text>().text=" "+data.roleKind;
            Transform skillLable = newItem.transform.Find("skillContain");
            foreach (sbyte eno in data.equipmentIdList)
            {
                GameObject newSkill = Instantiate(skillIcon);
                newSkill.transform.parent = skillLable;
                newSkill.transform.Find("Text").GetComponent<Text>().text = " "+eno;
            }
        }
    }
    public bool checkDataEqual(List<RoleData> other)
    {
        if(other== lastDatas)
        {
            return true;
        }
        if (other.Count != lastDatas.Count)
        {
            return false;
        }
        else
        {
            for(int i = 0; i < other.Count; i++)
            {
                if (!lastDatas[i].equal(other[i]))
                {
                    return false; 
                }
            }
        }
        return true;
    }
    public void OnChange(List<RoleData> newDatas)
    {//如果在label激活的时候的改变只可能是1.重置随机角色2.随机角色认证成永久角色.无论那种新的角色列表和旧的一定是不同的
        updateRole(newDatas);
    }
	void Start () {
        storage = GameObject.Find("Icons").GetComponent<IconStorage>();
        register = GameObject.Find("client").GetComponent<dataRegister>();
        register.onRoleListChange += OnChange;
        if (!checkDataEqual(register.roleList))//有两种情况1.角色列表没有被打开过2.角色列表被打开过所以有item
        {
            updateRole(register.roleList);
            lastDatas = register.roleList;
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (nextDatas != null)
        {
            updateRole(nextDatas);
            lastDatas = nextDatas;
            nextDatas = null;
        }
	}
    public void onCloseClick()
    {
        gameObject.SetActive(false);
    }
    public void onChangeClick()
    {
        hall.askRandomRole();
    }
}