using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReiniciarEscena : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(Reiniciar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Reiniciar()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

}
