using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    private int _levelSelection = 0;
    private int _maxLevel = 2;
    private int _minLevel = -1;
    private GameObject Player;
    

    void Awake()
    {
        Player = GameObject.FindWithTag("Player");
    }

    void OnLevelUp()
    {
        //Increase levelSelection by 1 and update positon of PlayerController
        if (_levelSelection < _maxLevel)
        {
            _levelSelection++;
            GameObject NextLevel = GameObject.Find("Level_" + _levelSelection.ToString());
            Player.transform.position = NextLevel.transform.position;
        }
    }

    void OnLevelDown()
    {
        //Decrease levelSelection by 1 and update positon of PlayerController
        if (_levelSelection > _minLevel)
        {
            _levelSelection--;
            GameObject PreviousLevel = GameObject.Find("Level_" + _levelSelection.ToString());
            Player.transform.position = PreviousLevel.transform.position;
        }
    }

    void OnInteract()
    {
        //Load selected level indicated by _levelSelection
        if (_levelSelection == -1)
        {
            SceneManager.LoadScene("CasinoScene");
        }
        else
        {
            SceneManager.LoadScene("QuizScene");
        }
    }




}
