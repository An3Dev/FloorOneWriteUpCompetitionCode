using PlayFab;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform cardContainer;

    public DataManager dataManager;
    bool wasNotLoggedIn = true;



    List<Card> cards = new List<Card>();
    List<Person> lastSorted = new List<Person>();

    private void Start()
    {
        //InvokeRepeating(nameof(Refresh), 2f, 2f);
    }

    private void Update()
    {
        if (PlayFabClientAPI.IsClientLoggedIn() && wasNotLoggedIn)
        {
            wasNotLoggedIn = false;
            Refresh();
        }
    }

    public void Refresh()
    {
        print("Refresh");
        List<Person> people = dataManager.GetPeople();

        List<Person> sorted = people.OrderBy(o => o.strikes).ToList();
        sorted.Reverse();

        if (lastSorted.SequenceEqual<Person>(sorted))
        {
            for (int i = 0; i < lastSorted.Count; i++)
            {
                cardContainer.GetChild(i).GetComponent<Card>().SetStrikes(lastSorted[i].strikes);
            }
            return;
        }
        lastSorted = sorted;

        for (int i = 0; i < cardContainer.childCount; i++)
        {
            Destroy(cardContainer.GetChild(i).gameObject);
        }

        for (int i = 0; i < sorted.Count; i++)
        {
            GameObject cardGO = Instantiate(cardPrefab, cardContainer);
            Card newCard = cardGO.GetComponent<Card>();
            cards.Add(newCard);
            newCard.Setup(sorted[i], i + 1);
        }      
    }

}