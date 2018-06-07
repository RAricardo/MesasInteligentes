using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class botonDisponibilidad : MonoBehaviour {

    public Text id;
    public Button boton;

    public void establecerDisponibilidad(string disponibilidad) {
        switch (disponibilidad) {
            case "disponible":
                boton.GetComponent<Image>().color = new Color(0.1933962f, 1, 0.3185079f);
                boton.GetComponentInChildren<Text>().text = "Disponible";
                break;
            case "ocupada":
                boton.GetComponent<Image>().color = new Color(1, 0.116458f, 0.049f);
                boton.GetComponentInChildren<Text>().text = "Ocupada";
                break;
            case "reservada":
                boton.GetComponent<Image>().color = new Color(1, 0.7099202f, 0.04705882f);
                boton.GetComponentInChildren<Text>().text = "Reservada";
                break;
        }
    }
}
