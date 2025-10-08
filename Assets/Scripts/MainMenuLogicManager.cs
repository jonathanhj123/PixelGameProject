using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuLogicManager : MonoBehaviour
{
    private GameObject _mainMenuUI;
    private VisualElement _mainMenuUIRoot;
    private Button _startBTN;

    public AudioSource _audioSource;
    [SerializeField] private AudioClip _BGMusic;
    [SerializeField] private AudioClip _buttonClick;

    void Start()
    {
        _mainMenuUI = GameObject.FindWithTag("MainMenuUI");
        _mainMenuUIRoot = _mainMenuUI.GetComponent<UIDocument>().rootVisualElement;

        _startBTN = _mainMenuUIRoot.Q<Button>("StartButton");
        _startBTN.clicked += StartGame;

        _audioSource.PlayOneShot(_BGMusic, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        _audioSource.PlayOneShot(_buttonClick, 0.5f);
        SceneManager.LoadScene("LevelSelect");   
    }
}
