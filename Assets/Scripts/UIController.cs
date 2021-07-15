using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Transform MainCanvas;
    public GameObject _go;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance != null)
        {
            GameObject.Destroy(this.gameObject);
            return;
        }
        Instance = this;
        _go = Resources.Load("UI/Popup") as GameObject;
    }

    public PopUp CreatePopUp()
    {
   
        GameObject popupgo = Instantiate(_go, MainCanvas );
        return popupgo.GetComponent<PopUp>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
