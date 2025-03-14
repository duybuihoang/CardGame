using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogParser 
{
    public static DialogData ParseFromJson(string jsonText)
    {
        return JsonUtility.FromJson<DialogData>(jsonText);
    }

    public static string ConvertToJson(DialogData dialogData)
    {
        return JsonUtility.ToJson(dialogData, true);
    }
}
