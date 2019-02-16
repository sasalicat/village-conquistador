using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill_flashBomb : add_skill_accum {
    public const int BaseDamage = 50;
    public const float BaseStiff = 0.3f;
    protected int selfMissileNo = 2;
    private GameObject missilePraf;//暫存總missileTable內得到的預設體
    private RoleState selfState;
    getVector getVector;
    float time = 0;
    protected Vector3 misLocalPos = new Vector3(0, -1, 0);
    private GameObject phantom = null;
    private
    public override float AccumTime
    {
        get
        {
            return 1f;
        }
    }

    public override float CD
    {
        get
        {
            return 6f;
        }
    }

    public override uint Consumption
    {
        get
        {
            return 0;
        }
    }

    public override bool Designated
    {
        get
        {
            return false;
        }
    }

    public override sbyte Kind
    {
        get
        {
            return EquipmentTable.PASSIVE_SKILL;
        }
    }

    public override sbyte No
    {
        get
        {
            return 3;
        }
    }

    public override void trigger(Dictionary<string, object> args)
    {

        base.trigger(args);
        Debug.Log("閃光彈被觸發");
        //((Anim_Mobile)anim).switchWeaponFalse();

        ((Anim_Mobile)anim).action(3);
        getVector = GameObject.Find("keyTabel").GetComponent<getVector>();
        Vector3 origenPlayerPosition = (Vector3)args["PlayerPosition"];//施放技能時玩家位置
        Vector3 mousePosition = (Vector3)args["MousePosition"];//施放技能時鼠標點擊位置
        //Instantiate(testObj, mousePosition,transform.rotation);
        Vector3 relative = mousePosition - origenPlayerPosition;
        Transform rightHand = ((Anim_Mobile)anim).righthand.transform;
        GameObject newone = Instantiate(missilePraf, Vector2.zero, Quaternion.Euler(relative), rightHand);
        //newone.GetComponent<Missile>().enabled = false;
        //newone.GetComponent<Collider2D>().enabled = false;
        newone.transform.rotation = rightHand.rotation;
        newone.transform.localPosition = Vector2.zero;
        newone.transform.Rotate(new Vector3(0, 0, 90));
        newone.transform.localScale = new Vector2(1.2f, 1.2f);
        phantom = newone;
        GameObject 
    }

}
