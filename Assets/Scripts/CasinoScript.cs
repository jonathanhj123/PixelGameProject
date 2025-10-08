using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CasinoScript : MonoBehaviour
{
    private Label _Card;
    private VisualElement _casinoUIRoot;
    private Button[] _buttons = new Button[4];
    [SerializeField] private Sprite[] Cards = new Sprite[4]; // 0 = Es, 1 = Konge, 2 = Dame, 3 = Bonde.
    private int _Score = 0;
    private Label _ScoreLabel;
    private int _HighScore = 0;
    private Label _HighScoreLabel;
    private bool _HasAnswered = false;
    private int _Random = 0;
    private float _wait = 0;
    [SerializeField] public int _timer = 1;
    [SerializeField] private Sprite _CardBack;

    //Audio variables
    public AudioSource _audioFX;
    [SerializeField] private AudioClip BigMoney;
    [SerializeField] private AudioClip CardFlip;

    void Start()
    {

        _casinoUIRoot = GameObject.FindWithTag("CasinoUIDOC").GetComponent<UIDocument>().rootVisualElement;
        _Card = _casinoUIRoot.Q<Label>("Card");
        _buttons[0] = _casinoUIRoot.Q<Button>("Answer1");
        _buttons[1] = _casinoUIRoot.Q<Button>("Answer2");
        _buttons[2] = _casinoUIRoot.Q<Button>("Answer3");
        _buttons[3] = _casinoUIRoot.Q<Button>("Answer4");
        _HighScoreLabel = _casinoUIRoot.Q<Label>("HighScore");
        _ScoreLabel = _casinoUIRoot.Q<Label>("Score");
        _ScoreLabel.text = "Score: 0";
        _HighScoreLabel.text = "HighScore: 0";




        _buttons[0].text = "Es";
        _buttons[1].text = "Konge";
        _buttons[2].text = "Dame";
        _buttons[3].text = "Bonde";


        SetAnswerAndCard();


        
    }


    void Update()
    {

        if (_HasAnswered)
        {

            if (_wait < _timer)
            {
                _wait = _wait + Time.deltaTime;
            }
            else
            {
                ResetColors();
                SetAnswerAndCard();
                _HasAnswered = false;
                _wait = 0;
            }
        }
    }


    public void SetAnswerAndCard()
    {
        _Random = Random.Range(0, 4);
        _Card.style.backgroundImage = Background.FromSprite(_CardBack);

        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].clicked -= RightAnswer;
            _buttons[i].clicked -= WrongAnswer;
            if (_Random == i)
            {
                _buttons[i].clicked += RightAnswer;
            }
            else
            {
                _buttons[i].clicked += WrongAnswer;
            }
        }
        ResetColors();
        Debug.Log(_Random);
    }

    public void RightAnswer()
    {
        _Score++;
        UpdateScore();
        Debug.Log("Right");
        setColors();
        _HasAnswered = true;
        _Card.style.backgroundImage = Background.FromSprite(Cards[_Random]);

        //Play audio fx with random pitch
        _audioFX.pitch = Random.Range(0.7f, 1.3f);
        _audioFX.PlayOneShot(BigMoney, 0.5f);
        _audioFX.PlayOneShot(CardFlip, 1.5f);
    }

    public void WrongAnswer()
    {
        Debug.Log("Wrong");
        setColors();
        if (_Score > _HighScore)
        {
            _HighScore = _Score;
            _HighScoreLabel.text = "High Score: " + _HighScore.ToString();
        }
        _HasAnswered = true;
        _Card.style.backgroundImage = Background.FromSprite(Cards[_Random]);
        _Score = 0;
        UpdateScore();

        //Play audio fx with random pitch
        _audioFX.pitch = Random.Range(0.7f, 1.3f);
        _audioFX.PlayOneShot(CardFlip, 1.5f);
    }


    public void setColors()
    {
        for (int i = 0; i < 4; i++)
        {
            if (_Random == i)
            {
                _buttons[i].style.color = Color.green;
            }
            else
            {
                _buttons[i].style.color = Color.red;
            }
        }
    }

    public void ResetColors()
    {
        for (int i = 0; i < 4; i++)
        {
            _buttons[i].style.color = Color.black;
        }
    }
    public void UpdateScore()
    {
        _ScoreLabel.text = "Score : " + _Score.ToString();   
    }
}
