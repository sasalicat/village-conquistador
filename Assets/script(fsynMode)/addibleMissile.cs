using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class addibleMissile : Missile {
    protected bool canBeRefected = true;

    public additiondele.withDamage onCauseDamage;
    public additiondele.withTraget onHit; 
    public virtual void BeReflected(Vector2 direct,GameObject actor)
    {
        Creater = actor;
        transform.up = direct;
    }
    
}
