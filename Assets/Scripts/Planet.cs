using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float rotationSpeed = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);// = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,4,0), rotationSpeed * Time.deltaTime);
    }
}
