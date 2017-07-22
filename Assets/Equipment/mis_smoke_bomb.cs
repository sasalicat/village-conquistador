using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_smoke_bomb : Missile
{
    private Vector3 vspeed;
    private Animator anim;
    public const int limitDistance = 15;

    public Vector3 origenPlayerPosition;
    public Vector3 mousePosition;
    public bool boom = false;
    public GameObject missilePraf2;
    GameObject newone;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

        Vector3 position = (mousePosition - origenPlayerPosition) + origenPlayerPosition;

        if ((mousePosition - origenPlayerPosition).magnitude < limitDistance)
        {
            transform.DOMove(mousePosition, 1f, false).OnComplete(() => getSkill2(mousePosition)).SetEase(Ease.OutQuart);
        }
        else
        {
            //向量 = 終點 - 起始點
            //起始點 = 向量 - 終點
            //終點 = 向量 + 起始點
            //((終點 - 起始點)) 變單位向量
            //終點 = 長度 * (終點 - 起始點).normalized + 起始點
            Vector3 destination = (limitDistance * (mousePosition - origenPlayerPosition).normalized) + origenPlayerPosition;
            transform.DOMove(destination, 1f, false).OnComplete(() => getSkill2(destination)).SetEase(Ease.OutQuart);
        }
    }

    void getSkill2(Vector3 destination)
    {
        newone = Instantiate(missilePraf2, destination, transform.rotation);
        Missile missile = newone.GetComponent<Missile>();
        missile.Creater = this.Creater;
        newone.transform.localScale = new Vector3(2, 2, 1);
        Destroy(gameObject);
    }

    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        boom = anim.GetBool("boom");
        if (boom == true)
        {
            RoleState role = other.gameObject.GetComponent<RoleState>();
            if (role != null)
                role.TakeDamage(Damage);
            Debug.Log("Boom actity");
        }
    }

    public void getAnimation()
    {

        Destroy(newone);
    }
}
