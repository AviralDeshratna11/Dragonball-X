using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int Score = 0;
    
    
    // Start is called before the first frame update
    public void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberGameSession = FindObjectsOfType<GameSession>().Length;
        if (numberGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public int GetScore() { return Score; }
    
    public void AddToScore(int scoreValue)
    {
        Score += scoreValue;
    }
    
    public void ResetGame()
    {
        Destroy(gameObject);
    }

    
}
