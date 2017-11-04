using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class SGUtils {

    //JSON Array의 값이 JSON Object 일때 키와 값이 맞는 JSON Object를 리턴
    static public LitJson.JsonData GetJsonArrayForKey(LitJson.JsonData jsonData, string key, object value)
    {
        if (jsonData.IsArray == false)
            return null;

        for (int jsonCnt = 0; jsonCnt < jsonData.Count; jsonCnt++)
        {
            if (jsonData[jsonCnt].Keys.Contains(key))
            {
                if (value.GetType() == typeof(int))
                {
                    if ((int)jsonData[jsonCnt][key] == (int)value)
                        return jsonData[jsonCnt];
                }
                else
                {
                    if (jsonData[jsonCnt][key].ToString() == (string)value)
                        return jsonData[jsonCnt];
                }
            }
        }
        return null;
    }
}
