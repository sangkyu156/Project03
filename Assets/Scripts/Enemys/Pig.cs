using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : EnemyBase
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
        maxHealth = 12;
        speed = 3.1f;
        power = 1;
    }

    //오브젝트 비활성화
    void OnTargetReached()
    {
        if (gameObject.activeSelf)
            CreateManager.Instance.ReturnPool(this);
    }

    //포션드랍
    void PotionDrop()
    {
        GameObject pottion = Managers.Resource.Instantiate("Object/HP_Potion");
        float posX = Random.Range(-0.5f, 0.3f);
        float posY = Random.Range(-0.5f, 0.3f);
        pottion.transform.position = new Vector2(transform.position.x + posX, transform.position.y + posY);
    }
}
