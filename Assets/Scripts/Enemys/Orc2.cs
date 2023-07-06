using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc2 : EnemyBase
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
                Drop2();
                Drop2();
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
        maxHealth = 900;
        speed = 2.4f;
        power = 12;
    }

    //������Ʈ ��Ȱ��ȭ
    void OnTargetReached()
    {
        if (gameObject.activeSelf)
            CreateManager.Instance.ReturnPool(this);
    }
}
