using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControler : MonoBehaviour
{
    [SerializeField] private string _nextLevelName;
    [SerializeField] public int _shotsToUse;
    [SerializeField] public string _thisLevelName;
    [SerializeField] public string _levelId;
    [SerializeField] public int _birdChange;

    private Monster[] _monsters;
    public int _shotsLeft;
    private bool _notReseted = true;
    public Vector2 _birdStartPosition;
    public static LevelControler Instance;

    // Start is called before the first frame update
    private void Start()
    {
        if(Instance != null)
        {
            GameObject.Destroy(this.gameObject);
            return;
        }

        Instance = this;
        _shotsLeft = _shotsToUse;
        _birdStartPosition = Bird.Instance._startPosition;
    }
    private void OnEnable()
    {

        _monsters = FindObjectsOfType<Monster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_shotsLeft == _birdChange)
        {
            Bird.Instance.Activation(false);
            BirdGreen.Instance.Activation(true);
        }
        if (MonstersAreAllDead())
        {
            GoToNextLevel();
            PlayerPrefs.SetInt("GameSaved", 1);
            PlayerPrefs.SetInt("LevelId", Int32.Parse(_levelId));
            PlayerPrefs.SetString("LevelSaved", _nextLevelName);
            PlayerPrefs.Save();

        }      
        
    }
    private void LateUpdate()
    {
        if(ShotsLeft())
        {
            Bird.Instance.gameObject.SetActive(false);
            BirdGreen.Instance.Activation(false);
            ResetLevel();

        }
       
        
    }

    private void ResetLevel()
    {
        _notReseted = false;
        Action act1 = () =>
        {
            SceneManager.LoadScene(_thisLevelName);

        };
        Action act2 = () => 
        {
            SceneManager.LoadScene(_nextLevelName);
        };
        PopUp popUp = UIController.Instance.CreatePopUp();
        popUp.Init(UIController.Instance.MainCanvas,
            "Niestety przegra³eœ.\nCzy chcesz powtórzyæ?",
            "Tak!",
            "nie :(",
            act1,
            act2
            );
    }

    private bool ShotsLeft()
    {
        if (_shotsLeft <= 0 && _notReseted)
        {
            return true;
        }
        return false;
    }

    private void GoToNextLevel()
    {
        Debug.Log("Go to next level" + _nextLevelName);
        SceneManager.LoadScene(_nextLevelName);
    }

    private bool MonstersAreAllDead()
    {
        foreach (var monster in _monsters)
        {
            if (monster.gameObject.activeSelf)
                return false;
        }
        return true;
    }
}
