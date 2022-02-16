using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameInput : MonoBehaviour
{
    [SerializeField] Text _objectiveText = null;
    [SerializeField] Text _scoreText = null;

    public static int _score = 0;

    public float _objTimer = 3;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(_objTimer);
        Destroy(_objectiveText);
    }
    private void Update()
    {
        _scoreText.GetComponent<Text>().text = "Score: " + _score;

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ReloadLevel();
            _score = 0; // reset score
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void ReloadLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex; // in Build settings, whatever scene is in the index
        SceneManager.LoadScene(activeSceneIndex); // load scene that is currently open
    }

}
