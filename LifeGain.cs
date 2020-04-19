using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{


    double Counter;
    double Distance;
    public GameObject distance;
    public GameObject Life;
    public GameObject AnalyseLight;
    double x;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Counter+=Time.deltaTime;

        if(distance.GetComponent<distanceBornFire>()Distance <= x /*distanciaDeDoisGrids */ && Counter>=1)
        {
            if(AnalyseLight.GetComponent<Fire>()light>0)
            {
                Life.GetComponent<Lifebar>()Life+=*1;
            }


            Counter=0;

        }
        else(distance.GetComponent<distanceBornFire>()Distance > x  /*distanciaDeDoisGrids */ )
        {

            Counter = 0;

        }



    }
}
