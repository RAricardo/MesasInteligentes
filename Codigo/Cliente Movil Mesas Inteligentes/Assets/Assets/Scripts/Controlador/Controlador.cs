using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour {

    [SerializeField]
    private Modelo modelo;
    [SerializeField]
    private Vista vista;
    [SerializeField]
    private WebClient webclient;

    internal void mostrarVistaMesaIndividual(string id) {
        vista.vistaListaDeMesas.gameObject.SetActive(false);
        vista.vistaMesaIndividual.inicializarVista(id);
        vista.vistaMesaIndividual.gameObject.SetActive(true);
    }

    //Obtener Datos de Modelo
    public string[,] obtenerMesas() {
        return modelo.MatrizDeMesas;
    }

    public void establecerTiempo(string id, string tiempo) {
        modelo.establecerTiempo(id, tiempo);
    }

    public string obtenerEstado(string id) {
        return modelo.obtenerEstado(id);
    }

    public string obtenerTiempo(string id) {
        return modelo.obtenerTiempo(id);
    }

    public void enviarMesas() {
        webclient.enviarMatriz();
    }
}
