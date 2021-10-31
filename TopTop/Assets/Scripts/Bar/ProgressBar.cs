using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ProgressBar : FillBar
{
    private UnityEvent onProgressComplete;

    // Create a property to handle the slider's value
    public new float CurrentValue
    {
        get
        {
            return base.CurrentValue;
        }
        set
        {
            // If the value exceeds the max fill, invoke the completion function
            if (value >= slider.maxValue)
                onProgressComplete.Invoke();

            // Remove any overfill (i.e. 105% fill -> 5% fill)
            base.CurrentValue = value % slider.maxValue;
        }
    }
    void Start()
    {
        // Initialize onProgressComplete and set a basic callback
        if (onProgressComplete == null)
            onProgressComplete = new UnityEvent();
        onProgressComplete.AddListener(OnProgressComplete);
    }

    void Update()
    {
        CurrentValue += 0.003f;
    }

    // The method to call when the progress bar fills up
    void OnProgressComplete()
    {
        SceneManager.LoadScene("EndGame");
    }

    public void Clear()
    {
        CurrentValue = 0;
    }
}
