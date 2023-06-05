using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LabelTextID : MonoBehaviour
{
    static public LabelTextID instance;

    public string textBaseID;

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    private void Start()
    {
        Set();
    }

    public void Set()
    {

        TextMeshProUGUI label = gameObject.GetComponent<TextMeshProUGUI>();
        if (label == null)
        {
            Debug.LogError("LabelTextID를 사용하려면 TextMeshPro 컴포넌트가 반드시 필요합니다!");
            return;
        }


        label.text = TextUtil.GetText(textBaseID);
    }

    private void Update()
    {
        Set();
    }
}