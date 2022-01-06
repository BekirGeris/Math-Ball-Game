using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FirebaseDownloadController : MonoBehaviour
{
    List<HighScore> data;

    [SerializeField] private List<TextMeshProUGUI> highScoreTexts;

    // Start is called before the first frame update
    void Start()
    {
        data = new List<HighScore>();

        loadData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loadData()
    {
        data.Clear();

        foreach (var text in highScoreTexts)
        {
            text.text = "";
        }

        FirebaseDatabase.DefaultInstance
            .GetReference("users")
            .OrderByChild("highScore")
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    // Handle the error...
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    foreach (var dataSnapshot in snapshot.Children)
                    {
                        data.Add(new HighScore(dataSnapshot.Child("uuid").Value.ToString(), 
                            dataSnapshot.Child("highScoreName").Value.ToString(), 
                            int.Parse(dataSnapshot.Child("highScore").Value.ToString())));
                    }

                    if(data.Count > 0)
                    {
                        data.Reverse();
                        int i = 0;
                        foreach(var hg in data)
                        {
                            highScoreTexts[i].text = hg.highScoreName + " --> " + hg.highScore;
                            i++;
                            if(i == 14)
                            {
                                break;
                            }
                        }
                    }
                }
            });
    }
}