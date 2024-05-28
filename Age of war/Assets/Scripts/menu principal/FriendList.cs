using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using System.Collections;

public class FriendList : MonoBehaviour
{
    List<FriendInfo> _friends = null;
    enum FriendIdType { PlayFabId, Username, Email, DisplayName };

    string friendSearch;
    [SerializeField]
    GameObject friendPanel;
    [SerializeField]
    GameObject listingPrefab;
    [SerializeField]
    Transform friendScrollView;
    [SerializeField]
    TMP_InputField friendInputField; // Champ de saisie pour l'ID de l'ami
    [SerializeField]
    List<FriendInfo> myFriends;

    public void InputFriendID(string idIn)
    {
        friendSearch = idIn;
    }

    public void SubmitFriendRequest()
    {
        friendSearch = friendInputField.text;
        if (string.IsNullOrEmpty(friendSearch))
        {
            Debug.LogWarning("Friend ID is null or empty. Please enter a valid Friend ID.");
            return;
        }

        Debug.Log("Submitting friend request with ID: " + friendSearch);
        AddFriend(FriendIdType.PlayFabId, friendSearch);
    }

    public void OpencloseFriends()
    {
        friendPanel.SetActive(!friendPanel.activeInHierarchy);
    }

    void DisplayFriends(List<FriendInfo> friendsCache)
    {
        // Effacer le contenu actuel du scrollView
        foreach (Transform child in friendScrollView)
        {
            Destroy(child.gameObject);
        }

        // Effacer la liste des amis actuels
        myFriends.Clear();

        if (friendsCache == null)
        {
            Debug.LogWarning("Friends cache is null.");
            return;
        }

        foreach (FriendInfo f in friendsCache)
        {
            bool isFound = false;
            // Vérifier si l'ami est déjà dans myFriends pour éviter les doublons
            foreach(FriendInfo g in myFriends)
            {
                if(f.FriendPlayFabId == g.FriendPlayFabId)
                {
                    isFound = true;
                    break;
                }
            } 

            // Ajouter l'ami uniquement s'il n'est pas déjà dans myFriends
            if (!isFound)
            {
                myFriends.Add(f);
                
                GameObject listing = Instantiate(listingPrefab, friendScrollView);
                ListingPrefab tempListing = listing.GetComponent<ListingPrefab>();
                print(tempListing);
                if (tempListing != null && tempListing.playerNameText != null)
                {
                    // Assurez-vous que ListingPrefab a un TMP_Text nommé playerNameText
                    tempListing.playerNameText.text = f.TitleDisplayName ?? "Unknown"; // Utilisez TitleDisplayName ou utilisez "Unknown" en cas de valeur null
                }
                else
                {
                    Debug.LogWarning("Temp listing or player name text is null.");
                }
            }
        }
    }




    void DisplayPlayFabError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    void DisplayError(string error)
    {
        Debug.LogError(error);
    }

    IEnumerator WaitForFriend()
    {
        yield return new WaitForSeconds(2);
        GetFriends();
    }

    public void RunWaitFunction(){
        StartCoroutine(WaitForFriend());
    }

    public void GetFriends()
    {
        PlayFabClientAPI.GetFriendsList(new GetFriendsListRequest(), result =>
        {
            _friends = result.Friends;
            DisplayFriends(_friends);
        }, DisplayPlayFabError);
    }

    void AddFriend(FriendIdType idType, string friendId)
    {
        Debug.Log("Adding friend with ID type: " + idType + " and ID: " + friendId);
        var request = new AddFriendRequest();
        bool validRequest = false;

        switch (idType)
        {
            case FriendIdType.PlayFabId:
                request.FriendPlayFabId = friendId;
                validRequest = true;
                break;
            case FriendIdType.Username:
                request.FriendUsername = friendId;
                validRequest = true;
                break;
            case FriendIdType.Email:
                request.FriendEmail = friendId;
                validRequest = true;
                break;
            case FriendIdType.DisplayName:
                request.FriendTitleDisplayName = friendId;
                validRequest = true;
                break;
        }

        if (validRequest)
        {
            PlayFabClientAPI.AddFriend(request, result =>
            {
                Debug.Log("Friend added successfully!");
                GetFriends();
            }, DisplayPlayFabError);
        }
        else
        {
            Debug.LogError("Invalid friend request. No valid ID type provided.");
        }
    }

    void RemoveFriend(FriendInfo friendInfo)
    {
        PlayFabClientAPI.RemoveFriend(new RemoveFriendRequest
        {
            FriendPlayFabId = friendInfo.FriendPlayFabId
        }, result =>
        {
            _friends.Remove(friendInfo);
            DisplayFriends(_friends);
        }, DisplayPlayFabError);
    }
}
