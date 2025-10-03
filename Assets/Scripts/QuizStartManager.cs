using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class QuizStartManager : MonoBehaviour
{
    private UIDocument _quizUI;
    private VisualElement _quizRoot;
    private Button _btn;

    void Awake()
    {
        _quizRoot = GameObject.FindWithTag("UIDoc").GetComponent<UIDocument>().rootVisualElement;
        _btn = _quizRoot.Q<Button>("OkButton");

        _btn.clicked += QuizScene;
    }

    public void QuizScene()
    {
        SceneManager.LoadScene("QuizScene");
    }

}
