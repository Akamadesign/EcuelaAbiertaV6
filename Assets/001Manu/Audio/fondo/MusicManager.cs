using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource _audiosource;
    public AudioClip[] songs;
    public float volume;
    [SerializeField] private float _trackTimer;

    // Start is called before the first frame update
    void Start()
    {
        _audiosource = GetComponent<AudioSource>();

        if (!_audiosource.isPlaying)
            ChangeSong(Random.Range(0, songs.Length));
    }

    // Update is called once per frame
    void Update()
    {
        _audiosource.volume = volume;

        if (!_audiosource.isPlaying)
            _trackTimer += 1 * Time.deltaTime;

        if (!_audiosource.isPlaying || _trackTimer >= _audiosource.clip.length)
            ChangeSong(Random.Range(0, songs.Length));
        
    }
    public void ChangeSong(int sonPicked)
    {
        _trackTimer = 0;
        _audiosource.clip = songs[sonPicked];
        _audiosource.Play();
    }
}
