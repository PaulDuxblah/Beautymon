using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Talent
{
    public string name;
    public string description;

    public static Talent load(string talentName)
    {
        StreamReader reader = new StreamReader("Assets\\Scripts\\Data\\Talents\\" + talentName.Replace(" ", "") + ".json");
        string json = reader.ReadToEnd();
        reader.Close();

        return JsonUtility.FromJson<Talent>(json);
    }
}
