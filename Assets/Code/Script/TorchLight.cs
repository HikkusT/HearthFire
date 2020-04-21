using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchLight : MonoBehaviour
{
    public float torchFuel;
    
    [SerializeField] float torchDecayRate;
    [SerializeField] float fatorDeAlcanceDeLuz;
    [SerializeField] float fatorDeIntensidadeDeLuz;

    GameObject torch;
    Light luz;

    void Start()
    {
        torch = this.gameObject;
        luz = GetComponentInChildren<Light>();
    }

    void Update()
    {
        if(torchFuel > 0) 
        {
            torchFuel -= torchDecayRate;
            
            luz.intensity = (torchFuel * fatorDeIntensidadeDeLuz);
            luz.range = (torchFuel * fatorDeAlcanceDeLuz);
        }
        else 
        {
            torchFuel = 0f;
            torch.SetActive(false);
        }

    }
}
