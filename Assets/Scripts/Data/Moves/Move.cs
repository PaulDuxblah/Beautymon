using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Move
{
    public string name = "";
    public string description = "";
    public Type type = null;

    public bool targetsEnemy = true;
    public Condition inflictsCondition = null;
    public int inflictsConditionModifier = 0;
    public bool inflictsConditionForce = false;

    public string modifiesStat = null;
    public int modifiesStatModifier = 0;

    public Climate invokesClimate = null;

    public bool makesContact = false;

    public bool attacksOnTheOtherStat = false;

    public int power = 0;
    public bool isPhysical = true;

    public static Move load(string moveName)
    {
        StreamReader reader = new StreamReader("Assets\\Scripts\\Data\\Moves\\" + moveName + ".json");
        string json = reader.ReadToEnd();
        reader.Close();

        Move move = JsonUtility.FromJson<Move>(json);

        json = json.Replace("{", "").Replace("}", "").Replace("\"", "").Replace("  ", "");
        string[] jsonArray = json.Split('\n');

        foreach (string line in jsonArray) {
            string editedLine = Utility.cleanJson(line);

            if (editedLine.Contains("type: ")) {
                move.type = Type.load(editedLine.Replace("type: ", "").Replace("\"", "").Replace(",", ""));
            } else if (editedLine.Contains("inflictsCondition: ")) {
                move.inflictsCondition = Condition.load(editedLine.Replace("inflictsCondition: ", "").Replace("\"", "").Replace(",", ""));
            } else if (editedLine.Contains("inflictsConditionModifier: ")) {
                move.inflictsConditionModifier = int.Parse(editedLine.Replace("inflictsConditionModifier: ", "").Replace("\"", "").Replace(",", ""));
            } else if (editedLine.Contains("inflictsConditionForce: ")) {
                move.inflictsConditionForce = editedLine.Contains("inflictsConditionForce: \"true\"") || editedLine.Contains("inflictsConditionForce: true");
            } else if (editedLine.Contains("invokesClimate: ")) {

            }
        }

        return move;
    }
}
