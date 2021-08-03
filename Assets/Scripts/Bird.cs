using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;
public class Bird : MonoBehaviour
{
    [SerializeField] float _lunchForce = 800;
    [SerializeField] float _maxDragDistance = 5;
    [SerializeField] public bool IsActive;

    public Vector2 _startPosition;
    Rigidbody2D _rigidbody2D;
    SpriteRenderer _spriteRenderer;
    GameObject cam1;
    GameObject cam2;
    Vector3 _cameraStartPosition ;
    bool _collided = false;
    public bool IsDragging { get; private set; }
    public static Bird Instance;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _cameraStartPosition = Camera.main.transform.position;
        cam1 =  GameObject.Find("CM vcam1");
        cam2 =  GameObject.Find("CM vcam2");
        if (Instance != null && Instance != this)
        {
            GameObject.Destroy(this.gameObject);
            return;
        }
        Instance = this;
        _startPosition = _rigidbody2D.position;
        if (IsActive)
        { 
            IsActive = true; 
        }


    }


    // Start is called before the first frame update
    void Start()
    {
        if(LevelControler.Instance._startBird != LevelControler.BirdColors.Red)
        {
            _rigidbody2D.position = LevelControler.Instance._birdStartPosition;
            _startPosition = _rigidbody2D.position;
        }
        _rigidbody2D.isKinematic = true;
        
        
    }

    private void OnMouseDown()
    {
        //if (!EventSystem.current.IsPointerOverGameObject())
        {
            _spriteRenderer.color = Color.red;
            IsDragging = true;
        }
    }

    private void OnMouseUp()
    {
        //if (!EventSystem.current.IsPointerOverGameObject())
        {
            var currentPosition = _rigidbody2D.position;
            var direction = _startPosition - currentPosition;
            direction.Normalize();
            _rigidbody2D.isKinematic = false;
            _rigidbody2D.AddForce(direction * _lunchForce);

            _spriteRenderer.color = Color.white;
            IsDragging = false;
            var levelControler = FindObjectOfType<LevelControler>();
            _collided = false;
            cam2.GetComponent<CinemachineVirtualCamera>().Priority = 20;
        }
        //levelControler._shotsLeft = levelControler._shotsLeft - 1;
    }
    private void OnMouseDrag()
    {
        //if (!EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 desiredPosition = mousePosition;

            if (desiredPosition.x > _startPosition.x)
                desiredPosition.x = _startPosition.x;
            GameObject Ground = GameObject.FindGameObjectWithTag("Ground");
            if (desiredPosition.y <= Ground.transform.position.y + 1)
            {
                desiredPosition.y = _startPosition.y - (_startPosition.y - (Ground.transform.position.y + 1.5f));
            }
            //Debug.Log(desiredPosition.y);
            float distance = Vector2.Distance(_startPosition, desiredPosition);
            if (distance > _maxDragDistance)
            {
                Vector2 direction = desiredPosition - _startPosition;
                direction.Normalize();
                desiredPosition = _startPosition + (direction * _maxDragDistance);
            }
            _rigidbody2D.position = desiredPosition;
        }
        //transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        //if ((_rigidbody2D.isKinematic != true)/*&&(_rigidbody2D.position.x > _startPosition.x+5)*/)
        //{
        //    Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + (_rigidbody2D.position.x - _startPosition.x), Camera.main.transform.position.y, Camera.main.transform.position.z);
        //}
        //if (_rigidbody2D.isKinematic == true)
        //{
        //    Camera.main.transform.position = _cameraStartPosition;
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfter(3));
        StartCoroutine(ShotCounter());
    }
    
    private IEnumerator ResetAfter(float time)
    {
        yield return new WaitForSeconds(time);
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.position = _startPosition;
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.rotation = 0;
        _rigidbody2D.freezeRotation = true;
        _rigidbody2D.freezeRotation = false;
        
        cam2.GetComponent<CinemachineVirtualCamera>().Priority = 9;




    }
    private IEnumerator ShotCounter()
    {
        yield return new WaitForSeconds(3.35f);
        if (!_collided)
        {
            LevelControler.Instance._shotsLeft -= 1;
            _collided = true;
        }
    }
    public void Activation(bool activator)
    {
        this.gameObject.SetActive(activator);
        IsActive = activator;
        if (activator)
        {
            cam2.GetComponent<CinemachineVirtualCamera>().m_Follow = this.gameObject.transform;
        }
    }
   
}
