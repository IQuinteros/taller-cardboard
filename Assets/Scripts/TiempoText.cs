using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TiempoText : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        PuntajeScript puntajeScript = mainCamera.GetComponent<PuntajeScript>();
        // Bind to tiempoActual changes to update this text
        puntajeScript.tiempoActualChanged += (int tiempoActual) =>
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = tiempoActual.ToString();
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
