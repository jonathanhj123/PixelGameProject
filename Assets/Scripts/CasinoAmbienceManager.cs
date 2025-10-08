using UnityEngine;

public class CasinoAmbienceManager : MonoBehaviour
{

    public AudioSource _audioCasinoAmbience;
    [SerializeField] private AudioClip BGMusic;
    [SerializeField] private AudioClip Ambience;

    void Start()
    {
        _audioCasinoAmbience.PlayOneShot(BGMusic, 0.5f);
        _audioCasinoAmbience.PlayOneShot(Ambience, 0.5f);
    }

    
}
