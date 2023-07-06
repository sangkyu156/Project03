using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Larva2 : EnemyBase
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
        else if (drop == false)
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
        maxHealth = 13;
        speed = 3.1f;
        power = 1;
    }

    //오브젝트 비활성화
    void OnTargetReached()
    {
        if (gameObject.activeSelf)
            CreateManager.Instance.ReturnPool(this);
    }
}
