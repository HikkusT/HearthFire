using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteSoundsScript : MonoBehaviour
{
    [SerializeField] PlayerVariableHolder playerVariables;
    [SerializeField] Button muteButton;
    [SerializeField] Sprite mutedButton;
    [SerializeField] Sprite unmutedButton;

    void Start()
    {
        muteButton.onClick.AddListener(() => MuteMusic());
    }

    void MuteMusic() 
    {
        if (playerVariables.soundEffects == false)
        {
            playerVariables.soundEffects = true;
            muteButton.image.sprite = mutedButton;
        }
        else
        {
            playerVariables.soundEffects = false;
            muteButton.image.sprite = unmutedButton;
        }
    }
}