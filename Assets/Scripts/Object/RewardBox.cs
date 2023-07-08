using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBox : MonoBehaviour
{
    public Ease ease;

    private void Start()
    {
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.7f).SetEase(ease).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
    }
}
