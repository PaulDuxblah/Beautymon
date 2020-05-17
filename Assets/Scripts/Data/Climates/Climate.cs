using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Climate
{
    public string name;
    public string description;

    public static Condition load(string climateName)
    {
        StreamReader reader = new StreamReader("Assets\\Scripts\\Data\\Climates\\" + climateName + ".json");
        string json = reader.ReadToEnd();
        reader.Close();

        return JsonUtility.FromJson<Condition>(json);
    }
}
