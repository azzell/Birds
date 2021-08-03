using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] float _health;
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
        var colObj = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collision.gameObject.GetComponent<Rigidbody2D>() == null)
            return;
        float damage;
        if(colObj.CompareTag("Crate"))
        {
             damage =( colObj.velocity.magnitude * colObj.mass );
        }
        else if(colObj.CompareTag("Ground"))
        {
            damage = (colObj.velocity.magnitude * colObj.mass);
        }
        else if (colObj.CompareTag("Player"))
        {
            damage = (colObj.velocity.magnitude * colObj.mass) * 4;
        }
        else if (colObj.CompareTag("Monster"))
        {
            damage = (colObj.velocity.magnitude * colObj.mass) * 3;

        }
        else
        {
            damage = colObj.velocity.magnitude * colObj.mass * 2;
        }
        _health -= damage;
        if (_health <= 0)
            StartCoroutine(Kaboom());

    }

    private IEnumerator Kaboom()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
