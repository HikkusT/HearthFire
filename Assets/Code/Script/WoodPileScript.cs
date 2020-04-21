using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodPileScript : MonoBehaviour
{
    [SerializeField] float maxWoodPileSize;
    [SerializeField] float minWoodPileSize;
    [SerializeField] AudioSource failSound;

    AudioSource pickupSound;

    float woodPileInitialSize;
    float woodPileSize;
    bool clicked = false;

    public Slider choppingBarSlider;

    public GameObject world;
    
    GameObject choppingBarCanvas;

    PlayerVariableHolder playerVariables;

    void OnMouseDown()
    {
        pickupSound = world.GetComponent<AudioSource>();
        playerVariables = world.GetComponent<PlayerVariableHolder>();
        choppingBarCanvas = this.gameObject.transform.GetChild(1).gameObject;
        choppingBarSlider = choppingBarCanvas.GetComponentInChildren<Slider>();

        if (clicked == false)
        {
            woodPileInitialSize = Mathf.Ceil(Random.Range(minWoodPileSize, maxWoodPileSize));
            woodPileSize = woodPileInitialSize;
            
            clicked = true;
        }

        if (playerVariables.isPlayerInventoryFull == true)
        {
            playerVariables.displayInventoryFullWarning = true;
            failSound.Play(0);
        }

        if (playerVariables.isPlayerInventoryFull == false)
        {        
            if (playerVariables.maxWood - playerVariables.wood >= woodPileSize )
            {
                //mover o jogador a pilha

                playerVariables.wood += woodPileSize;
                if (playerVariables.soundEffects == true)
                {
                    pickupSound.Play(0);
                }
                Destroy(gameObject);
            }


            else
            {
                //mover o jogador a pilha
                
                choppingBarCanvas.SetActive(true);
                woodPileSize -= playerVariables.maxWood - playerVariables.wood;
                choppingBarSlider.value = woodPileSize / woodPileInitialSize;
                playerVariables.wood += playerVariables.maxWood - playerVariables.wood;
                if (playerVariables.soundEffects == true)
                {
                    pickupSound.Play(0);
                }
            }
        }
    }
}
