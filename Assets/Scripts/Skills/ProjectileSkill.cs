using UnityEngine;

abstract public class ProjectileSkill : MonoBehaviour
{
    protected int curPower = 0;
    protected int nextPower = 0;
    protected float nextCooldown = 0;
    protected float deadTiem = 0;

    //������ ���� �ɷ�ġ ����
    abstract public void SetAbility();

    //�߻�
    abstract public void Shoot();

    //������Ʈ ��Ȱ��ȭ
    abstract public void OnTargetReached();
}
