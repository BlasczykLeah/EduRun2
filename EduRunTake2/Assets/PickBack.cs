using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickBack : MonoBehaviour
{
    public GameObject happy, sad;
    private int randNum;

    // Start is called before the first frame update
    void Start()
    {
        randNum = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
