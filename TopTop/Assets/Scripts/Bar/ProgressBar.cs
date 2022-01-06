using System.Collections;
using System.Collections.Generic;
using TopTop.GameData;
using Random = System.Random;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ProgressBar : FillBar
{
    private UnityEvent onProgressComplete;

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject adsPanel;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private Data gameData;

    Random random = new Random();

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
        gameData.Count = 0;
        gameData.TargetCount = random.Next(10, 50);
        menuPanel.SetActive(false);
        adsPanel.SetActive(false);
        endPanel.SetActive(true);
    }

    public void Clear()
    {
        CurrentValue = 0;
        base.slider.value = 0;
    }
}
