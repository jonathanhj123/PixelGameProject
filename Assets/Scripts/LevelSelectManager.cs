using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    private int _levelSelection = 0;
    private int _maxLevel = 2;
    private int _minLevel = -1;
    private GameObject Player;
    private GameObject SelectedLevel;
    private bool isMoving = false;


    void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        Player.transform.position = GameObject.Find("Level_0").transform.position;
    }

    public void OnLevelUp()
    {
        //Increase levelSelection by 1 and update positon of PlayerController
        if (_levelSelection < _maxLevel)
        {
            _levelSelection++;
            isMoving = true;
        }
    }

    public void OnLevelDown()
    {
        //Decrease levelSelection by 1 and update positon of PlayerController
        if (_levelSelection > _minLevel)
        {
            _levelSelection--;
            isMoving = true;
        }
    }

    void Update()
    {
        while (isMoving)
        {
            SelectedLevel = GameObject.Find("Level_" + _levelSelection.ToString());
            Player.transform.position = Vector2.Lerp(Player.transform.position, SelectedLevel.transform.position, 0.01f);

            if (Player.transform.position == SelectedLevel.transform.position)
            {
                isMoving = false;
            }
        }

    }
    public void OnInteract()
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
