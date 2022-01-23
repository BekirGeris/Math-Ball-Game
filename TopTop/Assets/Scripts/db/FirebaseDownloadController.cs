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

        FirebaseDatabase.DefaultInstance.GetReference("users").ValueChanged += HandleChildChanged;

    }

    private void HandleChildChanged(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }
        loadData();
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
                            double.Parse(dataSnapshot.Child("highScore").Value.ToString())));
                    }

                    if(data.Count > 0)
                    {
                        data.Reverse();
                        int i = 0;
                        foreach(var hg in data)
                        {
                            highScoreTexts[i].text = hg.highScoreName + " -- " + hg.highScore;
                            i++;
                            if(i == highScoreTexts.Count)
                            {
                                break;
                            }
                        }
                    }
                }
            });
    }
}
