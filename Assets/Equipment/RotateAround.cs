using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Transform aroundPoint;//围绕的物体
    public float angularSpeed;//角速度
    public float aroundRadius;//半径

    public float angled;//可以算是初始角度之后会成为当前角度的记录变数
    void Start()
    {
        //设置物体初始位置为围绕物体的正前方距离为半径的点
        Vector3 p = aroundPoint.rotation * Vector3.forward * aroundRadius;
        transform.position = new Vector3(p.x, p.y, aroundPoint.position.z);
    }

    void Update()
    {
        angled += (angularSpeed * Time.deltaTime) % 360;//累加已经转过的角度
        float posX = aroundRadius * Mathf.Sin(angled * Mathf.Deg2Rad);//计算x位置
        float posY = aroundRadius * Mathf.Cos(angled * Mathf.Deg2Rad);//计算y位置

        transform.position = new Vector3(posX, posY,0 ) + aroundPoint.position;//更新位置
    }
}