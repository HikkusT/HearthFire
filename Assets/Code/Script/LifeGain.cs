using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{
    [SerializeField] float xDistance;

    PlayerVariableHolder playerVariables;

    GameObject world;

    double Counter;
    public GameObject AnalyseLight;
   
    // Start is called before the first frame update
    void Start()
    {
        world = GameObject.Find("World");
        playerVariables = world.GetComponent<PlayerVariableHolder>();
    }

    // Update is called once per frame
    void Update()
    {

        Counter+=Time.deltaTime;

        if(playerVariables.playerDistanceToFireplace <= xDistance /*distanciaDeDoisGrids */ && Counter>=1)
        {
            if(playerVariables.isPlayerOnLight = true)
            {
                playerVariables.life+= 1f;
            }


            Counter = 0;

        }

        else
        {

            Counter = 0;

        }



    }
}
