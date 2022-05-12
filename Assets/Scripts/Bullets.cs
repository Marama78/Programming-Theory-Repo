using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{

    public float distanceToPlayer = 20.0f;
    public float speed = 200.0f;
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
        if(other.gameObject.CompareTag("alien"))
        {
            DisableObject();
        }
    }

    private void DisableObject()
    {
        transform.position = new Vector3(0, -20, 0);
        gameObject.SetActive(false);
    }
}