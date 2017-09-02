using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bx_bx : MonoBehaviour
{
    public List<RuntimeAnimatorController> animators = new List<RuntimeAnimatorController>();
    public List<ContortionData> datas = new List<ContortionData>();
    // Use this for initialization
    void Start()
    {
        datas.Add(new controtionSample());
        datas.Add(new mis_bx_bx());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        other.GetComponent<Controler>().distortionByNo(1);
    }

}