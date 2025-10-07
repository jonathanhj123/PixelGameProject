using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using NUnit.Framework.Interfaces;
using Unity.Mathematics;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class QuizManager : MonoBehaviour
{
    private VisualElement _quizRoot;
    private VisualElement _gameOverRoot;
    private VisualElement _winRoot;
    private UIDocument _winUI;
    private UIDocument _gameOverUI;
    private UIDocument _quizUI;
    private Button[] _button = new Button[4];
    private Label _label;
    private List<QuestionsAndAnswers> _QNA = new List<QuestionsAndAnswers>();
    private int _rightAnswers = 0;
    private int _wrongAnswers = 0;
    private bool _hasAnswered = false;
    private int _questionAmount = 0;
    [SerializeField] private float _timer = 2;
    private float _wait = 0;
    private GameObject _quiz;
    private GameObject _gameOver;
    private GameObject _win;
    private Button _btn;
    private int _randomIndex;
    private Label _score;

    void Awake()
    {
        // Linker alle de forskellige objekter sammen så vi kan hente dem og bruge dem i dette script.
        _quiz = GameObject.FindWithTag("UIDoc");
        _gameOver = GameObject.FindWithTag("GameOver");
        _win = GameObject.FindWithTag("WinTag");


        // Tilføjer alle spørgsmål i vores QNA liste som er en liste af objektet QuestionsAndAnswers som ligge i det andet script

        // Kalmarunionen
        _QNA.Add(new QuestionsAndAnswers("Hvem var hovedarkitekten bag Kalmarunionen?", new string[] { "Droning Margrete 1", "Dronning Margrete springhest", "Christian 1", "Valdemar Atterdag" }, 0));
        _QNA.Add(new QuestionsAndAnswers("Hvilken periode eksisterede Kalmarunionen? ", new string[] { "1400-1523", "1397-1523", "1423-1597", "1288-1434" }, 1));
        _QNA.Add(new QuestionsAndAnswers("Hvem var dronning Margrete 1. gift med?", new string[] { "Henry 4 af England", "Christian 1. af Danmark", "Kong Haakon 6 af Norge.", "Magnus Eriksson af Sverige" }, 2));
        _QNA.Add(new QuestionsAndAnswers("Hvor længe varede rigsfællesskabet mellem Norge og Danmark?", new string[] { "1648", "1918", "1550", "1814" }, 3));
        // Christian 4.
        _QNA.Add(new QuestionsAndAnswers("Hvilken styreform havde Danmark under Christian 4.?", new string[] { "Enevælde", "Demokrati", "Adelsvælde", "Teokrati" }, 2));
        _QNA.Add(new QuestionsAndAnswers("Hvilken krig deltog Christian 4. i 1625?", new string[] { "Svenskekrigene", "Torstenssonfejden", "3 års krigen", "Trediveårskrigen" }, 3));
        _QNA.Add(new QuestionsAndAnswers("Hvad mistede Christian 4. under Torstenssonfejden (1643-45)?", new string[] { "Sin højre arm", "Synet på sit højre øje", "Sit venstre ben", "Sin venstre hånd" }, 1));
        _QNA.Add(new QuestionsAndAnswers("Hvor lang var Christian 4. regeringstid?", new string[] { "52 år", "60 år", "45 år", "36 år" }, 0));
        _QNA.Add(new QuestionsAndAnswers("Hvem var Christian 4. i krig mod i 1625?", new string[] { "Den franske kong", "Den engelske dronning", "Greven af Venedig", "Den tysk-romersk kejser" }, 3));

        // Statskuppet 1660
        _QNA.Add(new QuestionsAndAnswers("Hvilken styreform overgik Danmark til efter statskuppet (1660)?", new string[] { "Demokrati", "Enevælde", "Diktatur", "Adelsvælde" }, 1));
        _QNA.Add(new QuestionsAndAnswers("Hvilken konge var med til statskuppet?", new string[] { "Christian 5.", "Valdemar Atterdag", "Frederik 3.", "Christian 4." }, 2));
        _QNA.Add(new QuestionsAndAnswers("Efter svenskekrigene 1657-1660, hvilket områder mistede Danmark-Norge?", new string[] { "Skåne, Halland og Blekinge", "Slesvig-Holsten", "De dansk vestindiske øer", "Norge" }, 0));
        _QNA.Add(new QuestionsAndAnswers("Hvem var en af drivkræfterne i diskussionen om enevælde?", new string[] { "Theodor Zhale", "Curfitz Ulfeldt", "Hans Zimmerman", "Peder Schumacher (Griffenfeldt)" }, 3));

        // Grundloven
        _QNA.Add(new QuestionsAndAnswers("Hvilken kong underskrev grundloven?", new string[] { "Frederik 7.", "Christian 7.", "Christian 5.", "Frederik 6." }, 0));
        _QNA.Add(new QuestionsAndAnswers("Hvem havde stemmeret i forhold til Grundloven fra 1849?", new string[] { "Mænd og kvinder", "Alle mænd", "Mænd over 30 år", "Adlen" }, 2));
        _QNA.Add(new QuestionsAndAnswers("Hvor stor procentdel af den danske befolkning kunne stemme i 1849?", new string[] { "34%", "14%", "12%", "8%" }, 1));
        _QNA.Add(new QuestionsAndAnswers("Hvilken af disse rettigheder fik danskerne i 1849?", new string[] { "Abort frihed", "Forsamlingsfrihed", "Kvinders stemmeret", "Stemmeret til 18 år" }, 1));

        // Genforeningen / Påskekrisen
        _QNA.Add(new QuestionsAndAnswers("Hvad truede de socialdemokratiske med, under krisen?", new string[] { "Generalstrejke", "Statskup", "Kommunistisk revolution", "Revolution" }, 2));


        // Her henter vi alle komponenterne fra de forskellige objekter.
        _quizRoot = _quiz.GetComponent<UIDocument>().rootVisualElement;
        _label = _quizRoot.Q<Label>("Question");
        _score = _quizRoot.Q<Label>("Score");
        _button[0] = _quizRoot.Q<Button>("Answer1");
        _button[1] = _quizRoot.Q<Button>("Answer2");
        _button[2] = _quizRoot.Q<Button>("Answer3");
        _button[3] = _quizRoot.Q<Button>("Answer4");

        _winUI = _win.GetComponent<UIDocument>();
        _quizUI = _quiz.GetComponent<UIDocument>();
        _gameOverUI = _gameOver.GetComponent<UIDocument>();

        _score.text = (_rightAnswers + "/3");


    }
    void Start()
    {
        _randomIndex = UnityEngine.Random.Range(0, _QNA.Count);
        setQuestionAndAswers(_QNA[_randomIndex]);
    }

    void Update()
    {
        // Her tager vi forbehold for om spilleren har svaret forkert 2 gange, hvis spilleren har svaret de 2 første forkert så stopper spillet med det samme og hvis ikke, så tjekker den om han svarer rigtigt eller forkert på det sidste spørgsmål
        if (_questionAmount == 3)
        {
            if (_wrongAnswers >= 2)
            {
                _wrongAnswers = 0;
                _questionAmount = 0;
                Debug.Log("noob");
                GameOver();
            }
            else
            {
                _questionAmount = 0;
                _wrongAnswers = 0;
                WinGame();
            }
        }
        if (_wrongAnswers >= 2)
        {

            _wrongAnswers = 0;
            _questionAmount = 0;
            Debug.Log("Du Noob");
            GameOver();
        }
        if (_hasAnswered)
        {

            if (_wait < _timer)
            {
                _wait = _wait + Time.deltaTime;
            }
            else
            {
                ResetColors();
                _randomIndex = UnityEngine.Random.Range(0, _QNA.Count);
                setQuestionAndAswers(_QNA[_randomIndex]);
                _score.text = (_rightAnswers + "/3");
                _hasAnswered = false;
                _wait = 0;
            }
        }

    }
    // Metoden som reagerer på det rigtige svar
    public void RightAnswer()
    {
        setColors();
        _questionAmount++;
        _rightAnswers++;
        Debug.Log(_questionAmount);
        Debug.Log("Rigtigt svar");
    }

    // Metoden som reagerer på det forkerte svar
    public void WrongAnswer()
    {
        setColors();
        _questionAmount++;
        _wrongAnswers++;
        Debug.Log("Forkert svar");
        Debug.Log(_questionAmount);

    }
    // Sætter spørgsmålet til det som der tilfældigt blevet valgt fra listen, samt sæt alle svarmulighederne til de muligheder der er og sæt "RightAnswer()" metoden på den rigtige og "WrongAnswer()" på den forkerte
    void setQuestionAndAswers(QuestionsAndAnswers qna)
    {
        _label.text = qna.Question;


        for (int i = 0; i < 4; i++)
        {
            _button[i].text = qna.Answers[i];
            _button[i].clicked -= RightAnswer;
            _button[i].clicked -= WrongAnswer;

            if (i == qna.RightAnswerIndex)
            {
                _button[i].clicked += RightAnswer;

            }
            else
            {
                _button[i].clicked += WrongAnswer;
            }
        }
        Debug.Log("setQNA right index : " + qna.RightAnswerIndex);
    }

    // Metoden der bliver kaldt hvis det er spilleren har 2 forkerte svar.
    void GameOver()
    {
        _quizUI.enabled = false;
        _gameOverUI.enabled = true;
        Debug.Log("Game over");
        _gameOverRoot = _gameOver.GetComponent<UIDocument>().rootVisualElement;
        _btn = _gameOverRoot.Q<Button>("TryAgain");
        _btn.clicked += TryAgain;
        _btn = _gameOverRoot.Q<Button>("MainMenu");
        _btn.clicked += MainMenu;
    }
    // Metoden der bliver kaldt hvis spilleren vinder runden.
    public void WinGame()
    {
        _quizUI.enabled = false;
        _winUI.enabled = true;
        Debug.Log("Win game");
        _winRoot = _win.GetComponent<UIDocument>().rootVisualElement;
        _btn = _winRoot.Q<Button>("NextLVL");
        _btn.clicked += MainMenu;
        _btn = _winRoot.Q<Button>("MainMenu");
        _btn.clicked += MainMenu;
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void setColors()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == _QNA[_randomIndex].RightAnswerIndex)
            {
                _button[i].style.color = Color.green;
            }
            else
            {
                _button[i].style.color = Color.red;
            }
        }
        Debug.Log("setColors Righ index : " + _QNA[_randomIndex].RightAnswerIndex);
        _hasAnswered = true;
    }
    public void ResetColors()
    {
        for (int i = 0; i < 4; i++)
        {
            _button[i].style.color = Color.black;
        }
    }
}
