using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Platformer;

public class GameManager : MonoBehaviour
{
    public int coinsCounter = 0;
    public Text coinText;
    //public PlayerController player;
    public PlayerController player;
    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        coinText.text = coinsCounter.ToString();

        if (player.deathState == true)
        {
            // Reload the scene after 3 seconds
            Invoke("ReloadLevel", 0.1f);
        }
    }

}
