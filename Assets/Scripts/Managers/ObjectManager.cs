using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    //오브젝트 생성
    public GameObject CreateObject(string name)
    {
        GameObject go = Managers.Resource.Instantiate($"Object/{name}");

        return go;
    }
}
