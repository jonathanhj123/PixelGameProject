using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;


public class QuizManager : MonoBehaviour
{

    private VisualElement _root;
    private Button[] _button = new Button[4];
    private Label _label;
    private QuestionsAndAnswers[] QNA = new QuestionsAndAnswers[4];
    private int _rightAnswers = 0;
    private int _wrongAnswers = 0;
    private bool _hasAnswered = false;
    private int _questionAmount = 0;
    private float _timer = 2;
    private float _wait = 0;

    void Awake()
    {
        QNA[0] = new QuestionsAndAnswers("Hvem var hovedarkitekten bag Kalmarunionen?", new string[] { "Droning Margrete 1", "Dronning Margrete springhest", "Christian 1", "Valdemar Atterdag" }, 0);
        QNA[1] = new QuestionsAndAnswers("Hvilken periode eksisterede Kalmarunionen? ", new string[] { "1400-1523", "1397-1523", "1423-1597", "1288-1434" }, 1);
        QNA[2] = new QuestionsAndAnswers("Hvem var dronning Margrete 1. gift med?", new string[] { "Henry 4 af England", "Christian 1. af Danmark", "Kong Haakon 6 af Norge.", "Magnus Eriksson af Sverige" }, 2);
        QNA[3] = new QuestionsAndAnswers("Hvor længe varede rigsfællesskabet mellem Norge og Danmark?", new string[] { "1648", "1918", "1550", "1814" }, 3);

        _root = GetComponent<UIDocument>().rootVisualElement;
        _label = _root.Q<Label>("Question");
        _button[0] = _root.Q<Button>("Answer1");
        _button[1] = _root.Q<Button>("Answer2");
        _button[2] = _root.Q<Button>("Answer3");
        _button[3] = _root.Q<Button>("Answer4");
    }
    void Start()
    {

        setQuestionAndAswers(QNA[Random.Range(0, QNA.Length)]);
    }

    void Update()
    {

        if (_questionAmount == 3)
        {
            if (_wrongAnswers >= 2)
            {
                gameOver();
            }
            else
            {
                winGame();
            }
        }
        if (_wrongAnswers >= 2)
        {
            gameOver();

        }
        if (_hasAnswered)
        {

            if (_wait < _timer)
            {
                _wait = _wait + Time.deltaTime;
            }
            else
            {
                setQuestionAndAswers(QNA[Random.Range(0, QNA.Length)]);
                _hasAnswered = false;
            }
        }

    }

    public void rightAnswer()
    {
        _questionAmount++;
        _hasAnswered = true;
        _rightAnswers++;
        Debug.Log(_questionAmount);
        Debug.Log("Rigtigt svar");

        // Make visuals for the player to see if its correct or not
    }

    public void wrongAnswer()
    {
        _questionAmount++;
        _hasAnswered = true;
        _wrongAnswers++;
        Debug.Log("Forkert svar");
        Debug.Log(_questionAmount);
        // Make visuals for the player to see if its correct or not
    }

    void setQuestionAndAswers(QuestionsAndAnswers qna)
    {
        _label.text = qna._question;


        for (int i = 0; i < 4; i++)
        {
            _button[i].text = qna._answers[i];
            _button[i].clicked -= rightAnswer;
            _button[i].clicked -= wrongAnswer;

            if (i == qna.RightAnswerIndex)
            {
                _button[i].clicked += rightAnswer;

            }
            else
            {
                _button[i].clicked += wrongAnswer;
            }
        }
    }
    void gameOver()
    {
        // Game over screen, tabt.
        //Debug.Log("Game over");
    }
    void winGame()
    {
        // Vis skærm med win¨
        // Debug.Log("Win game");
    }
}
