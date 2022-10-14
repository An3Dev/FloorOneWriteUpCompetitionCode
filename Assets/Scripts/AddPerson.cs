using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AddPerson : MonoBehaviour
{
    public DataManager dataManager;
    public CardGenerator cardGenerator;
    public TMP_InputField inputField;

    public void OnClickSubmitButton()
    {
        if (!dataManager.DoesPersonExist(inputField.text))
        {
            dataManager.AddPerson(inputField.text);
            print("Added person: " + inputField.text);
            inputField.text = "";
            cardGenerator.Refresh();
            gameObject.SetActive(false);
        }
        else
        {
            print("Person already in list");
        }
    }

    public void OnClickOutside()
    {
        //string[] array = new string[] { "this", "si" };
        //var card = new Person("Brian", 1, new string[] { "In the lounge" });

        inputField.text = "";
        gameObject.SetActive(false);
    }
}
