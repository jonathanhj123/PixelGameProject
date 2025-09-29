using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuLogicManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private GameObject _mainMenuUI;
    private VisualElement _mainMenuUIRoot;
    private Button _startBTN;
    void Start()
    {
        _mainMenuUI = GameObject.FindWithTag("MainMenuUI");
        _mainMenuUIRoot = _mainMenuUI.GetComponent<UIDocument>().rootVisualElement;

        _startBTN = _mainMenuUIRoot.Q<Button>("StartButton");
        _startBTN.clicked += StartGame;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("QuizScene");   
    }
}
