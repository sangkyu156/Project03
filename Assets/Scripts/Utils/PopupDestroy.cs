using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupDestroy : MonoBehaviour
{
    public void PopupOff()
    {
        Destroy(gameObject);
    }

    public void ParentPopupOff()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
