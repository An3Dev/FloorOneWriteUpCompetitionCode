using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Card : MonoBehaviour
{
    public static int num = 1;
    public TextMeshProUGUI username, placement, strikes;

    public Transform reasonsContainer;
    public GameObject reasonTextPrefab;

    private Person person;

    public void Setup(Person person, int placement)
    {
        this.person = person;
        username.text = person.name;
        if (this.person.strikes == 1)
        {
            this.strikes.text = person.strikes.ToString() + " strike";
        }
        else
        {
            this.strikes.text = person.strikes.ToString() + " strikes";

        }
        this.placement.text = placement.ToString();

        PopulateReasonsContainer(person.reasonsForStrike);
    }

    public void SetPlacement(int placement)
    {
        this.placement.text = placement.ToString();
    }

    public void SetStrikes(int strikes)
    {
        if (strikes == 1)
        {
            this.strikes.text = strikes.ToString() + " strike";
        }
        else
        {
            this.strikes.text = strikes.ToString() + " strikes";

        }
    }

    void PopulateReasonsContainer(List<string> reasons)
    {
        foreach(string reason in reasons)
        {
            Instantiate(reasonTextPrefab, reasonsContainer).GetComponent<TextMeshProUGUI>().text = reason;
        }
        print("Populated reasons");
    }
}
