using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button _start;
    public Button _continue;
    public Button _choosLevel;
    // Start is called before the first frame update
    void Start()
    {
        //Button ChoosLevel = GameObject.Find("ChoosLevel")
        _choosLevel.interactable = false;
        //ChoosLevel.
        if (PlayerPrefs.HasKey("GameSaved"))
        {
            if (PlayerPrefs.GetInt("GameSaved") != 1)
            {
                _continue.interactable = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnStartPress()
    {
        if (PlayerPrefs.HasKey("GameSaved"))
        {
            if (PlayerPrefs.GetInt("GameSaved") == 1)
            {
                Action action = () =>
                {
                    PlayerPrefs.DeleteAll();
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("Level1");
                };
                PopUp popUp = UIController.Instance.CreatePopUp();
                popUp.Init(UIController.Instance.MainCanvas, "Czy na pewno chcesz zacz¹æ grê od nowa?", "Nie", "Tak", action);
            }
        }
        else SceneManager.LoadScene("Level1");
    }

    public void OnContinuePress()
    {
        if (PlayerPrefs.HasKey("LevelSaved"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("LevelSaved"));
        }
    }
}
