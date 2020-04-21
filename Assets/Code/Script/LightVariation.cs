using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightVariation : MonoBehaviour
{
    [SerializeField] float maxVariation;
    [SerializeField] float minVariation;
    private float currIntensity = 5;
    private Light source;
    private float seed;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<Light>();
        seed = Random.Range(0, 1000);
    }

    // Update is called once per frame
    void Update()
    {
        float noise = Mathf.PerlinNoise(seed, Time.time);
        source.intensity = Mathf.Lerp(currIntensity * minVariation, currIntensity * maxVariation, noise);
    }
}
