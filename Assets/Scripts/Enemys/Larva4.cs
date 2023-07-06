using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Larva4 : EnemyBase
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
                for (int i = 0; i < 3; i++)
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
        maxHealth = 22;
        speed = 3.1f;
        power = 3;
    }

    //오브젝트 비활성화
    void OnTargetReached()
    {
        if (gameObject.activeSelf)
            CreateManager.Instance.ReturnPool(this);
    }
}
