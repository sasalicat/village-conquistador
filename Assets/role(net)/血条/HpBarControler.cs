using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarControler : MonoBehaviour {
    public Transform role;
    public const float maxX=3.55f;
    RectTransform rect;
    public GameObject hpline;
    public GameObject hpBar;
    public FloatingManager floating;
    // Use this for initialization
    void Start()
    {
        floating = GameObject.Find("HPCanvas").GetComponent<FloatingManager>();
    }
    // Update is called once per frame
    void Update () {
        if (role != null)
        {
            transform.position = new Vector3(role.position.x, role.position.y+3, 0);
        }
	}
    public void onGetRole()
    {
        rect = hpline.GetComponent<RectTransform>();
        role.GetComponent<Controler>().On_Hp_Change += HpChange;
        role.GetComponent<Controler>().On_Take_Damage += TakeDamage;
    }
    public void HpChange(Dictionary<string,object> arg)
    {
        
        float percent = (float)arg["Percent"];
        //Debug.Log("hpchange"+percent);
        //transform.localPosition = new Vector3(maxX * percent, -0.04f, 0);
        rect.anchoredPosition = new Vector2(maxX-maxX * percent, -0.04f);
    }
    public void TakeDamage(Dictionary<string, object> arg)//这主要是伤害的浮动数字显示
    {
        floating.createPrab(hpBar,((damage)arg["Damage"]).num);
    }
}
