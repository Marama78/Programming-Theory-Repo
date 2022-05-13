using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public GameObject player;

    public float horizontalEspacement;
    public float verticalEspacement;

    public int alienHorizontalState = -10;

    public string playerName = string.Empty;

    public GameObject[] alienMissile;
    public int currentAlienMissile = 0;

    public GameObject[] explosionParticles;
    public int currentExplosion= 0;

    public int totalAlien = 0;
    private void Awake()
    {
        

        if (Instance!=null)
        {
            Debug.Log("Welcome " + playerName);
            GetDefaultValues();
            Destroy(gameObject);
        }

        Instance = this;
        player = GameObject.Find("Player");

        SetAlienMissile();
        SetExplosions();

        DontDestroyOnLoad(Instance);
    }

    private void GetDefaultValues()
    {
        GameManager.Instance.player = GameObject.Find("Player");
        GameManager.Instance.SetAlienMissile();
        GameManager.Instance.SetExplosions();
    }

    public void SetAlienMissile()
    {
        GameManager.Instance.alienMissile = GameObject.FindGameObjectsWithTag("alienMissile");

        for (int i = 0; i < alienMissile.Length; i++)
        {
            GameManager.Instance.alienMissile[i].gameObject.SetActive(false);
        }
    }

    public void SetExplosions()
    {
        GameManager.Instance.explosionParticles = GameObject.FindGameObjectsWithTag("explosion");

        for (int i=0; i<explosionParticles.Length;i++)
        {
            GameManager.Instance.explosionParticles[i].gameObject.SetActive(false);
        }
    }

    public void StartNewGame()
    {
        GameObject input = GameObject.FindGameObjectWithTag("inputPlayerName");

        string name = input.GetComponent<TextMeshProUGUI>().text;

        if (name.Length > 0)
        {
            for (int i = 0; i < name.Length; i++)
            {
                if (i == 0)
                    playerName += name[i].ToString().ToUpper();
                else
                    playerName += name[i];
            }

            SceneManager.LoadScene(1);
        }
    }
}
