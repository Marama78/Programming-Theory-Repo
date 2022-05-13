using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipBehaviour : MonoBehaviour
{

    int state = 0;
    float delay = 2.0f;
    float speedSlerp = 0.005f;
    // Start is called before the first frame update
    void Start()
    {
            delay = Random.Range(3.0f, 6.0f);
        StartCoroutine(AutoMove());
    }

    // Update is called once per frame
    void Update()
    {

        ReadState();

      
    }

    private void ReadState()
    {
        switch(state)
        {
            case 0: 
            MoveVehicule(-4.6f,-3.2f,-55.5f);
                break;

            case 1:
                MoveVehicule(28.0f,23.0f,-50.0f);
                break;

            case 2:
                MoveVehicule(41.5f,142.7f,41.0f);
                break;

            case 30:
                MoveVehicule(55.0f,79.0f,-8.6f);
                break;

            default:
                MoveVehicule(28.0f, 23.0f, -50.0f);
                break;

        }
    }
    private void MoveVehicule(float _angleX, float _angleY, float _angleZ)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(_angleX, _angleY, _angleZ), speedSlerp);

    }

    IEnumerator AutoMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            state = Random.Range(0, 4);
            delay = Random.Range(3.0f, 6.0f);
        }
    }
}
