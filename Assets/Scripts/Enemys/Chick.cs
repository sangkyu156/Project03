using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chick : EnemyBase
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
        else if (drop == false) //else�� �ٲ㵵 �Ǵ��� �׽�Ʈ �ؾ���
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
        maxHealth = 5;
        speed = 3;
        power = 1;
    }

    //������Ʈ ��Ȱ��ȭ
    void OnTargetReached()
    {
        if (gameObject.activeSelf)
            CreateManager.Instance.ReturnPool(this);
    }
}
