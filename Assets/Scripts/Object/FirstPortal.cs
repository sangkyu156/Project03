using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FirstPortal : MonoBehaviour
{
    GameObject stageCanvas;
    GameObject stageScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "NoDamage")
        {
            GameObject store = Managers.Resource.Instantiate("UI/Popup/StorePopup");
            store.transform.SetParent(stageCanvas.transform, false);
            stageScene.GetComponent<StageScene>().FieldMoneyLastSibling();
            Destroy(gameObject);
        }
    }
    void Start()
    {
        stageCanvas = GameObject.FindGameObjectWithTag("StageCanvas");
        stageScene = GameObject.FindGameObjectWithTag("StageScene");
    }
}
