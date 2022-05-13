using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public GameObject player;

    public float horizontalEspacement;
    public float verticalEspacement;

    public int alienHorizontalState = -10;


    public GameObject[] alienMissile;
    public int currentAlienMissile = 0;

    public GameObject[] explosionParticles;
    public int currentExplosion= 0;


    private void Awake()
    {
        if(Instance!=null)
        {
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
        alienMissile = GameObject.FindGameObjectsWithTag("alienMissile");

        for (int i = 0; i < alienMissile.Length; i++)
        {
            alienMissile[i].gameObject.SetActive(false);
        }
    }

    public void SetExplosions()
    {
        explosionParticles = GameObject.FindGameObjectsWithTag("explosion");

        for (int i=0; i<explosionParticles.Length;i++)
        {
            explosionParticles[i].gameObject.SetActive(false);
        }
    }
}
