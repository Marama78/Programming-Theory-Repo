using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public GameObject player;

    public float horizontalEspacement;
    public float verticalEspacement;

    public int alienHorizontalState = 0;
    private void Awake()
    {
        if(Instance!=null)
        {
            GetDefaultValues();
            Destroy(gameObject);
        }

        player = GameObject.Find("Player");

        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    private void GetDefaultValues()
    {
        GameManager.Instance.player = GameObject.Find("Player");
    }
}
