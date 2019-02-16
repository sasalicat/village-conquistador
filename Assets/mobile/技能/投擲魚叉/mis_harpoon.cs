using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_harpoon : baseAddMissile {
    public override void  Start()
    {
        Speed = STAND_FLY_SPEED * 2;
        base.Start();

    }
    public void onshiftStop(Dictionary<string, object> inf)
    {
        if (inf["hitter"] != null)//如果是撞到東西停止的
        {
            ((GameObject)inf["hitter"]).GetComponent<Controler>().addBuffByNo(0);//被撞到的物體加moveless buff
        }
        Destroy(this);
    }

    protected override void AftHit(Controler hitter)//帶有callback的shift 只有fysn 以上的controler才有,這造成了mis_harpoon這類missile只能使用於fsyn以上的版本
    {
        this.GetComponent<Collider2D>().enabled = false;//關掉碰撞器
        transform.parent = ((fsynControler)hitter).transform;//使自身跟隨被攻擊的物件
        ((fsynControler)hitter).beShift(vspeed.normalized * 10, 0.3f,onshiftStop); 
    }
}
