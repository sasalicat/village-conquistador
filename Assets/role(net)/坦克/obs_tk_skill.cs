using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obs_tk_skill : ObstacleState
{
    public SpriteRenderer s;
    public bool local;
    public damage damage;
    override protected void Start()
    {
        base.Start();
        maxHp = 500;
        nowHp = 500;

    }

    void Update()
    {
        base.Update();
    }
    void OnTriggerEnter2D(Collider2D other)
    {

    }
    public override void methodNull()
    {
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        AnimatorTable animatortable = this.transform.parent.gameObject.GetComponent<AnimatorTable>();
        animatortable.SkillEnd();
        Controler controler = this.transform.parent.gameObject.GetComponent<Controler>();
        controler.skillLimit = null;
    }

}