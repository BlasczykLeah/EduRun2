using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

[RequireComponent(typeof(AudioSource))]
public class enemySonicEssence : MonoBehaviour
{
    [SerializeField] enemyOptions enemy;

    AudioSource essenceSource;

    private void Awake()
    {
        essenceSource = GetComponent<AudioSource>();
        essenceSource.playOnAwake = false;
    }

    [SerializeField]
    float waitMin, waitMax;
    bool randomDelay;

    void Start()
    {
        // set output routing
        AudioMixer mixer = Resources.Load("MainMixer") as AudioMixer;
        AudioMixerGroup enemyGroup = mixer.FindMatchingGroups("Master/EnemyEssence")[0];

        essenceSource.outputAudioMixerGroup = enemyGroup;

        // set loop OFF
        if (essenceSource.loop)
            essenceSource.loop = false;

        // set random pitch
        essenceSource.pitch = UnityEngine.Random.Range(.9f, 1.2f);

        // set audio clip and volume
        float volume;
        essenceSource.clip = AudioManager.audioManager.GetEnemyEssenceClip(enemy, out volume);
        essenceSource.volume = volume;

        // set audiosource 
        essenceSource.spatialBlend  = 1;
        essenceSource.rolloffMode = AudioRolloffMode.Linear;

        // play
        if (essenceSource?.isPlaying == false) // can't use !bool contruction with null-conditional / elvis operator
            essenceSource.Play();
    }

    void PlayEssense(){
        essenceSource.pitch = UnityEngine.Random.Range(.95f, 1.05f);
        essenceSource.Play();
        randomDelay = false;
    }

    void Update(){
        if(!essenceSource.isPlaying && !randomDelay){
            randomDelay = true;
            float randomDur = UnityEngine.Random.Range(waitMin, waitMax);
            StartCoroutine(waitThenPlay(randomDur, PlayEssense));
        }
    }

    IEnumerator waitThenPlay(float dur, Action onComplete){
        yield return new WaitForSeconds(dur);
        onComplete();
    }

    void OnDestroy(){
        essenceSource?.Stop();
    }

    void OnDisable(){
        essenceSource?.Stop();
    }
}
