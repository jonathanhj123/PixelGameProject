using System.ComponentModel.Design.Serialization;
using System.Linq;
using Mono.Cecil.Cil;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UIElements;

public class QuizManagerScript : MonoBehaviour
{
    private VisualElement _root;
    private Button[] _button = new Button[4];
    private int _RAI = 3;
    void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;

        _button[0] = _root.Q<Button>("Answer1");
        _button[1] = _root.Q<Button>("Answer2");
        _button[2] = _root.Q<Button>("Answer3");
        _button[3] = _root.Q<Button>("Answer4");

        _button[0].text = "Fuck";
        _button[1].text = "Af";
        _button[2].text = "Du";
        _button[3].text = "Grim";

        _RAI = 2;

        for (int i = 0; i < 4; i++)
        {
            if (i == _RAI)
            {
                _button[i].clicked += () => rightAnswer();
            }
            else
            {
                _button[i].clicked += () => wrongAnswer();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void wrongAnswer()
    {
        Debug.Log("Forkert Svar");
    }

    public void rightAnswer()
    {
        Debug.Log("Rigtig Svar");
    }
}
