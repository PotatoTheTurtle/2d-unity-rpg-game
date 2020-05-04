using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{
    public FloatValue music;
    private AudioSource audioComponent;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        audioComponent = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        audioComponent.volume = music.RuntimeValue;
    }
}
