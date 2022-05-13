using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public GameObject[] aliens;

    int alienHorizontalState = 0;
    float speed = 0.01f;
    float DEFAULT_speed = 0.01f;
    //-- vertically
    float startTop = 13.0f;
    float topPosition = 4.0f;
    float bottomPosition = -7.0f;

    //-- horizontally
    float limitationHorizontal = 8.0f;
    float totalLine = 4;
    float totalColumns = 10;


    public Transform RiserPosition;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.verticalEspacement = (topPosition - bottomPosition) / totalLine;
        GameManager.Instance.horizontalEspacement = (limitationHorizontal * 2) / totalColumns;

        BuildAllLines();
        StartCoroutine(MoveAliens());
    }

    private void BuildAllLines()
    {
        for (int i = 0; i < 4; i++)
        {
            BuildWaves(topPosition + i * GameManager.Instance.verticalEspacement, i);
        }
    }

    private void BuildWaves(float _positionY)
    {
        for (int i = 0; i < totalColumns; i++)
        {
            GameObject newAlien = Instantiate(aliens[0], RiserPosition.transform,true);

            newAlien.transform.position = 
                new Vector3((i * GameManager.Instance.horizontalEspacement) - limitationHorizontal, _positionY);
            newAlien.gameObject.SetActive(true);
        }
    }

    private void BuildWaves(float _positionY, int _indexAliensArray)
    {
        for (int i = 0; i < totalColumns; i++)
        {
            GameObject newAlien = Instantiate(aliens[_indexAliensArray], RiserPosition);

            newAlien.transform.position =
                new Vector3((i * GameManager.Instance.horizontalEspacement) - limitationHorizontal, _positionY);

            newAlien.gameObject.SetActive(true);
        }
       


    }

    private void DisableAllAliens()
    {
        for (int i=0; i<aliens.Length;i++)
        {
            aliens[i].transform.position = new Vector3(0, startTop, 0);
            aliens[i].gameObject.SetActive(false);
        }
    }

    IEnumerator MoveAliens()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);

            int temp = GameManager.Instance.alienHorizontalState;
            temp++;

            if (temp > 11) temp = 0;

            GameManager.Instance.alienHorizontalState = temp;

            speed = DEFAULT_speed;
            Debug.Log("" + GameManager.Instance.alienHorizontalState + "  :  " + temp);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
