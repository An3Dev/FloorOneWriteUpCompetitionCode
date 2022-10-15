using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class DataManager : MonoBehaviour
{
    private const string Key = "Info";
    bool wasNotLoggedIn = true;
    List<Person> people = new List<Person>();
    public CardGenerator cardGenerator;
    private void Start()
    {
        //Test a = new Test();
        //a.a = "asdf";
        //Test[] t = new Test[] { a, a };
        //(JsonConvert.SerializeObject(t));

        //print(JsonUtility.ToJson(new Test()));

    }

    private void Update()
    {
        if (PlayFabClientAPI.IsClientLoggedIn() && wasNotLoggedIn)
        {
            wasNotLoggedIn = false;
            print("Client logged in");

            // Get the current user data
            // turn the json to objects
            // put objects in a list
            CheckData();
            InvokeRepeating(nameof(CheckData), 1, 0.5f);
        }
    }

    void CheckData()
    {
        GetUserData(PlayFabLogin.playfabID);
        cardGenerator.Refresh();
        if (people == null || people.Count == 0)
        {
            return;
        }
        //foreach (var item in people)
        //{
        //    print(item.ToString());
        //}
    }

    public void AddPerson(string name)
    {
        Person person = new Person(name, 0, new string[0]);
        people.Add(person);
        // upload user data to playfab
        SetUserData();
    }

    public void AddReason(Person target, string newReason)
    {
        target.AddStrike(newReason);
        //print("strikes: " + target.reasonsForStrike[0]);
        // upload user data to playfab
        SetUserData();
    }

    public List<Person> GetPeople()
    {
        return people;
    }

    public bool DoesPersonExist(string name)
    {
        foreach(var person in people)
        {
            if (person.name.ToLower().Equals(name.ToLower()))
            {
                return true;
            }
        }
        return false;
    }

    public Person GetPersonByName(string name)
    {
        foreach (var person in people)
        {
            if (person.name.ToLower().Equals(name.ToLower()))
            {
                return person;
            }
        }

        return null;
    }

    void SetUserData()
    {
        var dict = new Dictionary<string, string>() { };
        
        //foreach (string player in Data.People)
        //{
        //    dict.Add(player, "{}");
        //}
        string json = JsonConvert.SerializeObject(people);
        //print("person: " + people[0].name);
        //print("JSON: " + json);
        dict.Add(Key, json);
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = dict
        },
        result => Debug.Log("Successfully updated user data"),
        error =>
        {
            //Debug.Log("Got error uploading user data");
            Debug.Log(error.GenerateErrorReport());
        });
    }

    void GetUserData(string myPlayFabId)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = myPlayFabId,
            Keys = null
        }, result =>
        {
            //Debug.Log("Got user data:");
            if (!result.Data.ContainsKey(Key))
            {
                return;
            }

            string json = result.Data[Key].Value;
            if (json.Length > 0)
            {
                
                people = JsonConvert.DeserializeObject<List<Person>>(json);
            }

            //foreach(var item in people)
            //{
            //    print(item.name);
            //}
            //foreach (string player in Data.People)
            //{a
            //    print(result.Data[player].Value);
            //}
            //if (result.Data == null || !result.Data.ContainsKey("Ancestor")) Debug.Log("No Ancestor");
            //else Debug.Log("Ancestor: " + result.Data["Ancestor"].Value);
        }, (error) =>
        {
            //Debug.Log("Got error retrieving user data:");
            Debug.Log(error.GenerateErrorReport());
        });
    }
}
