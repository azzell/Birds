using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField] float _explosionRadius;
    [SerializeField] float _damage;
    public GameObject ExplosionEffect;

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
        if (collision.relativeVelocity.magnitude > 2)
        {
            Explode();
        }
    }

    private void Explode()
    {
       var coliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);
        foreach (var item in coliders)
        {
            Vector2 direction = item.transform.position - transform.position;
            var closestPoint = item.ClosestPoint(transform.position);
            var distance = Vector2.Distance(closestPoint, transform.position);
            var damagePercent = Mathf.InverseLerp(_explosionRadius, 0, distance);
            item.GetComponent<Rigidbody2D>().AddForce(direction * (_damage * damagePercent));
            Monster monster = item.gameObject.GetComponent<Monster>();
            if (monster != null)
            {
                monster.ExplosionDeath();
            }
        }
        GameObject ExplosionEff = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Destroy(ExplosionEff, 10);
        Destroy(this.gameObject);
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
