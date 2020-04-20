﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeCutScript : MonoBehaviour
{
    [SerializeField] GameObject woodPile;
    [SerializeField] float treeChoppingTime;

    Slider choppingBarSlider;

    GameObject world;
    GameObject choppingBarCanvas;
    GameObject woodPileSpawn;

    PlayerVariableHolder playerVariables;
    
    void OnMouseDown()
    {
        world = GameObject.Find("World");
        playerVariables = world.GetComponent<PlayerVariableHolder>();
        choppingBarCanvas = this.gameObject.transform.GetChild(1).gameObject;
        choppingBarSlider = choppingBarCanvas.GetComponentInChildren<Slider>();


        if (playerVariables.isPlayerChopping == false)
        {
            playerVariables.isPlayerChopping = true;

            //adiciona mover o jogador para a arvore

            StartCoroutine(chopper());
        }
    }
    
    IEnumerator chopper() 
    {
        choppingBarCanvas.SetActive(true);

        yield return new WaitForSeconds(treeChoppingTime / 5); //poderia colocar o som de chop a cada update
        choppingBarSlider.value = 0.8f;
        yield return new WaitForSeconds(treeChoppingTime / 5);
        choppingBarSlider.value = 0.6f;
        yield return new WaitForSeconds(treeChoppingTime / 5);
        choppingBarSlider.value = 0.4f;
        yield return new WaitForSeconds(treeChoppingTime / 5);
        choppingBarSlider.value = 0.2f;
        yield return new WaitForSeconds(treeChoppingTime/5);
        choppingBarSlider.value = 0f;

        woodPileSpawn = Instantiate(woodPile, transform.position, transform.rotation) as GameObject;
        woodPileSpawn.GetComponent<WoodPileScript>().world = world;
        
        playerVariables.isPlayerChopping = false;
        
        Destroy(gameObject);
    }

}
  