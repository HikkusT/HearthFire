﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVariableHolder : MonoBehaviour
{
    [SerializeField] float playerNearDistanceforWood;
    
    
    [SerializeField] GameObject player;

    [HideInInspector] public bool isPlayerNear;
    [HideInInspector] public bool isPlayerInventoryFull;
    [HideInInspector] public bool isPlayerChopping;
     public bool isPlayerOnLight;
    [HideInInspector] public bool isPlayerOnPenumbra;
    [HideInInspector] public bool isTorchOn;
    [HideInInspector] public bool playerHasWood;
    [HideInInspector] public bool displayWoodWarning;
    [HideInInspector] public bool displayInventoryFullWarning;
    [HideInInspector] public bool soundEffects;
    [HideInInspector] public bool isLightPathToplayerBlocked;

    [HideInInspector] public float playerDistanceToFireplace;
    [HideInInspector] public float life;
    [HideInInspector] public float wood;
    [HideInInspector] public float lightRadius;

    public float maxWood;
    public float maxLife;
    
    void Start()
    {
        life = maxLife;
        soundEffects = true;
    }

    void Update()
    {      
        if(wood == maxWood) 
        {
            isPlayerInventoryFull = true;
        }
        else 
        {
            isPlayerInventoryFull = false;
        }

        if(wood == 0)
        {
            playerHasWood = false;
        }
        else 
        {
            playerHasWood = true;
        }

        if (isTorchOn = true)
        {
            isPlayerOnLight = true;
        }
        if (playerDistanceToFireplace <= lightRadius && isLightPathToplayerBlocked == false)
        {
            isPlayerOnLight = true;
        }
        else
        {
            isPlayerOnLight = false;
        }

        if (playerDistanceToFireplace <= playerNearDistanceforWood)
        {
            isPlayerNear = true;
        }
        else
        {
            isPlayerNear = false;
        }
    }
}
