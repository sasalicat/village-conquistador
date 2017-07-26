using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_treatment_halo : Missile
{
    public List<RoleState> roles = new List<RoleState>();
    // Use this for initialization
    void Start()
    {
        roles.Add(this.transform.parent.GetComponent<RoleState>());
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        RoleState role = other.gameObject.GetComponent<RoleState>();

        if (role != null)
        {
            Debug.Log("mis_treatment_halo~~~~~~");
            roles.Add(role);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        roles.Remove(other.gameObject.GetComponent<RoleState>());
    }

    public void BeenTreat()
    {
        for(int i = 0;i < roles.Count; i++)
        {
            roles[i].BeenTreat(transform.parent.gameObject, (int)(roles[i].maxHp * 0.01));
        }
    }
}