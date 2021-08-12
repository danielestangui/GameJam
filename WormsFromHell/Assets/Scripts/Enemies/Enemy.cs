using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float _speed = 10f;
    public float _maxSpeed = 3f;
    public int _points;
    public int _level;
    public int _healthPoints;

    protected bool isAlive;
    protected Transform target;
    protected Rigidbody rb;

    void Start()
    {
        isAlive = true;
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag(Tag.Player).transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_healthPoints <= 0)
        {
            Die();
            return;
        }
        Move();
    }

    protected virtual void Move()
    {
    }

    public void TakeHit(int damage) {
        _healthPoints = _healthPoints - damage;
    }

    public virtual void Die()
    {
        AddPoints();
        DestroyImmediate(this.gameObject);
    }

    private void AddPoints() {
        if (target.tag == Tag.Player) {
            target.gameObject.GetComponent<PlayerController>().AddPoints(_points);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Tag.Player) {
            collision.gameObject.GetComponent<PlayerController>().Die();
        }
    }

}
