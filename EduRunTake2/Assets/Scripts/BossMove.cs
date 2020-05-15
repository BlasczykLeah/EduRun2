using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [Header("Boss Things")]
    public Transform[] myPoints;    // 0 = starting point, 1 = player, else other random points
    public float waitToMove;
    int passiveCounter;     // count of points chosen that arent the player
    public int maxPoints;   // max points boss will use before forced to player
    public float speed = 1;

    Transform activePoint;
    bool attacking = false;
    bool moving = false;

    [Header("Quiz Things")]
    public GameObject questionBox;

    void Start()
    {
        Invoke("PickNewPoint", waitToMove);
    }

    void Update()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, activePoint.position, Time.deltaTime * speed);

            float value = Mathf.Abs(transform.position.x - activePoint.position.x) + Mathf.Abs(transform.position.y - activePoint.position.y);
            if (value < 0.5F)
            {
                moving = false;
                activePoint = transform;

                if (attacking)
                {
                    Time.timeScale = 0;
                    questionBox.SetActive(true);
                }
                else
                {
                    Invoke("PickNewPoint", waitToMove);
                }
            }
        }
    }

    void PickNewPoint()
    {
        moving = true;

        if(passiveCounter >= maxPoints)
        {
            //go to the player
            activePoint = myPoints[1];
            attacking = true;
        }
        else if(passiveCounter == 0)
        {
            //dont go to player
            int rand = Random.Range(2, myPoints.Length);
            activePoint = myPoints[rand];
            passiveCounter++;
        }
        else
        {
            //go anywhere
            int rand = Random.Range(0, myPoints.Length);
            activePoint = myPoints[rand];
            if (rand == 1) attacking = true;
            else passiveCounter++;
        }
    }

    public void moveToStart()
    {
        moving = true;
        attacking = false;
        passiveCounter = 0;
        activePoint = myPoints[0];
    }
}
