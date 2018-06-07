using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class botonCambiar : ClientElement {

    private string id;

    public void mostrarDatosHistoria() {
        string estado = GetComponentInChildren<Text>().text;
        controlador.mostrarVistaMesaIndividual(id);
    }

    public void setID(string id) {
        this.id = id;
    }
}
