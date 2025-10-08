using UnityEngine;

public class QuizAmbienceManager : MonoBehaviour
{

    public AudioSource _audioQuizAmbience;
    [SerializeField] private AudioClip _quizBGMusic;

    void Start()
    {
        _audioQuizAmbience.PlayOneShot(_quizBGMusic, 0.5f);
    }


}
