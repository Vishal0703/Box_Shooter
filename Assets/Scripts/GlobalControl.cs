using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public enum Difficulty { Easy, Medium, Hard};

    public Difficulty hardness = Difficulty.Easy;

    public float level1Time = 50f;
    public int level1Beat = 100;
    public float level2Time = 30f;


    public void SetDifficulty()
    {
        switch(hardness)
        {
            case Difficulty.Easy:
                {
                    level1Time = 50f;
                    level1Beat = 80;
                    level2Time = 30f;
                    break;
                }
            case Difficulty.Medium:
                {
                    level1Time = 35f;
                    level1Beat = 100;
                    level2Time = 20f;
                    break;
                }
            case Difficulty.Hard:
                {
                    level1Time = 25f;
                    level1Beat = 100;
                    level2Time = 15f;
                    break;
                }
        }
    }

}
