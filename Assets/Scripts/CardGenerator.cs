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
        //print("Refresh");
        List<Person> people = dataManager.GetPeople();

        List<Person> sorted = people.OrderBy(o => o.strikes).ToList();
        sorted.Reverse();
        if (cards.Count > 0 && lastSorted.Count > 0)
        {
            if (lastSorted.SequenceEqual<Person>(sorted)) // if the list is in the same order
            {
                print("The list is in the same order, only data that doesn't affect placement changed");

                for (int i = 0; i < lastSorted.Count; i++)
                {
                    Card card = cardContainer.GetChild(i).GetComponent<Card>();
                    if (lastSorted[i].strikes != sorted[i].strikes)
                        card.SetStrikes(sorted[i].strikes);

                    if (lastSorted[i].reasonsForStrike.SequenceEqual<string>(sorted[i].reasonsForStrike))
                        card.SetReasons(sorted[i].reasonsForStrike);
                }
                lastSorted = sorted;
                return;
            }
            else if (lastSorted.Count == sorted.Count) // this means that the order changed but there is a same amount of people
            {
                print("Order changed but same amount of people");
                List<Card> newCards = new List<Card>();
                for (int i = 0; i < sorted.Count; i++)
                {
                    // change the placement in the container
                    //Card outerCard = cards[i];
                    for(int p = 0; p < lastSorted.Count; p++)
                    {
                        if (lastSorted[p].name.Equals(sorted[i].name))
                        {
                            newCards.Add(cards[p]);
                            cards[p].SetPlacement(i);
                            cards[p].transform.SetSiblingIndex(i);
                            cards[p].SetStrikes(sorted[i].strikes);
                            cards[p].SetReasons(sorted[i].reasonsForStrike);
                            break;
                        }
                    }           
                }
                cards = newCards;
                lastSorted = sorted;

                return;
            }
        }
        
        lastSorted = sorted;

        print("New person was added or need to generate cards");
        for (int i = 0; i < cardContainer.childCount; i++)
        {
            Destroy(cardContainer.GetChild(i).gameObject);
        }
        cards.Clear();

        for (int i = 0; i < sorted.Count; i++)
        {
            GameObject cardGO = Instantiate(cardPrefab, cardContainer);
            Card newCard = cardGO.GetComponent<Card>();
            cards.Add(newCard);
            newCard.Setup(sorted[i], i);
        }      
    }

}