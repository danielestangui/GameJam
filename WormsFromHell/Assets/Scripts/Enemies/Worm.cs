using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : Enemy
{

    [Header("Death Events")]
    public GameObject _deathEffect;
    public GameObject[] _worms;


    protected override void Move()
    {
        
        float dir = 1f;

        if (target.position.x < transform.position.x)
        {
            dir = -1f;
        }

        rb.AddForce(dir * Vector3.right * _speed);

        float limitedSpeed = Mathf.Clamp(rb.velocity.x, -_maxSpeed, _maxSpeed);
        rb.velocity = new Vector3(limitedSpeed, rb.velocity.y, 0);

        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();

        if (dir < 0)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }

    public override void Die()
    {

        if (_worms.Length > 0)
        {
            foreach (GameObject w in _worms)
            {
                Vector3 startPosition = new Vector3(transform.position.x + ((float)Random.Range(-100,100)/100), transform.position.y + ((float)Random.Range(0, 50) / 100));
                Instantiate(w,startPosition, transform.rotation);
            }
        }
        else {
            Vector3 direction = new Vector3(transform.position.x - target.position.x, transform.position.y - target.position.y, 0);
            Destroy(Instantiate(_deathEffect, transform.position, Quaternion.FromToRotation(Vector3.forward,direction)) as GameObject, 2f);
        }

        base.Die();
    }

}
