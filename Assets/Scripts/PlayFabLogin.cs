using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System.Collections.Generic;

public class PlayFabLogin : MonoBehaviour
{
    public static string playfabID;
    public void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            /*
            Please change the titleId below to your own titleId from PlayFab Game Manager.
            If you have already set the value in the Editor Extensions, this can be skipped.
            */
            PlayFabSettings.staticSettings.TitleId = "60D33";
        }
        var request = new LoginWithCustomIDRequest { CustomId = "CustomID", CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        //var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
        //var requestEmail = new LoginWithEmailAddressRequest { Email = "an3marcelo@gmail.com", Password = "Test12355", TitleId = "60D33" };
        //PlayFabClientAPI.LoginWithEmailAddress(requestEmail, OnLoginSuccess, OnLoginFailure);
        //var registerRequest = new RegisterPlayFabUserRequest { Email = "an3marcelo@gmail.com", Password = "Test12355", RequireBothUsernameAndEmail = false };
        //PlayFabClientAPI.RegisterPlayFabUser(registerRequest, errorCallback: OnLoginFailure, resultCallback: OnRegister);
        //PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        //SetUserData();
        playfabID = result.PlayFabId;
        //GetUserData(playfabID);
    }

    private void OnRegister(RegisterPlayFabUserResult result)
    {
        print("Successfully registered");
    }

    void SetUserData()
    {
        var dict = new Dictionary<string, string>() { };
        dict.Add("Info", "");
        foreach (string player in Data.People)
        {
            dict.Add(player, "{}");
        }   
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = dict     
        },
        result => Debug.Log("Successfully updated user data"),
        error => {
            Debug.Log("Got error setting user data Ancestor to Arthur");
            Debug.Log(error.GenerateErrorReport());
        });
    }

    void GetUserData(string myPlayFabId)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = myPlayFabId,
            Keys = null
        }, result => {
            Debug.Log("Got user data:");
            foreach (string player in Data.People)
            {
                print(result.Data[player].Value);
            }
            //if (result.Data == null || !result.Data.ContainsKey("Ancestor")) Debug.Log("No Ancestor");
            //else Debug.Log("Ancestor: " + result.Data["Ancestor"].Value);
        }, (error) => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.GenerateErrorReport());
        });
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }
}
