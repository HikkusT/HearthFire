using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeCutScript : MonoBehaviour
{
    [HideInInspector] public Voxel voxel;
    [SerializeField] GameObject woodPile;
    [SerializeField] float treeChoppingTime;
    [SerializeField] AudioSource choppingSound;

    //DisableThisStart:
    [SerializeField] float maxWoodPileSize;
    [SerializeField] float minWoodPileSize;
    float woodForPlayer;
    AudioSource pickupSound;
    [SerializeField] AudioSource failSound;
    //DisableThisEnd:

    Slider choppingBarSlider;

    GameObject world;
    GameObject choppingBarCanvas;
    GameObject woodPileSpawn;

    PlayerVariableHolder playerVariables;
    private bool isReady = false;
    
    void OnMouseDown()
    {
        //some changes here too
        world = GameObject.Find("World");
        playerVariables = world.GetComponent<PlayerVariableHolder>();
        choppingBarCanvas = this.gameObject.transform.GetChild(1).gameObject;
        choppingBarSlider = choppingBarCanvas.GetComponentInChildren<Slider>();
        pickupSound = world.GetComponent<AudioSource>();

        if (playerVariables.isPlayerInventoryFull == true)
        {
            playerVariables.displayInventoryFullWarning = true;
            failSound.Play(0);
        }

        if (playerVariables.isPlayerChopping == false && playerVariables.isPlayerInventoryFull == false)
        {
            EventManager.Instance.DispatchEvent(voxel);

            isReady = true;
        }
    }

    public void Interact()
    {
        if (isReady && playerVariables.isPlayerChopping == false)
        {
            EventManager.Instance.DispatchEvent(this, true);
            playerVariables.isPlayerChopping = true;
            StartCoroutine(chopper());
        }
    }
    
    IEnumerator chopper() 
    {
        choppingBarCanvas.SetActive(true);

        choppingSound.Play(0);
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
        choppingSound.Stop();

        //To set back woodpile enable this
        //woodPileSpawn = Instantiate(woodPile, transform.position, transform.rotation) as GameObject;
        //woodPileSpawn.GetComponent<WoodPileScript>().world = world;

        //DisableThisStart:
        woodForPlayer = Mathf.Ceil(Random.Range(minWoodPileSize, maxWoodPileSize));

        if (playerVariables.maxWood - playerVariables.wood >= woodForPlayer)
        {
         
            playerVariables.wood += woodForPlayer;
            
            if (playerVariables.soundEffects == true)
            {
                pickupSound.Play(0);
            }
            Destroy(gameObject);
        }
        else
        {
            playerVariables.wood = playerVariables.maxWood;
            if (playerVariables.soundEffects == true)
            {
                pickupSound.Play(0);
            }
        }
    
        
        //DisableThisEnd:

        playerVariables.isPlayerChopping = false;
        
        Destroy(gameObject);
        EventManager.Instance.DispatchEvent(this, false);
    }

}
  