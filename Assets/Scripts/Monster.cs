using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Monster : MonoBehaviour
{

    [SerializeField] Sprite _deadSprite;
    [SerializeField] ParticleSystem _partical;

    public bool _hasDied { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieOnCollision(collision))
        {
            StartCoroutine(Die());
        }
    }

    private bool ShouldDieOnCollision(Collision2D collision)
    {
        if (_hasDied)
            return false;
        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
            return true;
        if (collision.GetContact(0).normal.y < -0.5)
            return true;

        return false;
    }

    private IEnumerator Die()
    {
        GetComponent<SpriteRenderer>().sprite = _deadSprite;
        _partical.Play();
        _hasDied = true;
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
