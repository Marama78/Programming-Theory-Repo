using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerService : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // GameManager.Instance.totalAlien = 0;
        GameManager.Instance.SetAlienMissile();
        GameManager.Instance.SetExplosions();
        GameManager.Instance.player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("" + GameManager.Instance.totalAlien);
        if (GameManager.Instance.totalAlien <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
