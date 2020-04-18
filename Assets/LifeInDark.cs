using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeInDark : MonoBehaviour
{

    bool CurrentLight;
    double Counter;
    public GameObject AnalyseLight;
    public GameObject Life;
    // Start is called before the first frame update
    void Start()
    {

        
    }
    // Update is called once per frame
    void Update()
    {
        if(AnalyseLight.GetKeyComponent<Fire>()light!=0)
        {

            //Verifica a luminosidade do jogo, se ela for maior que zero, o if executa
            CurrentLight=true;
        }
        else
        {
            CurrentLight=false;
        }
        Counter+=Time.deltaTime;
        if(CurrentLight==false && Counter >=1)
        {
            Life.GetComponent<Lifebar>()Life+=-1; /*Acessa a Barra de Vida */
            Counter=0;
        }

        else if(CurrentLight==true)
        {

            Counter=0;
        }
        if(Life.GetComponent<Lifebar>()Life==0)
        {

            Destroy(AnalyseLight.GetComponent<Player>());       /* Mata o Jogador */

            //GAMEOVER
        }
        print(Counter);
    }
}
