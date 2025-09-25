using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;


public class QuizManager : MonoBehaviour
{

    private VisualElement _root;
    private Button[] _button = new Button[4];
    private Label _label;
    private int _rightAnswerIndex = 3;
    private QuestionsAndAnswers[] QNA = new QuestionsAndAnswers[4];
    void Start()
    {

        QNA[0] = new QuestionsAndAnswers("Hvem var hovedarkitekten bag Kalmarunionen?", new string[] {"Droning Margrete 1", "Dronning Margrete springhest", "Christian 1", "Valdemar Atterdag"},0);
        QNA[1] = new QuestionsAndAnswers("Hvilken periode eksisterede Kalmarunionen? ", new string[] {"1400-1523", "1397-1523", "1423-1597", "1288-1434"} , 1);
        QNA[2] = new QuestionsAndAnswers("Hvem var dronning Margrete 1. gift med?",new string[] {"Henry 4 af England", "Christian 1. af Danmark", "Kong Haakon 6 af Norge.", "Magnus Eriksson af Sverige"} , 2);
        QNA[3] = new QuestionsAndAnswers("Hvor længe varede rigsfællesskabet mellem Norge og Danmark?", new string[] {"1648", "1918", "1550", "1814"} , 3);

        _root = GetComponent<UIDocument>().rootVisualElement;
        _label = _root.Q<Label>("Question");
        _button[0] = _root.Q<Button>("Answer1");
        _button[1] = _root.Q<Button>("Answer2");
        _button[2] = _root.Q<Button>("Answer3");
        _button[3] = _root.Q<Button>("Answer4");

        setQuestionAndAswers(QNA[Random.Range(0,QNA.Length)]);
    }

    public void rightAnswer()
    {
        Debug.Log("Rigtigt Svar");
    }

    public void wrongAnswer()
    {
        Debug.Log("Forkert svar");
    }

    void setQuestionAndAswers(QuestionsAndAnswers qna)
    {
        _label.text = qna._question;
        

            for (int i = 0; i < 4; i++)
        {
            
            _button[i].text = qna._answers[i];

            if (i == qna.RightAnswerIndex)
            {
                Debug.Log("Fuck af");
                _button[i].clicked += () => rightAnswer();
            }
            else
            {
                Debug.Log("Hej ven");
                _button[i].clicked += () => wrongAnswer();
            }
        }
    }
}
