using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    float limitationHorizontal = 1.0f;

   /* Vector3 targetLeft;
    Vector3 targetRight;*/
    Vector3 targetMidle;
  /*  Vector3 targetDown1;
    Vector3 targetDown2;
    Vector3 targetDown3;
    Vector3 targetDown4;*/

    Vector3 newposition;
    bool canMove = false;
    float speed = 0.01f;
    float DEFAULT_speed = 0.01f;
    float espacementH;
    int oldMovementState = -100;
    // Start is called before the first frame update
    void Start()
    {
        newposition = transform.position;
        espacementH = GameManager.Instance.horizontalEspacement;

        StartCoroutine(EditTargets());
        //StartCoroutine(MoveHorizontally());
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (oldMovementState != GameManager.Instance.alienHorizontalState)
            {
                speed = DEFAULT_speed;
                oldMovementState = GameManager.Instance.alienHorizontalState;

            }

            newposition = MoveAlien(GameManager.Instance.alienHorizontalState);

            this.transform.position = Vector3.Lerp(transform.position, newposition, speed);
        }
    }

    
    private Vector3 MoveAlien(int _state)
    {
        int iteration = 0;
        int pingpongState = 0;
        Vector3 result = Vector3.zero;
        switch(_state)
        {
            case 0:
                iteration = -1;
                break;
            case 1:
                iteration = -2;
                break;
            case 2:
                iteration = -3;
                break;
            case 3:
                iteration = -2;
                pingpongState = 1;
                break;
            case 4:
                iteration = -1;
                pingpongState = 1;
                break;
            case 5:
                return targetMidle;
            case 6:
                iteration = 1;
                break;
            case 7:
                iteration = 2;
                break;
            case 8:
                iteration = 3;
                break;
            case 9:
                iteration = 2;
                pingpongState = 1;
                break;
            case 10:
                iteration = 1;
                pingpongState = 1;
                break;
            case 11:
                return targetMidle;

        }

        float newPosX = targetMidle.x + espacementH * iteration;
        result = new Vector3(newPosX, transform.position.y, 0);

        if (transform.position.x < newPosX && iteration<0 && pingpongState==0)
        {
            result = new Vector3(newPosX, transform.position.y, 0);
            speed = 0;
        }
        else if(transform.position.x > newPosX && iteration < 0 && pingpongState == 1)
        {
            result = new Vector3(newPosX, transform.position.y, 0);
            speed = 0;
        }
        else if (transform.position.x > newPosX && iteration > 0 && pingpongState == 0)
        {
            result = new Vector3(newPosX, transform.position.y, 0);
            speed = 0;
        }
        else if (transform.position.x < newPosX && iteration > 0 && pingpongState == 1)
        {
            result = new Vector3(newPosX, transform.position.y, 0);
            speed = 0;
        }

        return result;
    }


   /* IEnumerator MoveHorizontally()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);

            int temp = HorizontalState;
            temp++;

            if (temp > 11) temp = 0;
            HorizontalState = temp;
            speed = DEFAULT_speed;
         //   Debug.Log("hori : " + HorizontalState);
        }
    }*/

    IEnumerator EditTargets()
    {
        yield return new WaitForSeconds(2.0f);

        Vector3 temp = transform.position;

        //targetLeft = temp;
       // targetRight = temp;
        targetMidle = temp;
        Debug.Log(targetMidle);

       /* targetDown1 = temp;
        targetDown2 = temp;
        targetDown3 = temp;
        targetDown4 = temp;


        targetLeft = new Vector3(targetMidle.x - GameManager.Instance.horizontalEspacement, temp.y, 0);
        
        targetRight = new Vector3(targetMidle.x + GameManager.Instance.horizontalEspacement, temp.y, 0);

        /*targetDown1.position = new Vector3(targetMidle.position.x, targetMidle.position.y + (GameManager.Instance.verticalEspacement), 0);
        targetDown2.position = new Vector3(targetMidle.position.x, targetMidle.position.y + 2 * (GameManager.Instance.verticalEspacement), 0);
        targetDown3.position = new Vector3(targetMidle.position.x, targetMidle.position.y + 3 * (GameManager.Instance.verticalEspacement), 0);
        targetDown4.position = new Vector3(targetMidle.position.x, targetMidle.position.y + 4 * (GameManager.Instance.verticalEspacement), 0);

        */
        this.transform.position = targetMidle;
       // this.transform.localScale = targetMidle.localScale;
        canMove = true;

      ///  Debug.Log("targetLeft.x " + targetLeft.position.x);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("playerMissile"))
        {
            Destroy(gameObject);
        }
    }
}
