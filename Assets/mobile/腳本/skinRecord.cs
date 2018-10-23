using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skinRecord  {
    public int leftHandNo;
    public int rightHandNo;
    public int faceNo;
    public int eyesNo;
    public int mouseNo;
    public int weaponNo;
    public skinRecord(int lefthand,int righthand,int face,int eyes,int mouse,int weaponNo)
    {
        this.leftHandNo = lefthand;
        this.rightHandNo = righthand;
        this.faceNo = face;
        this.eyesNo = eyes;
        this.mouseNo = mouse;
        this.weaponNo = weaponNo;
    }
}
