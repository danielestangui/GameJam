using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Collider col;
    private Transform target;

    private float offset;

    void Start()
    {
        col = GetComponent<Collider>();
        target = GameObject.FindGameObjectWithTag(Tag.Player).transform;
        offset = (GetPlayerHight()+ col.bounds.size.y)/2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = target.position.y - (transform.position.y + offset);
        if (dist > 0)
        {
            gameObject.layer = 10;
        }

        if (dist < -1f) {
            //Platform Layer
            gameObject.layer = 12;
        }
    }

    private float GetPlayerHight() {
        return target.gameObject.GetComponent<Collider>().bounds.size.y;
    }
}
