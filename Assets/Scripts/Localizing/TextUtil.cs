using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class TextUtil : MonoBehaviour
{
    static Dictionary<string, TextDB.Data> textIDBaseData = new Dictionary<string, TextDB.Data>();
    static public TextUtil instance;
    static public int languageNumber = 1;

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    static public string GetText(string id)
    {
        id = id.Trim();
        if (textIDBaseData.ContainsKey(id) == false)
        {
            Debug.Log("id missing! - " + id);
            Load();
            //BuildTextIDBaseData();
        }

        // �ٽ� �ѹ� Ȯ���ؼ� ���ٸ� �װ� ���°Ŵ� �׳� ���ڿ��� �����Ѵ�.
        if (textIDBaseData.ContainsKey(id) == false)
        {
            return ("�������");
        }

        TextDB.Data textData = textIDBaseData[id];
        if (textData == null)
            return string.Empty;

        return GetText(textData);
    }

    static public string GetText(TextDB.Data data)
    {
        //TextDB.Data data = TextDB.GetData(id);
        if (data == null)
            return string.Empty;

        if (languageNumber == 0)
            Setup.language = Setup.Language.Korean;
        else if (languageNumber == 1)
            Setup.language = Setup.Language.Japanese;
        else if (languageNumber == 2)
            Setup.language = Setup.Language.English;

        switch (Setup.language)
        {//sJPN sKOR sENG
            case Setup.Language.Korean: return CheckText(data.sKOR);
            case Setup.Language.Japanese: return CheckText(data.sJPN);
            case Setup.Language.English: return CheckText(data.sENG);
            default: return CheckText(data.sJPN);
        }
    }
    static public string CheckText(string inputText)
    {
        string outputText = inputText;

        int cnt = 0;
        // ���͹��� ���� ������ ��� �����Ѵ�.
        int i;
        bool removeMode = false;
        for (i = outputText.Length - 1; i >= 0; i--)
        {
            if (i > 1 && outputText[i] == '\n')
            {
                removeMode = true;
                continue;
            }

            if (removeMode == true)
            {
                if (outputText[i] == ' ')
                {
                    outputText = outputText.Remove(i, 1);
                    //Debug.Log("remove index = " + i);
                }
                else
                    removeMode = false;
            }

            // ������ġ!
            cnt++;
            if (cnt > 30000)
                break;
        }

        return outputText.Replace("^", "\n");
    }

    static void Load()
    {
        int i;
        for (i = 0; i < TextDB.GetDataSize(); i++)
        {
            TextDB.Data data = TextDB.GetDataByIndex(i);
            textIDBaseData[data.sTextID] = data;
        }
    }
}
