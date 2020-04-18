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
    Transform luminousArea;
    Transform penumbraArea;
    Light luz;

    void Start()
    {
        torch = this.gameObject;
        luminousArea = this.gameObject.transform.GetChild(0);
        penumbraArea = this.gameObject.transform.GetChild(1);
        luz = GetComponentInChildren<Light>();
    }

    void Update()
    {
        if(torchFuel > 0) 
        {
            torchFuel -= torchDecayRate;
            
            luminousArea.localScale = new Vector3(torchFuel * 10f, 0.1f, torchFuel * 10f);
            penumbraArea.localScale = new Vector3(torchFuel * 17f, 0.1f, torchFuel * 17f);
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
