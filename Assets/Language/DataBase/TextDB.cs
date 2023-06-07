using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextDB
{	
	static string tableName = "TextDB";
	static List<Data> dataList = new List<Data> ();
	static Dictionary<int, Data> dataDic = new Dictionary<int, Data>();
	
	public class Data
	{
		
		public int iId;
		public string sTextID;
		public string sDesc;
		public string sJPN;
		public string sKOR;
		public string sENG;
	}
	
	static public Data GetData(int id)
	{
		if(dataDic.Count == 0)
		{
			Load();
		}
		
		if(dataDic.ContainsKey(id)==false)
        {
            Debug.Log("테이블 " + tableName + "에 " + id + "가 없음");
            return null;
        }
		
		return dataDic[id];
	}
	
	static public Data GetDataByIndex(int idx)
	{
		if(dataList.Count == 0)
		{
			Load();
		}
		
		return dataList[idx];
	}

	static public int GetDataSize()
    {
		if(dataList.Count == 0)
		{
			Load();
		}

        return dataList.Count;
    }
	
	static void Load()
	{
		//Debug.Log("DataBase Path = " +Application.streamingAssetsPath + "/DataBase/TextDB.bytes");
		string rawData = System.IO.File.ReadAllText(Application.streamingAssetsPath  + "/DataBase/TextDB.bytes");
		rawData = rawData.Replace("\\n", "\n");
		rawData = rawData.Replace("\\\"", "\"");
		
		string[] splitCodes = { "[%]" };
        string[] splited = rawData.Split(splitCodes, System.StringSplitOptions.None); //AES.decrypt(rawData).Split(splitCodes, System.StringSplitOptions.None);

        int idx = 0;
        bool flag = true;
        while(flag)
        {
            Data data = new Data();
            
           data.iId = System.Convert.ToInt32(splited[idx++]);
           data.sTextID = splited[idx++];
           data.sDesc = splited[idx++];
           data.sJPN = splited[idx++];
           data.sKOR = splited[idx++];
           data.sENG = splited[idx++];

			dataList.Add(data);
			dataDic[data.iId]=data;
            if (idx+1 >= splited.Length)
                break;
        }
	}
}