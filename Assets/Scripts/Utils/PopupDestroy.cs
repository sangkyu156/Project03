using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupDestroy : MonoBehaviour
{
    public void PopupOff()
    {
        Managers.Sound.Play("Button01");
        Destroy(gameObject);
    }

    public void ParentPopupOff()
    {
        Managers.Sound.Play("Button01");
        Destroy(gameObject.transform.parent.gameObject);
    }
}
