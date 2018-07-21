using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_lm_info : enemyInfo {
    public override int getBaseHp(int level)
    {
        return 300 + ((int)(3000 * ((float)level / 100)));
    }
}
