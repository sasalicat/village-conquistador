using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillShow : MonoBehaviour {
    public GameObject detailSkill;//用来重复利用显示技能详细说明的物件 
    public Image detailIcon;
    public Text detailStatement;
    public Text detailName;
    public Text detailconsume;
    public IconStorage storage;
    public SkillStatements statement;

    public sbyte no;
    public void deleteself()
    {
        Destroy(gameObject);
    }
    public void onMouseEnter()
    {
       
        if (statement.skillTexts[no] != null)
        {
            
            Debug.Log("no is:" + no);
            
            detailIcon.sprite = storage.skillIcon[no];
            Debug.Log("statement Length is:" + statement.skillTexts.Length);
            detailStatement.text = statement.skillTexts[no].statement;
            detailName.text = statement.skillTexts[no].name;
            detailconsume.text = statement.skillTexts[no].consume;
            //detailSkill.transform.position=
        }
        else
        {
            detailIcon.sprite = null;
            detailName.text = "没有角色信息";
            detailStatement.text = "应该是还没有写,你咬我啊";
        }
        Vector3 mouse = Input.mousePosition;
        detailSkill.transform.position = new Vector3(mouse.x + 150, mouse.y - 158, mouse.z);
        detailSkill.SetActive(true);
    }
    public void onMouseExit()
    {
        detailSkill.SetActive(false);
    }
}
