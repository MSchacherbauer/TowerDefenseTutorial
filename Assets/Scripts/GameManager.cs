using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private bool gameEnded = false;
    // Update is called once per frame
    void Update()
    {
        if (gameEnded) return;
        if (PlayerStats.Lives <= 0 )
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("Game Over!");
        gameEnded = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
