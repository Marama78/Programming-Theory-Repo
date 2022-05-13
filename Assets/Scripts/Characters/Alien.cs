using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{

    Vector3 vStartPosition;
    Vector3 targetMidle;
  

    Vector3 newposition;
    bool canMove = false;
    float speed = 0.01f;
    float DEFAULT_speed = 0.01f;
    float espacementH;
    int oldMovementState = -100;

    int currentExplosionParticles = 0;

    int killObject = 0;

    public int alienState = 0;
    private int coroutineState = 0;

    private int attackRate = 0;
    private int lockerState = 0;
    // Start is called before the first frame update
    void Start()
    {
        attackRate = Random.Range(1, 3);

        newposition = transform.position;
        espacementH = GameManager.Instance.horizontalEspacement;

        StartCoroutine(EditTargets());
    }

   
    public bool GetCanMove()
    {
        return canMove;
    }
    // Update is called once per frame
    void Update()
    {
        if (canMove && killObject==0 && alienState == 0)
        {
            if (oldMovementState != GameManager.Instance.alienHorizontalState)
            {
                speed = DEFAULT_speed;
                oldMovementState = GameManager.Instance.alienHorizontalState;

            }

            newposition = GetNewPosition(GameManager.Instance.alienHorizontalState);

            this.transform.position = Vector3.Lerp(transform.position, newposition, speed);
        }

        ReadBehaviour();

        if (killObject==1)
        {
            killObject = 2;
            GameManager.Instance.currentExplosion++;

            if (GameManager.Instance.currentExplosion > GameManager.Instance.explosionParticles.Length - 1)
            {
                GameManager.Instance.currentExplosion = 0;
                currentExplosionParticles = 0;
            }
            else
            {
                currentExplosionParticles = GameManager.Instance.currentExplosion;

            }

            GameManager.Instance.explosionParticles[GameManager.Instance.currentExplosion].gameObject.transform.position = transform.position;
            GameManager.Instance.explosionParticles[GameManager.Instance.currentExplosion].gameObject.SetActive(true);
            StartCoroutine(AutoDestroyExplosion());
            transform.GetChild(0).gameObject.SetActive(false);
        }
  
    }

    private void ReadBehaviour()
    {

        if (alienState == 1)
        {
            ReadyToEagle();
            if (coroutineState == 0)
            {
                coroutineState = 1;
                StartCoroutine(UpdateAlienState());
            }
        }
        if (alienState == 2)
        {
            PatternEagleAttack();
            if (coroutineState == 0)
            {
                coroutineState = 1;
                StartCoroutine(UpdateAlienState());
            }
        }
        if (alienState == 3)
        {
            RestorePosition();
            if (coroutineState == 0)
            {
                coroutineState = 1;
                StartCoroutine(UpdateAlienState());
            }
        }
        if (alienState == 4)
        {
            if (coroutineState == 0)
            {
                coroutineState = 1;
                StartCoroutine(UpdateAlienState());
            }
        }
        if (alienState == 5)
        {
            RestoreEagle();
            if (coroutineState == 0)
            {
                coroutineState = 1;
                StartCoroutine(UpdateAlienState());
            }
        }


    }
   
    private Vector3 GetNewPosition(int _state)
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
                    lockerState = 0;
                break;
            case 8:
                iteration = 3;
                if (lockerState == 0)
                {
                    targetMidle += new Vector3(0, -GameManager.Instance.verticalEspacement, 0);
                    lockerState = 1;
                }
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
            default:
                Vector3 target = vStartPosition;

                //-- correct the new position --
                targetMidle = vStartPosition;

                if (transform.position.x < vStartPosition.x) speed = 0;
                if (transform.position.y < vStartPosition.y) speed = 0;


                return target;
               
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


    public void SetStartPosition(Vector3 _startPosition)
    {
        vStartPosition = _startPosition;
    }


    private void ReadyToEagle()
    {
        float speedEagle = 0.1f;
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, -2), speedEagle);
    }

    private void PatternEagleAttack()
    {
        float speedEagle = 0.1f;
        float speedForward = 10.0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 180), speedEagle);

        transform.Translate(Vector3.up * speedForward * Time.deltaTime);
    }

    private void RestorePosition()
    {
        float speedEagle = 0.1f;
        transform.position = Vector3.Lerp(transform.position, new Vector3(newposition.x, newposition.y, -2), speedEagle);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), speedEagle);
    }

    private void RestoreEagle()
    {
        float speedEagle = 0.1f;
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, 0), speedEagle);
       // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), speedEagle);
    }

    IEnumerator UpdateAlienState()
    {
        yield return new WaitForSeconds(1.5f);
        coroutineState = 0;
        if (alienState != 0)
        {
            alienState++;
            if (alienState > 5) alienState = 0;
        
        }
    }

    IEnumerator EditTargets()
    {
        //-- We must wait two seconds before the game starts --

        yield return new WaitForSeconds(2.0f);

        Vector3 temp = transform.position;
        targetMidle = temp;
     ///   Debug.Log(targetMidle);
        this.transform.position = targetMidle;
        canMove = true;

    }

    public void MakePattern()
    {
        Debug.Log("pattern is good");
        if (canMove && alienState == 0)
        {
            alienState = 1;
        Debug.Log("pattern starts");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("playerMissile"))
        {
            killObject = 1;
            GetComponent<BoxCollider>().enabled = false;
        }
    }


    IEnumerator AutoDestroyExplosion()
    {
        yield return new WaitForSeconds(1.0f);
    ///    Debug.Log("killed");
        GameManager.Instance.explosionParticles[currentExplosionParticles].gameObject.SetActive(false);
        Destroy(gameObject);
        GameManager.Instance.totalAlien--;

    }

}
