#pragma warning disable 0219

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.Text;
using System;
using System.Reflection;

public class ExcelImporterMaker
{
    private enum ValueType
    {
        BOOL,
        STRING,
        INT,
        FLOAT,
        DOUBLE,
        LONG,
        NONE,
    }

    static private string rawData = string.Empty;
    static private string loadData = string.Empty;

    static private string jsonData = string.Empty;
    static private List<ExcelRowParameter> typeList = new List<ExcelRowParameter>();
    static private List<ExcelSheetParameter> sheetList = new List<ExcelSheetParameter>();

    [MenuItem("MyGame/엑셀 데이터 가져오기", false, 202)]
    static void ExportExcelToAssetbundleX()
    {
        // 공통은 양쪽에다 익스포트한다.
        ExportExcelToAssetbundlePath(Application.dataPath + "/../DataBase", "Assets/StreamingAssets/DataBase/", 0);        
        EditorUtility.ClearProgressBar();
    }

    static void ExportExcelToAssetbundlePath(string path, string targetPath, float startProgress)
    {
        sheetList.Clear();

        // 특정 폴더를 검색하여 파일을 얻어와서 처리한다.        
        DirectoryInfo info = new DirectoryInfo(path);
        FileInfo[] fi = info.GetFiles();
        int i, n = fi.Length;
        int a = 0, b = 0;
        for (i = 0; i < n; i++)
        {
            if (fi[i].Extension == ".meta")
                continue;

            if (fi[i].FullName.Contains("~$"))
                continue;

            if (fi[i].FullName.Contains(".txt"))
                continue;

            if (EditorUtility.DisplayCancelableProgressBar("Excel importing...", fi[i].FullName, ((float)i / (float)n) * 0.25f + startProgress))
                break;

            using (FileStream stream = File.Open(fi[i].FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                Debug.Log("파일명 : " + fi[i].FullName);

                typeList.Clear();

                IWorkbook book = null;
                if (Path.GetExtension(fi[i].FullName) == ".xls")
                {
                    book = new HSSFWorkbook(stream);
                }
                else
                {
                    book = new XSSFWorkbook(stream);
                }

                ISheet s = book.GetSheetAt(0);  // 첫번째 시트만 빼온다.
                ExcelSheetParameter sht = new ExcelSheetParameter();
                sht.sheetName = s.SheetName;
                sht.isEnable = true;
                sheetList.Add(sht);

                IRow titleRow = s.GetRow(0);
                for (a = 0; a < titleRow.LastCellNum; a++)
                {
                    ExcelRowParameter lastParser = null;
                    ExcelRowParameter parser = new ExcelRowParameter();
                    ICell cell = titleRow.GetCell(a);
                    if (cell == null) continue;
                    if (cell.CellType != CellType.String)
                    {
                        Debug.Log("변수명이 string 타입이 아닙니다. cell idx = " + a);
                        continue;
                    }

                    parser.name = cell.StringCellValue;

                    switch (parser.name[0])
                    {
                        case 'f': parser.type = ValueType.FLOAT; break;
                        case 's': parser.type = ValueType.STRING; break;
                        case 'i': parser.type = ValueType.INT; break;
                        case 'l': parser.type = ValueType.LONG; break;
                        case 'b': parser.type = ValueType.BOOL; break;
                        case 'n': parser.type = ValueType.NONE; break;
                        default: Debug.Log("변수 타입(f, s, i, l, b)이 지정되지 않았습니다. cell idx = " + a); continue;
                    }

                    typeList.Add(parser);
                }

                // db 로그 출력
                string debugStr = string.Empty;
                for (a = 0; a < typeList.Count; a++)
                {
                    debugStr += typeList[a].name + " | ";
                }
                Debug.Log(debugStr);


                jsonData = string.Empty;
                rawData = string.Empty;
                loadData = "\n";
                for (a = 1; a <= s.LastRowNum; a++)
                {
                    IRow dataRow = s.GetRow(a);

                    jsonData += "\t{ ";
                    debugStr = string.Empty;
                    for (b = 0; b < dataRow.LastCellNum; b++)
                    {
                        if (b >= typeList.Count)
                            continue;
                        if (typeList[b].type == ValueType.NONE)
                            continue;

                        jsonData += typeList[b].name;

                        ICell cell = dataRow.GetCell(b);
                        if (cell == null)
                        {
                            debugStr += "" + " | ";
                            rawData += "" + "[%]";
                            jsonData += ":\"" + "" + "\",";
                            if (a == 1)
                                loadData += "           data." + typeList[b].name + " = splited[idx++];\n";
                        }
                        else
                        {
                            switch (typeList[b].type)
                            {
                                case ValueType.BOOL:
                                    debugStr += cell.BooleanCellValue.ToString() + " | ";
                                    rawData += cell.BooleanCellValue.ToString() + "[%]";
                                    jsonData += ":\"" + cell.BooleanCellValue.ToString() + "\",";
                                    if (a == 1)
                                        loadData += "           data." + typeList[b].name + " = System.Convert.ToBoolean(splited[idx++]);\n";
                                    break;

                                case ValueType.FLOAT:
                                    debugStr += cell.NumericCellValue.ToString() + " | ";
                                    rawData += cell.NumericCellValue.ToString() + "[%]";
                                    jsonData += ":" + cell.NumericCellValue.ToString() + ",";
                                    if (a == 1)
                                        loadData += "           data." + typeList[b].name + " = System.Convert.ToSingle(splited[idx++]);\n";
                                    break;

                                case ValueType.INT:
                                    debugStr += cell.NumericCellValue.ToString() + " | ";
                                    rawData += cell.NumericCellValue.ToString() + "[%]";
                                    jsonData += ":" + cell.NumericCellValue.ToString() + ",";
                                    if (a == 1)
                                        loadData += "           data." + typeList[b].name + " = System.Convert.ToInt32(splited[idx++]);\n";
                                    break;

                                case ValueType.LONG:
                                    debugStr += cell.NumericCellValue.ToString() + " | ";
                                    rawData += cell.NumericCellValue.ToString() + "[%]";
                                    jsonData += ":" + cell.NumericCellValue.ToString() + ",";
                                    if (a == 1)
                                        loadData += "           data." + typeList[b].name + " = System.Convert.ToInt64(splited[idx++]);\n";
                                    break;

                                case ValueType.STRING:
                                    switch (cell.CellType)
                                    {
                                        case CellType.Numeric:
                                            debugStr += cell.NumericCellValue.ToString() + " | ";
                                            rawData += cell.NumericCellValue + "[%]";
                                            jsonData += ":\"" + cell.NumericCellValue.ToString().Replace('\n', ' ').Replace('\r', ' ') + "\",";
                                            break;
                                        case CellType.String:
                                            debugStr += cell.StringCellValue.ToString() + " | ";
                                            rawData += cell.StringCellValue + "[%]";
                                            jsonData += ":\"" + cell.StringCellValue.ToString().Replace('\n', ' ').Replace('\r', ' ') + "\",";
                                            break;
                                        case CellType.Blank:
                                            debugStr += "" + " | ";
                                            rawData += "" + "[%]";
                                            jsonData += ":\"" + "" + "\",";
                                            break;
                                    }

                                    if (a == 1)
                                        loadData += "           data." + typeList[b].name + " = splited[idx++];\n";
                                    break;

                                default:
                                    break;
                            }
                        }
                    }

                    jsonData += "}, \n";
                    Debug.Log(debugStr);
                }

                // 데이터를 채워 넣는다.
                string className = Path.GetFileNameWithoutExtension(fi[i].FullName);
                ExportEntity(className);

                Directory.CreateDirectory(targetPath);
                File.WriteAllText(targetPath + className + ".bytes", rawData.Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r"));

                //InvokeStringMethod5("Assembly-CSharp", "", className, "Load", string.Empty);

            }
        }

        AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);

        Debug.Log("변환이 완료되었습니다.");
    }

    static void ExportEntity(string className)
    {
        string templateFilePath = "Assets/Editor/EntityTemplate.txt";
        string entittyTemplate = File.ReadAllText(templateFilePath);
        StringBuilder builder = new StringBuilder();
        foreach (ExcelRowParameter row in typeList)
        {
            if (row.type == ValueType.NONE)
                continue;

            builder.AppendLine();
            builder.AppendFormat("		public {0} {1};", row.type.ToString().ToLower(), row.name);
        }

        entittyTemplate = entittyTemplate.Replace("$Types$", builder.ToString());
        entittyTemplate = entittyTemplate.Replace("$ExcelData$", className);
        entittyTemplate = entittyTemplate.Replace("$LoadData$", loadData);

        Directory.CreateDirectory("Assets/Script/DataBase/");
        File.WriteAllText("Assets/Script/DataBase/" + className + ".cs", entittyTemplate);
    }

    public static string InvokeStringMethod5(string assemblyName, string namespaceName, string typeName, string methodName, string stringParam)
    {
        string assemblyQualifiedName = string.Format("{0}.{1},{2}", namespaceName, typeName, assemblyName);
        Type calledType = Type.GetType(assemblyQualifiedName);
        if (calledType == null) throw new InvalidOperationException(assemblyQualifiedName + " not found");
        MethodInfo method = calledType.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic);
        switch (method.GetParameters().Length)
        {
            case 0:
                return (string)method.Invoke(null, null);
            case 1:
                return (string)method.Invoke(null, new object[] { stringParam });
            default:
                throw new NotSupportedException(methodName + " must have 0 or 1 parameter only");
        }
    }

    private class ExcelSheetParameter
    {
        public string sheetName;
        public bool isEnable;
    }

    private class ExcelRowParameter
    {
        public ValueType type;
        public string name;
        //public ExcelRowParameter nextArrayItem;
    }
}
