using System;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class QuestionsAndAnswers : MonoBehaviour
{

    public string _question;
    public string[] _answers = new string[4];
    public int RightAnswerIndex;


    public QuestionsAndAnswers(String theQuestion, String[] answers, int rightAnswerIndex)
    {
        _question = theQuestion;
        _answers = answers;
        RightAnswerIndex = rightAnswerIndex;
    }
    public void Awake()
    {

    }


    public int getRightAnswerIndex()
    {
        return RightAnswerIndex;
    }
}