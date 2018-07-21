using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class add_skill_accum : skill_accum,canBeAddition {
    private additiondele.withPos be_used;
    private additiondele.withaddMis on_create_missile;
    private additiondele.withDictionary on_cause_damage;
    public additiondele.withPos beUsed
    {
        set
        {
            be_used = value;
        }
        get
        {
            return be_used;
        }
    }
    public additiondele.withaddMis onCreateMissile
    {
        set
        {
            on_create_missile = value;
        }
        get
        {
            return on_create_missile;
        }
    }
    public additiondele.withDictionary onCauseDamage
    {
        set
        {
            on_cause_damage = value;
        }
        get
        {
            return on_cause_damage;
        }
    }
}
