using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    private int _levelSelection = 0;
    private int _maxLevel = 2;
    private int _minLevel = -1;
    private GameObject Player;

    public AudioSource _audioSource;
    [SerializeField] private AudioClip _buttonClick;


    void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        Player.transform.position = GameObject.Find("Level_0").transform.position;
    }

    public void OnLevelUp()
    {
        //Increase levelSelection by 1 and update positon of PlayerController + Play sound
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.PlayOneShot(_buttonClick, 0.5f);
        if (_levelSelection < _maxLevel)
        {
            _levelSelection++;
            GameObject NextLevel = GameObject.Find("Level_" + _levelSelection.ToString());
            Player.transform.position = NextLevel.transform.position;

        }
    }

    public void OnLevelDown()
    {
        //Decrease levelSelection by 1 and update positon of PlayerController + Play sound
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.PlayOneShot(_buttonClick, 0.5f);
        if (_levelSelection > _minLevel)
        {
            _levelSelection--;
            GameObject PreviousLevel = GameObject.Find("Level_" + _levelSelection.ToString());
            Player.transform.position = PreviousLevel.transform.position;
        }
    }

   
    public void OnInteract()
    {
        //Load selected level indicated by _levelSelection + play sound
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.PlayOneShot(_buttonClick, 0.5f);
        if (_levelSelection == -1)
        {
            SceneManager.LoadScene("CasinoScene");
        }
        else
        {
            SceneManager.LoadScene("QuizStartScene");
        }
    }




}
