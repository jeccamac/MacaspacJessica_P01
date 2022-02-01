using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInput : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ReloadLevel();
        }
    }

    void ReloadLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex; // in Build settings, whatever scene is in the index
        SceneManager.LoadScene(activeSceneIndex); // load scene that is currently open
    }
}
