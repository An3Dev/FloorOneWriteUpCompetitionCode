using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Card : MonoBehaviour
{
    public static int num = 1;
    public TextMeshProUGUI username, placement, strikes;

    public Transform reasonsContainer;
    public GameObject reasonsTitleText;
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
        this.placement.text = (placement + 1).ToString();
    }

    public Person GetPerson()
    {
        return person;
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

    public void SetReasons(List<string> reasons)
    {
        PopulateReasonsContainer(reasons);
    }

    void PopulateReasonsContainer(List<string> reasons)
    {
        string s = "";
        reasonsTitleText.SetActive(true);
        if (reasons.Count > 0)
        {
            foreach (string reason in reasons)
            {
                s += reason + ", ";
            }
            s = s.Substring(0, s.Length - 2); // remove the last comma
        }
        else
        {
            reasonsTitleText.SetActive(false);

            s = "Has no strikes";
        }

        if (reasonsContainer.childCount > 0)
        {
            reasonsContainer.GetChild(0).GetComponent<TextMeshProUGUI>().text = s;
        }
        else
        {
            Instantiate(reasonTextPrefab, reasonsContainer).GetComponent<TextMeshProUGUI>().text = s;
        }
        //foreach(string reason in reasons)
        //{
        //    Instantiate(reasonTextPrefab, reasonsContainer).GetComponent<TextMeshProUGUI>().text = reason;
        //}
        //print("Populated reasons");
    }
}
