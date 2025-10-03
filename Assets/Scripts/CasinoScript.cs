using UnityEngine;
using UnityEngine.UIElements;

public class CasinoScript : MonoBehaviour
{
    private Label _Card;
    private VisualElement _casinoUIRoot;
    private Button[] _buttons = new Button[4];
    public Sprite[] _cards = new Sprite[4]; // 0 = Es, 1 = Konge, 2 = Dame, 3 = Bonde.
    void Start()
    {

        _casinoUIRoot = GameObject.FindWithTag("CasinoUIDOC").GetComponent<UIDocument>().rootVisualElement;
        _Card = _casinoUIRoot.Q<Label>("Card");
        _buttons[0] = _casinoUIRoot.Q<Button>("Answer1");
        _buttons[1] = _casinoUIRoot.Q<Button>("Answer2");
        _buttons[2] = _casinoUIRoot.Q<Button>("Answer3");
        _buttons[3] = _casinoUIRoot.Q<Button>("Answer4");


        UpdateCasinoCard();
        SetRightAnswer();
    }


    void Update()
    {

    }

    public void UpdateCasinoCard()
    {
        // update the card to show what the card is.
    }

    public void SetRightAnswer()
    {
        // set the right button with the right answer
    }

    public void RightAnswer()
    {
        // Show it was right answer, give them a point
    }

    public void WrongAnswer()
    {
        // Shot it was wrong answer end the streak and go back to the level select
    }
}
