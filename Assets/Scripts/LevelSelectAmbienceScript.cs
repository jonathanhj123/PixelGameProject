using UnityEngine;

public class LevelSelectAmbienceScript : MonoBehaviour
{

    public AudioSource _audioLevelAmbience;
    [SerializeField] private AudioClip _levelBGMusic;

    void Start()
    {
        _audioLevelAmbience.PlayOneShot(_levelBGMusic, 0.5f);
    }


}
