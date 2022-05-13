using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{

    private float distanceToPlayer = 30.0f;
    private float speed = 20.0f;
    public int typeOfMissile = 0;
    // Start is called before the first frame update
      void Start()
    {
        
    }

    // Update is called once per frame
      void Update()
    {

        transform.Translate(Vector3.up * Time.deltaTime * speed);

        float distance = Vector3.Distance(GameManager.Instance.player.transform.position, transform.position);

        if (distance > distanceToPlayer) DisableObject();
    }

     private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("alien") && typeOfMissile==0)
        {
            DisableObject();
        }

        if (other.gameObject.CompareTag("player") && typeOfMissile == 1)
        {
            DisableObject();
        }
    }

    protected virtual void DisableObject()
    {
        transform.position = new Vector3(0, -20, 0);
        gameObject.SetActive(false);
    }
}
