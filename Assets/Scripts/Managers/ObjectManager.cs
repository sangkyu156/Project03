using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    //������Ʈ ����
    public GameObject CreateObject(string name)
    {
        GameObject go = Managers.Resource.Instantiate($"Object/{name}");

        return go;
    }
}
