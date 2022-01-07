using Firebase.Extensions;
using Firebase.Database;
using Firebase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TopTop.Toast;
using TopTop.GameData;
using System;
using Newtonsoft.Json.Serialization;

public class FirebaseUploadController : MonoBehaviour
{
    List<HighScore> data;
    //FirebaseFirestore db;
    DatabaseReference reference;

    [SerializeField] private TextMeshProUGUI shareNameEditText;
    [SerializeField] private Data gameData;
    [SerializeField] private GameObject cicleBarSharePanel;
    [SerializeField] private GameObject sharePanel;
    [SerializeField] private ShowToast showToast;
    [SerializeField] private Timer timer;
    [SerializeField] private FirebaseDownloadController firebaseDownloadController;

    // Start is called before the first frame update
    void Start()
    {
        data = new List<HighScore>();

        reference = FirebaseDatabase.DefaultInstance.RootReference;
     
        //db = FirebaseFirestore.DefaultInstance;

    }

    public void Update()
    {
        if(timer.getTime() >= 15000)
        {
            timer.clearTimer();
            cicleBarSharePanel.SetActive(false);
            sharePanel.SetActive(false);
            showToast.MyShowToastMethod("High Score Not Shared");
        }
    }

    public void addHighScore()
    {
        string highScoreName = shareNameEditText.text;
        if (highScoreName.Length <= 1)
        {
            showToast.MyShowToastMethod("Name cannot be empty");
        }
        else if(highScoreName.Length > 7)
        {
            showToast.MyShowToastMethod("The name cannot be longer than 6 characters");
        }
        else
        {
            timer.StartTimer();
            cicleBarSharePanel.SetActive(true);

            /*
            DocumentReference docRef = db.Collection("highScores").Document(highScoreName);
            Dictionary<string, object> score = new Dictionary<string, object>
            {
                { "Name", highScoreName },
                { "HighScore", gameData.HighScore }
            };
            docRef.SetAsync(score)
                .ContinueWithOnMainThread(task => {
                Debug.Log("Added data to the " + highScoreName + " document in the cities collection.");
                showToast.MyShowToastMethod("High Score Shared");
                cicleBarSharePanel.SetActive(false);
                sharePanel.SetActive(false);
                });*/

            shareHigScore();
        }
    }

    public void shareHigScore()
    {
        if (PlayerPrefs.GetString("UUID", "null") == "null")
        {
            Guid myuuid = Guid.NewGuid();
            PlayerPrefs.SetString("UUID", myuuid.ToString());
        }
        string highScoreName = shareNameEditText.text.ToString();
        int highScore = gameData.HighScore;
        string uuid = PlayerPrefs.GetString("UUID", "null");
        HighScore hs = new HighScore(uuid, highScoreName, highScore);
        string json = JsonUtility.ToJson(hs);
        isHgNameOkay(uuid, highScoreName, 
            () => {

                reference.Child("users").Child(uuid).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task => {
                    showToast.MyShowToastMethod("High Score Shared");
                    sharePanel.SetActive(false);
                    PlayerPrefs.SetInt("highScoreIsCurrent", 0); //hg güncel
                    cicleBarSharePanel.SetActive(false);
                    timer.clearTimer();
                    firebaseDownloadController.loadData();
                });

            },
            () => {

                showToast.MyShowToastMethod("Error! Name is not available.");
                cicleBarSharePanel.SetActive(false);
                timer.clearTimer();
                firebaseDownloadController.loadData();
            });
    }

    public void isHgNameOkay(string uuid, string highScoreName, Action onSuccess, Action onFailure)
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("users")
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("IsFaulted");
                }
                else if (task.IsCompleted)
                {
                    bool flag = false;
                    bool isHaveUUID = false;
                    bool isHaveName = false;
                    data.Clear();
                    DataSnapshot snapshot = task.Result;

                    foreach (var dataSnapshot in snapshot.Children)
                    {
                        data.Add(new HighScore(dataSnapshot.Child("uuid").Value.ToString(), 
                            dataSnapshot.Child("highScoreName").Value.ToString(), 
                            int.Parse(dataSnapshot.Child("highScore").Value.ToString())));
                    }

                    foreach (var hg in data)
                    {
                        if (uuid.Equals(hg.uuid))
                        {
                            isHaveUUID = true;
                        }

                        if (highScoreName == hg.highScoreName)
                        {
                            isHaveName = true;
                        }

                        if (uuid == hg.uuid && highScoreName == hg.highScoreName)
                        {
                            flag = true;
                        }
                    }

                    if (isHaveUUID)
                    {
                        if (!isHaveName)
                        {
                            flag = true;
                            deleteOldHighScore(uuid);
                        }
                    }

                    if (isHaveUUID == false && isHaveName == false)
                    {
                        flag = true;
                    }

                    if (flag)
                    {
                        onSuccess();
                    }
                    else
                    {
                        onFailure();
                    }
                }
            });
    }

    private void deleteOldHighScore(string uuid)
    {
        Debug.Log(uuid);
        reference.Child("users").Child(uuid).RemoveValueAsync();
    }
}
