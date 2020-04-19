using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] float torchInitialFuel;

    [SerializeField] Gradient barGradient;

    GameObject torchText;
    GameObject torchTextBackimage;
    GameObject torchBar;
    GameObject torch;
    GameObject barSlider;

    Slider torchBarSlider;

    Image fill;

    Text torchStatus;
    
    TorchLight torchScript;

    float fuelPercentage;

    void Start()
    {
        torchText = GameObject.Find("TorchText");
        torchStatus = torchText.GetComponent<Text>();
        torchTextBackimage = GameObject.Find("TorchTextBackimage");
        torchBar = GameObject.Find("TorchBar");
        torchBarSlider = torchBar.GetComponent<Slider>();
        torch = GameObject.Find("Torch");
        torchScript = torch.GetComponent<TorchLight>();
        barSlider = GameObject.Find("BarSlider");
        fill = barSlider.GetComponent<Image>();
    }

    void Update()
    {
        fuelPercentage = (torchScript.torchFuel / torchInitialFuel) * 100f;

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
    }
}
