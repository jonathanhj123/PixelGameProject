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

    public QuestionsAndAnswers generateQuestion(String question, String answer1, String answer2, String answer3, String answer4, int rightAnswerIndex)
    {
        String[] answer = { answer1, answer2, answer3, answer4 };
        return new QuestionsAndAnswers(question, answer, rightAnswerIndex);
    }

    public int getRightAnswerIndex()
    {
        return RightAnswerIndex;
    }
}