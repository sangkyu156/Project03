using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : EnemyBase
{
    public string idName;

    void Update()
    {
        if (currentHealth <= 0 && drop == false)
        {
            Death();
            if (drop == false)
            {
                drop = true;
                for (int i = 0; i < 1; i++)
                {
                    Drop();
                }
            }
        }
        else if (drop == false) //else로 바꿔도 되는지 테스트 해야함
        {
            TargetConfirm();
        }

        Rotation();

        PositionReset();

        Knockback();
    }

    protected override void SetAbility()
    {
        base.SetAbility();
        maxHealth = 6;
        speed = 3;
        power = 1;
    }

    //오브젝트 비활성화
    void OnTargetReached()
    {
        if (gameObject.activeSelf)
            CreateManager.Instance.ReturnPool(this);
    }
}
