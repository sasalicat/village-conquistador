using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PassiveEquipment : Equipment {
    void passiveSkill(Vector3 clickPos);
}
