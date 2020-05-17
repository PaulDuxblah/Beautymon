using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Condition
{
    public string name;
    public string description;

    public static Condition load(string conditionName)
    {
        StreamReader reader = new StreamReader("Assets\\Scripts\\Data\\Conditions\\" + conditionName + ".json");
        string json = reader.ReadToEnd();
        reader.Close();

        return JsonUtility.FromJson<Condition>(json);
    }
}
