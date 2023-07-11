using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig5 : EnemyBase
{
    public string idName;

    void Update()
    {
        if (currentHealth <= 0 && drop == false)
        {
            Death();
            Managers.Data.pigCount++;
            if (drop == false)
            {
                drop = true;
                for (int i = 0; i < 1; i++)
                {
                    Drop2();
                }

                if (Random.Range(0, 20) == 7)
                {
                    PotionDrop();
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
        maxHealth = 37;
        speed = 4f;
        power = 6;
    }

    //������Ʈ ��Ȱ��ȭ
    void OnTargetReached()
    {
        if (gameObject.activeSelf)
            CreateManager.Instance.ReturnPool(this);
    }

    //���ǵ��
    void PotionDrop()
    {
        GameObject pottion = Managers.Resource.Instantiate("Object/HP_Potion");
        float posX = Random.Range(-0.5f, 0.3f);
        float posY = Random.Range(-0.5f, 0.3f);
        pottion.transform.position = new Vector2(transform.position.x + posX, transform.position.y + posY);
    }
}
