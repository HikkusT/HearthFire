using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] float torchInitialFuel; //colocar o mesmo que o da fireplace
    [SerializeField] float warningDisplayTime;

    [SerializeField] Gradient barGradient;

    [SerializeField] Slider healthBarSlider;

    [SerializeField] GameObject torchText;
    [SerializeField] GameObject torchTextBackimage;
    [SerializeField] GameObject torchBar;
    [SerializeField] GameObject torch;
    [SerializeField] GameObject barSlider;
    [SerializeField] GameObject woodWarning;
    [SerializeField] GameObject inventoryFullWarning;
    [SerializeField] PlayerVariableHolder playerVariables;
    [SerializeField] Text woodBarText;

    Slider torchBarSlider;

    Image fill;

    Text torchStatus;
    
    TorchLight torchScript;

    float fuelPercentage;

    bool displayOnEffect;

    void Start()
    {
        torchStatus = torchText.GetComponent<Text>();
        torchBarSlider = torchBar.GetComponent<Slider>();
        torchScript = torch.GetComponent<TorchLight>();
        fill = barSlider.GetComponent<Image>();
    }

    void Update()
    {
        healthBarSlider.value = (playerVariables.maxLife - playerVariables.life) / playerVariables.maxLife;

        fuelPercentage = (torchScript.torchFuel / torchInitialFuel) * 100f;

        woodBarText.text = string.Format("Wood:{0}/{1}Kg", playerVariables.wood.ToString("f0"), playerVariables.maxWood.ToString("f0"));

        if (fuelPercentage > 0)
        {
            torchBar.SetActive(true);
            
            torchStatus.text = string.Format("Torch: {0}%", fuelPercentage.ToString("f0"));

            torchBarSlider.value = fuelPercentage / 100f;

            fill.color = barGradient.Evaluate(torchBarSlider.normalizedValue);
        }    
        
        else
        {
            torchBar.SetActive(false);
        }

        if(playerVariables.displayWoodWarning == true && displayOnEffect == false)
        {
            StartCoroutine(WarningCleanCoroutine(warningDisplayTime));
            woodWarning.SetActive(true);
            displayOnEffect = true;
        }

        if (playerVariables.displayInventoryFullWarning == true && displayOnEffect == false)
        {
            StartCoroutine(WarningCleanCoroutine(warningDisplayTime));
            inventoryFullWarning.SetActive(true);
            displayOnEffect = true;
        }
        
        if (playerVariables.displayInventoryFullWarning == true && displayOnEffect == true)
        {
            playerVariables.displayInventoryFullWarning = false;
        }
        
        if (playerVariables.displayWoodWarning == true && displayOnEffect == true)
        {
            playerVariables.displayWoodWarning = false;
        }

        IEnumerator WarningCleanCoroutine(float cleanTime)
        {
            yield return new WaitForSeconds(cleanTime);
            woodWarning.SetActive(false);
            inventoryFullWarning.SetActive(false);
            displayOnEffect = false;
        }   
    }
}
