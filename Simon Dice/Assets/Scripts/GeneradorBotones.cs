using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneradorBotones : MonoBehaviour
{
    [SerializeField] GameObject BotonPrefab;
    // Start is called before the first frame update
    void Start()
    {
        float tamañoBotonX = BotonPrefab.GetComponent<RectTransform>().rect.width;
        float tamañoBotonY = BotonPrefab.GetComponent<RectTransform>().rect.height;
        var listaDificultades = LevelManager.lvlManager.Dificultades;
        int posX = 0;
        int posY = 0;
        for (int i = 0; i < listaDificultades.Count; i++)
        {
            posX = i % 3;
          
            GameObject newButton = GameObject.Instantiate(BotonPrefab,gameObject.transform);
            newButton.GetComponent<RectTransform>().anchoredPosition=new Vector2((-1 + posX )*(tamañoBotonX + 10), (-1 + posY ) * (tamañoBotonY + 10));
            newButton.GetComponentInChildren<Text>().text = listaDificultades[i].dificultad;
            //Hacer que el boton mande a un nivel y cambie la dificultad al presionarlo
            int x = new int();
            x = i;
            newButton.GetComponent<Button>().onClick.AddListener(delegate{LevelManager.lvlManager.StartGame(listaDificultades[x]); });

            if(i%3 == 2)
            {
                posY--;
            }
     
        
        }
       
    }

}
