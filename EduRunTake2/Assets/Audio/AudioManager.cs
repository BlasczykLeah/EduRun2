using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyOptions { triangle, circle, slime };

[System.Serializable]
public struct enemyEssence
{
    public enemyOptions enemy;
    public AudioClip audioClip;
    [Range(0.0f, 1.2f)]
    public float clipVolume;
}

[System.Serializable]
public enum fxOptions { itemPickup, correct, incorrect, gethit, tap };
//public enum fxOptions { fireBall, fireballImpact, squish, slime, bat, itemPickup, itemDrop };


[System.Serializable]
public struct audioOptions{
    public fxOptions fx;
    public AudioClip audioClip;
    
    [Range(0.0f, 1.2f)]
    public float clipVolume;
}

[System.Serializable]
public struct soundtrackOptions
{
    public AudioClip audioClip;
    public int playCount;
}



public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    [Header("Don't repeat enemy options")]
    [SerializeField] enemyEssence[] enemyEssenceOptions = new enemyEssence[] { };

    private void Awake()
    {
        if(audioManager == null)
        {
            audioManager = this;
        } else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public AudioClip GetEnemyEssenceClip(enemyOptions enemy, out float volume)
    {
        foreach (var essence in enemyEssenceOptions)
        {
            if(enemy == essence.enemy)
            {
                volume = enemyEssenceOptions[(int)essence.enemy].clipVolume;
                return enemyEssenceOptions[(int)essence.enemy].audioClip;
            }
        }
        volume = 1;
        return null;
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     FXplayer.fxplayer.PlayFX(fxOptions.itemPickup);
        // }

        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     FXplayer.fxplayer.PlayFX(fxOptions.fireball);
        // }
    }
}
