using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMove : MonoBehaviour
{
    Transform target;
    GameObject me;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        me = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (me == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector2.MoveTowards(gameObject.transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }
}
