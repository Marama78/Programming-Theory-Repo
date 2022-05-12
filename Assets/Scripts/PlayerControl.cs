using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody playerRb;
    [SerializeField]
    private float speed = 1000.0f;
    [SerializeField]
    private float horizontalLimitation = 13.0f;
    private float verticalLimitationTop = -6.0f;
    private float verticalLimitationAbove = -10.0f;
    [SerializeField]
    private Vector3 newPosition;

    [SerializeField] private GameObject[] bullets;
    [SerializeField] private int currentIterationBullet = 0;
    // Start is called before the first frame update
    void Start()
    {
        bullets = GameObject.FindGameObjectsWithTag("playerMissile");

        Debug.Log("found " + bullets.Length+ " pbjects");

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space))
            Attack();
    }

    private void Attack()
    {
        currentIterationBullet++;

        if (currentIterationBullet >= bullets.Length - 1) 
            currentIterationBullet = 0;

        bullets[currentIterationBullet].gameObject.SetActive(true);
        bullets[currentIterationBullet].transform.position = transform.position;

    }

    private void MovePlayer()
    {
        //-- get input player --
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        newPosition = transform.position;

        newPosition += Vector3.right * inputHorizontal * Time.deltaTime * speed;
        newPosition += Vector3.up * inputVertical * Time.deltaTime * speed;

        //-- Clamp the values --
        if (newPosition.x < -horizontalLimitation)
        {
            newPosition = new Vector3(-horizontalLimitation, newPosition.y);
        }
        else if (newPosition.x > horizontalLimitation)
        {
            newPosition = new Vector3(horizontalLimitation, newPosition.y);
        }


        if (newPosition.y < verticalLimitationAbove)
        {
            newPosition = new Vector3(newPosition.x, verticalLimitationAbove);
        }
        else if (newPosition.y > verticalLimitationTop)
        {
            newPosition = new Vector3(newPosition.x, verticalLimitationTop);
        }

        //-- Update new position to transform :: smooths movements --
        transform.position = Vector3.Lerp(transform.position, newPosition, 0.01f);
    }
}
