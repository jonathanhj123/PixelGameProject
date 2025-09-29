using System;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class QuestionsAndAnswers : MonoBehaviour
{

    public string Question;
    public string[] Answers = new string[4];
    public int RightAnswerIndex;


    public QuestionsAndAnswers(String theQuestion, String[] answers, int rightAnswerIndex)
    {
        Question = theQuestion;
        Answers = answers;
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