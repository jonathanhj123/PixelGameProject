using UnityEngine;

public class CasinoAmbienceManager : MonoBehaviour
{

    public AudioSource _audioAmbience;
    [SerializeField] private AudioClip BGMusic;
    [SerializeField] private AudioClip Ambience;

    void Start()
    {
        _audioAmbience.PlayOneShot(BGMusic, 0.5f);
        _audioAmbience.PlayOneShot(Ambience, 0.5f);
    }

    
}
