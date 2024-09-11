using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GunManager gun;
    public AudioClip[] sGun;
    public AudioClip[] rGun;
    public AudioClip hitMarker;
    public AudioSource[] audioSource;

    public AudioSource[] playerSounds;
    public bool[] soundPlayingCheck;

    public void Shoot () {
        audioSource[0].spatialBlend = Random.Range(0.4f, 0.6f);
        audioSource[0].PlayOneShot(sGun[gun.gun]);
    }

    public void Reload () {
        audioSource[1].PlayOneShot(rGun[gun.gun]);
    }

    public void HitMarker () {
        audioSource[2].pitch = Random.Range(0.9f, 1.1f);
        audioSource[0].spatialBlend = Random.Range(0f, 0.2f);
        audioSource[2].PlayOneShot(hitMarker);
    }
    
    bool movementSound0Played;
        bool movementSound1Played;

    public void PlayerMovement (int movementType) {
        for (int i = 0; i < playerSounds.Length; i++) {
            if (i != movementType) {
                    playerSounds[i].Stop();
                    soundPlayingCheck[i] = false;
                } else {
                    if (soundPlayingCheck[movementType] != true) {
                        playerSounds[movementType].Play();
                        soundPlayingCheck[movementType] = true;
                    }
            }
        }
    }

    public void StopPlayerSounds () {
        for (int i = 0; i < playerSounds.Length; i++) {
            playerSounds[i].Stop();
        }
    }
}
