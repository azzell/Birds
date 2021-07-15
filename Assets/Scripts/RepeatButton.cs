using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;

public class RepeatButton : MonoBehaviour
{
    private LevelControler _levelcotroler;

    // Start is called before the first frame update
    void Start()
    {
       _levelcotroler= FindObjectOfType<LevelControler>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButtonPress()
    {
        Action action = () =>
        {
            SceneManager.LoadScene(LevelControler.Instance._thisLevelName);
        };

        Debug.Log(_levelcotroler._shotsLeft);
        Debug.Log(_levelcotroler._shotsToUse);
        PopUp popup = UIController.Instance.CreatePopUp();
        popup.Init(UIController.Instance.MainCanvas,
            "Czy na pewno chcesz zresetowaæ poziom",
            "Nie",
            "Tak",
            action);


    }
}
