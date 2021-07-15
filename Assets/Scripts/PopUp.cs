using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PopUp : MonoBehaviour
{
    [SerializeField] Button _button1;
    [SerializeField] Button _button2;
    [SerializeField] Text _popupText;
    [SerializeField] Text _btn1Text;
    [SerializeField] Text _btn2Text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Transform canvas, string popupMessage, string btn1txt, string btn2txt, Action action)
    {
        _popupText.text = popupMessage;
        _btn1Text.text = btn1txt;
        _btn2Text.text = btn2txt;

        transform.SetParent(canvas);
        transform.localScale = Vector3.one;

        _button1.onClick.AddListener(() =>
        {
            //gameObject.SetActive(false);
            GameObject.Destroy(this.gameObject);
        });
        _button2.onClick.AddListener(() =>
        {
            action();

        });
    }

    public void Init(Transform canvas, string popupMessage, string btn1txt, string btn2txt, Action action1, Action action2)
    {
        _popupText.text = popupMessage;
        _btn1Text.text = btn1txt;
        _btn2Text.text = btn2txt;

        transform.SetParent(canvas);
        transform.localScale = Vector3.one;

        _button1.onClick.AddListener(() =>
        {
            action1();
            GameObject.Destroy(this.gameObject);
        });
        _button2.onClick.AddListener(() =>
        {
            action2();
            GameObject.Destroy(this.gameObject);

        });
    }

}
