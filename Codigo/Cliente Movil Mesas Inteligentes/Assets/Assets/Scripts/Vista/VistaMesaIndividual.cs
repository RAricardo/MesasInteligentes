using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VistaMesaIndividual : ClientElement {

    public Text id, tiempo;
    public Image estado;
    public InputField input;
    public Button volver, aplicar;

    public void inicializarVista(string id) {
        this.id.text = id;
        establecerEstado(controlador.obtenerEstado(id));
    }

    public void establecerEstado(string disponibilidad) {
        switch (disponibilidad) {
            case "disponible":
                estado.GetComponent<Image>().color = new Color(0.1933962f, 1, 0.3185079f);
                estado.GetComponentInChildren<Text>().text = "Disponible";
                break;
            case "ocupada":
                estado.GetComponent<Image>().color = new Color(1, 0.116458f, 0.049f);
                estado.GetComponentInChildren<Text>().text = "Ocupada";
                break;
            case "reservada":
                estado.GetComponent<Image>().color = new Color(1, 0.7099202f, 0.04705882f);
                estado.GetComponentInChildren<Text>().text = "Reservada";
                break;
        }
    }

    public void establecerTiempo() {
        controlador.establecerTiempo(id.text, input.text);
        controlador.enviarMesas();
    }

    void Update () {
        establecerEstado(controlador.obtenerEstado(id.text));
        tiempo.text = controlador.obtenerTiempo(id.text);
	}
}
