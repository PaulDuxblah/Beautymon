using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Type
{
    public string name;
    public List<string> resistances;
    public List<string> weaknesses;
    public List<string> immunities;


    public bool isResistantAgainst(Type type)
    {
        return resistances.Contains(type.name);
    }

    public bool IsWeakAgainst(Type type)
    {
        return weaknesses.Contains(type.name);
    }

    public bool IsImmunedAgainst(Type type)
    {
        return immunities.Contains(type.name);
    }

    public static Type load(string typeName)
    {
        StreamReader reader = new StreamReader("Assets\\Scripts\\Data\\Types\\" + typeName + ".json");
        string json = reader.ReadToEnd();
        reader.Close();

        return JsonUtility.FromJson<Type>(json);
    }
}
