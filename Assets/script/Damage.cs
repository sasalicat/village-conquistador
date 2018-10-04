using UnityEngine;
using System.Collections;

public class damage
{
    public const int PHY_DAMAGE = 1;
    public const int SPEC_DAMAGE = 2;
    public int kind;//1-戰鬥傷害 2-特殊傷害
    public int num;//傷害數值
    public float stiffTime;
    public bool hitConversely;//是否對倒地的單位有效
    public bool makeConversaly;//是否使單位倒地
    public sbyte equipIndex = -1;
    public sbyte arg = 0;//而外參數,用於指示暴擊
    public GameObject damager;

    public damage(int kind, int num, float stiffTime)
    {
        this.kind = kind;
        this.num = num;
        this.stiffTime = stiffTime;
        hitConversely = false;
        makeConversaly = false;

    }
    public damage(int kind, int num, float stiffTime, bool hitConversely, bool makeConversaly, GameObject damager)
    {
        this.kind = kind;
        this.num = num;
        this.stiffTime = stiffTime;
        this.hitConversely = hitConversely;
        this.makeConversaly = makeConversaly;
        this.damager = damager;
    }
    public damage(int kind, int num, float stiffTime, bool hitConversely, bool makeConversaly, GameObject damager,sbyte eIndex)
    {
        this.equipIndex = eIndex;
        this.kind = kind;
        this.num = num;
        this.stiffTime = stiffTime;
        this.hitConversely = hitConversely;
        this.makeConversaly = makeConversaly;
        this.damager = damager;
    }
}
