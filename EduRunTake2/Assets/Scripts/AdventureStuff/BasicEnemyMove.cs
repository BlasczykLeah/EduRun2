using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMove : MonoBehaviour
{
    Transform target;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(gameObject.transform.position, target.transform.position, speed * Time.deltaTime);
    }
}
