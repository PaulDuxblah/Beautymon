using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Utility
{
    public static string cleanJson(string json)
    {
        string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
        invalid = invalid.Replace(":", "");
        foreach (char c in invalid) {
            json = json.Replace(c.ToString(), "");
        }
        return json;
    }
}
