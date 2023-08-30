using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuntajeContadorScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        PuntajeScript puntajeScript = mainCamera.GetComponent<PuntajeScript>();
        // Bind to puntajeActual changes to update this text
        puntajeScript.puntajeActualChanged += (int puntajeActual) =>
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = puntajeActual.ToString() + "/" + puntajeScript.GetPuntajeMaximo();
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
