using System.Linq;
using System.Collections.Generic;

[System.Serializable]
public class Person
{
    public static int num = 1;
    public string name;
    public int strikes;
    public List<string> reasonsForStrike = new List<string>();

    public Person(string name, int strikes, string[] reasons)
    {
        this.name = name;
        this.strikes = strikes;
        if (reasons != null)
            this.reasonsForStrike = reasons.ToList<string>();
        else
            this.reasonsForStrike = new List<string>();
    }

    public void AddStrike(string reason)
    {
        reasonsForStrike.Add(reason);
        strikes += 1;
    }
}
