
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    Vector2 Position;
    bool gehen = false;
    // Start is called before the first frame update
    void Start()
    {
        geheZuPosition(new Vector2(transform.position.x, transform.position.y));
    }

    // Update is called once per frame
    void Update()
    {
        if (gehen == true)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), Position, 0.95f * Time.deltaTime);
            if(new Vector2(transform.position.x , transform.position.y)== Position)
            {
                gehen = false;
            }
        }
    }

    private void geheZuPosition(Vector2 position)
    {
        System.Random rnd = new System.Random();
        Position = new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f);
        gehen = true;
        
    }
    

}
