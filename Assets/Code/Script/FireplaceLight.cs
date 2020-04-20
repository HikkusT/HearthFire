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
    public GameObject torch;
    public GameObject world;
    public GameObject fireplaceText;
    public GameObject fireplaceTextBackimage;
    [SerializeField] Light luz;

    float timeLeft;
    float fuel;
    float wood;
    float internalTimeManager; // utilizado para atualizar o horario da update executada em cada segundo na linha 52
    float nextUpdate; //Idem

    Transform luminousArea;
    Transform penumbraArea;
    
    PlayerVariableHolder playerVariables;
    TorchLight torchScript;
    Text fireplaceStatus;

    bool playerHasWood;
    bool isPlayerNear;

    void Start()
    {
        world = GameObject.Find("World");
        torch = world.GetComponent<World>().torch;
        fireplaceText = world.GetComponent<World>().fireplaceText;
        fireplaceTextBackimage = world.GetComponent<World>().fireplaceTextBackimage;
        fuel = fireplaceInitialFuel;
        luminousArea = this.gameObject.transform.GetChild(0);
        penumbraArea = this.gameObject.transform.GetChild(1);
        luz = GetComponentInChildren<Light>();
        playerVariables = world.GetComponent<PlayerVariableHolder>();
        torch.SetActive(false);
        torchScript = torch.GetComponent<TorchLight>();
        fireplaceStatus = fireplaceText.GetComponent<Text>();
    }

    void Update()
    {
        if (isPlayerNear == false)
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
            }
            if(isPlayerNear == true && playerHasWood == false && playerVariables.displayWoodWarning == false)
            {
                playerVariables.displayWoodWarning = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && fuel >= minimumFuelForTorch)
        {
            isPlayerNear = playerVariables.isPlayerNear;
            if (isPlayerNear == true)
            { 
                fuel -= fuelDecreaseByTorch;
                torchScript.torchFuel = torchInitialFuel;
                torch.SetActive(true);
            }
        }

        if (fuel >= 0)
        {
            fuel -= fuelDecayRate;

            luminousArea.localScale = new Vector3(fuel, 0.1f, fuel);
            penumbraArea.localScale = new Vector3(fuel * 1.7f, 0.1f, fuel * 1.7f);
            luz.intensity = (fuel * fatorDeIntensidadeDeLuz);
            luz.range = (fuel * fatorDeAlcanceDeLuz);
        }
    }
}