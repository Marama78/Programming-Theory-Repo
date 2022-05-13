using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStars : MonoBehaviour
{
    public float speed = 2.5f;
    public float bottomLine = -59;
    public float topLine = -59;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y<-59)
        {
            transform.position = new Vector3(transform.position.x, topLine);
        }

        transform.Translate(Vector3.up * speed * Time.deltaTime);    
    }
}
