using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    new public AudioClip audio;

    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        gameObject.AddComponent<AudioSource>();
        gameObject.GetComponent<AudioSource>().clip = audio;
        timer = audio.length;
        gameObject.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
    if(timer<=0) Destroy(gameObject);    
    timer-=Time.deltaTime;

    }
}
