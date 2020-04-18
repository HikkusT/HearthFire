using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireplaceLight : MonoBehaviour
{

    [SerializeField] float initialFuel;
    [SerializeField] float fuelDecayRate;
    [SerializeField] float fuelIncrease;
    [SerializeField] float fatorDeAlcanceDeLuz;
    [SerializeField] float fatorDeIntensidadeDeLuz;
     
    float fuel;
    Transform luminousArea;
    Transform penumbraArea;
    Light luz;

    void Start()
    {
        fuel = initialFuel;
        luminousArea = this.gameObject.transform.GetChild(0);
        penumbraArea = this.gameObject.transform.GetChild(1);
        luz = GetComponentInChildren<Light>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            fuel += fuelIncrease;
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