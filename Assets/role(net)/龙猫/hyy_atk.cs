using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hyy_atk : Missile
{
    public MissileTable missletable;
    // Use this for initialization
    void Start()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 30);
        GameObject keytable = GameObject.Find("keyTabel");
        missletable = keytable.GetComponent(typeof(MissileTable)) as MissileTable;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z-60*Time.deltaTime);
        if(this.transform.position.z <= -1)
        {
            Destroy(this.gameObject);
            GameObject newone = Instantiate(missletable.MissileList[20], this.transform.position, this.transform.rotation);
            Missile missile = newone.GetComponent<Missile>();
            missile.Creater = gameObject;
            missile.Damage = this.Damage;
        }

    }
}
