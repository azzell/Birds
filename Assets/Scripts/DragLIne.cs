using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragLIne : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Bird _bird;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        //_bird = FindObjectOfType<Bird>();
        //_bird = Bird.Instance;
        if (Bird.Instance.IsActive)
        {
            _lineRenderer.SetPosition(0, new Vector3(Bird.Instance.transform.position.x, Bird.Instance.transform.position.y, -0.1f));
        }
        if (BirdGreen.Instance.IsActive)
        {
            _lineRenderer.SetPosition(0, new Vector3(BirdGreen.Instance.transform.position.x, BirdGreen.Instance.transform.position.y, -0.1f));

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Bird.Instance.IsDragging)//_bird.IsDragging
        { 
            _lineRenderer.SetPosition(1, Bird.Instance.transform.position);
            _lineRenderer.enabled = true;
        }
        else if (BirdGreen.Instance.IsDragging)
        {
            _lineRenderer.SetPosition(1, BirdGreen.Instance.transform.position);
            _lineRenderer.enabled = true;
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }
}
