using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteMusicScript : MonoBehaviour
{
    [SerializeField] Button muteButton;
    [SerializeField] AudioSource musicPlayer;
    [SerializeField] Sprite mutedButton;
    [SerializeField] Sprite unmutedButton;

    void Start()
    {
        muteButton.onClick.AddListener(() => MuteMusic());
    }

    void MuteMusic()
    {

        if (musicPlayer.mute == false) 
        {
            musicPlayer.mute = true;
            muteButton.image.sprite = mutedButton;
        }
        else 
        {
            musicPlayer.mute = false;
            muteButton.image.sprite = unmutedButton;
        }
    }
}
