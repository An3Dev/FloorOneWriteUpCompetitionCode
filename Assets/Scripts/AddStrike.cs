using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AddStrike : MonoBehaviour
{
    public DataManager dataManager;
    public TMP_Dropdown dropdown;
    public CardGenerator cardGenerator;
    public TMP_InputField inputField;

    private void OnEnable()
    {
        // generate the dropdown options    
        GenerateDropdownOptions();
    }

    void GenerateDropdownOptions()
    {
        List<Person> peopleList = dataManager.GetPeople();
        List<string> names = new List<string>();
        
        foreach(var people in peopleList)
        {
            names.Add(people.name);
        }
        names.Sort();
        dropdown.ClearOptions();
        dropdown.AddOptions(names);
    }

    public void OnClickSubmitButton()
    {
        Person target = dataManager.GetPersonByName(dropdown.options[dropdown.value].text);
        if (target != null)
        {
            dataManager.AddReason(target, inputField.text);
            inputField.text = "";
            cardGenerator.Refresh();

            gameObject.SetActive(false);
        }
        else
        {
            print("Person does not exist");
        }

        //if (!dataManager.DoesPersonExist(inputField.text))
        //{
        //    dataManager.AddPerson(inputField.text);
        //    print("Added person: " + inputField.text);
        //}
        //else
        //{
        //    print("Person already in list");
        //}
    }

    public void OnClickOutside()
    {
        inputField.text = "";

        gameObject.SetActive(false);
    }
}
