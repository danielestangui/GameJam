using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float speed;
    public float _lifetime;
    public int _damage = 1;
    void Start()
    {
        Destroy(gameObject, _lifetime);
    }


    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        transform.Translate(Vector3.right * moveDistance);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {

            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<Enemy>().TakeHit(_damage);
                Destroy(this.gameObject);
            }

            if (other.gameObject.tag == "Platform")
            {
                Destroy(this.gameObject);
            }
        }
    }
}
