using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireplaceLight : MonoBehaviour
{
    [SerializeField] float fireplaceInitialFuel;
    [SerializeField] float torchInitialFuel;
    [SerializeField] float fuelDecayRate;
    [SerializeField] float fuelIncreaseByWood;
    [SerializeField] float fuelDecreaseByTorch;
    [SerializeField] float minimumFuelForTorch;
    [SerializeField] float fatorDeAlcanceDeLuz;
    [SerializeField] float fatorDeIntensidadeDeLuz;
    [SerializeField] float lightInitialRadius;

    [SerializeField] AudioSource pickupSound;
    [SerializeField] AudioSource fireplaceAudio;
    [SerializeField] AudioSource fireplaceFuelIncreaseAudio;
    [SerializeField] AudioClip failSound;
    [SerializeField] AudioClip fuelIncreaseSound;

    [SerializeField] Light luz;

    [SerializeField] Transform rayCastFirePoint;
    GameObject torch;
    GameObject world;
    GameObject fireplaceText;
    GameObject fireplaceTextBackimage;
    GameObject player;
    
    float timeLeft;
    float fuel;
    float wood;
    float internalTimeManager; // utilizado para atualizar o horario da update executada em cada segundo na linha 52+
    float nextUpdate; //Idem

    RaycastHit hit;
    
    PlayerVariableHolder playerVariables;
    TorchLight torchScript;
    Text fireplaceStatus;

    bool playerHasWood;
    bool isPlayerNear;

    void Start()
    {
        world = GameObject.Find("World");
        player = world.GetComponent<World>().player;
        torch = world.GetComponent<World>().torch;
        fireplaceText = world.GetComponent<World>().fireplaceText;
        fireplaceTextBackimage = world.GetComponent<World>().fireplaceTextBackimage;
        fuel = fireplaceInitialFuel;
        luz = GetComponentInChildren<Light>();
        playerVariables = world.GetComponent<PlayerVariableHolder>();
        torch.SetActive(false);
        torchScript = torch.GetComponent<TorchLight>();
        fireplaceStatus = fireplaceText.GetComponent<Text>();
        pickupSound = world.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerVariables.isPlayerNear == false)
        {
            if (internalTimeManager != 0f && nextUpdate != 1f)
            {
                internalTimeManager = 0f;
                nextUpdate = 1f;
            }
            if (fireplaceText.activeSelf == true || fireplaceTextBackimage.activeSelf == true) 
            {
                fireplaceText.SetActive(false);
                fireplaceTextBackimage.SetActive(false);
            }
        }
        
        if(playerVariables.isPlayerNear == true && Time.time > 0.5f)
        {
            internalTimeManager += Time.deltaTime;
            fireplaceText.SetActive(true);
            fireplaceTextBackimage.SetActive(true);
            wood = fuel / fuelIncreaseByWood;

            if (internalTimeManager >= nextUpdate)
            {
                //Executa a cada segundo
                nextUpdate = Mathf.FloorToInt(internalTimeManager) + 1;
                timeLeft = fuel * Time.deltaTime / (fuelDecayRate * 60f);
            }

            if (timeLeft.ToString("f0") == "1") 
            {
                fireplaceStatus.text = string.Format("There is {0} Kg of Lumber Left, Meaning Around 1 Minute of Fire", wood.ToString("f0"));
            }
            
            else
            {
                fireplaceStatus.text = string.Format("There is {0} Kg of Lumber Left, Meaning Around {1} Minutes of Fire", fuel.ToString("f0"), timeLeft.ToString("f0"));
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            isPlayerNear = playerVariables.isPlayerNear;
            playerHasWood = playerVariables.playerHasWood;

            if(isPlayerNear == true && playerHasWood == true)
            {
                fuel += (fuelIncreaseByWood * playerVariables.wood);
                playerVariables.wood = 0f;
                if (playerVariables.soundEffects == true)
                {
                    fireplaceFuelIncreaseAudio.clip = fuelIncreaseSound;
                    fireplaceFuelIncreaseAudio.Play(0);
                }
            }
            if(isPlayerNear == true && playerHasWood == false && playerVariables.displayWoodWarning == false)
            {
                playerVariables.displayWoodWarning = true;
                fireplaceFuelIncreaseAudio.clip = failSound;
                fireplaceFuelIncreaseAudio.Play(0);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (fuel >= minimumFuelForTorch)
            {
                isPlayerNear = playerVariables.isPlayerNear;
                if (isPlayerNear == true)
                {
                    fuel -= fuelDecreaseByTorch;
                    torchScript.torchFuel = torchInitialFuel;
                    torch.SetActive(true);
                    if (playerVariables.soundEffects == true)
                    {
                        pickupSound.Play(0);
                    }
                }
            }
        
            if(fuel < minimumFuelForTorch && playerVariables.soundEffects == true) 
            {
                fireplaceFuelIncreaseAudio.clip = failSound;
                fireplaceFuelIncreaseAudio.Play(0);
            }
        }

        if (fuel >= 0)
        {
            fuel -= fuelDecayRate;
            playerVariables.lightRadius = Mathf.Sqrt((fuel * fatorDeIntensidadeDeLuz)) * lightInitialRadius;
            luz.intensity = (fuel * fatorDeIntensidadeDeLuz);
            luz.range = (fuel * fatorDeAlcanceDeLuz);
        }

        if (playerVariables.soundEffects == true) 
        {
            fireplaceAudio.mute = false;
        }

        if (playerVariables.soundEffects == false || fuel <= 0)
        {
            fireplaceAudio.mute = true;
        }

        playerVariables.playerDistanceToFireplace = Mathf.Abs(Vector3.Distance(player.transform.position, this.gameObject.transform.position));

        Physics.Raycast(rayCastFirePoint.position, player.transform.position, out hit, playerVariables.playerDistanceToFireplace);
        
        if (hit.collider.CompareTag("Player") == true) 
        {
            playerVariables.isLightPathToplayerBlocked = false;
            Debug.Log("Hit Player");
        }
        if (hit.collider.CompareTag("Player") == false)
        {
            playerVariables.isLightPathToplayerBlocked = true;
            Debug.Log(hit.transform.gameObject.name);
        }
    }
}