using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Modelo : ClientElement {

    private string[,] matrizDeMesas = new string[,] { {"Mesa 1","disponible","0"},
                                                      {"Mesa 2","ocupada","0"}};

    public string[,] MatrizDeMesas {
        get {
            return matrizDeMesas;
        }

        set {
            matrizDeMesas = value;
        }
    }

    public void descontarTiempos() {
        for (int i = 0; i < matrizDeMesas.GetLength(0); i++) {
            float nuevo = float.Parse(matrizDeMesas[i, 2], CultureInfo.InvariantCulture.NumberFormat);
            if (nuevo > 0) {
                nuevo -= Time.deltaTime;
                matrizDeMesas[i, 1] = "reservada";
                matrizDeMesas[i, 2] = nuevo.ToString();
            }
        }
    }

    public void establecerTiempo(string id, string tiempo) {
        for (int i = 0; i < matrizDeMesas.GetLength(0); i++) {
            if (matrizDeMesas[i, 0].Equals(id)) {
                matrizDeMesas[i, 2] = tiempo;
            }
        }
    }

    public string obtenerEstado(string id) {
        for (int i = 0; i < matrizDeMesas.GetLength(0); i++) {
            if (matrizDeMesas[i, 0].Equals(id)) {
                return matrizDeMesas[i, 1];
            }
        }
        return "";
    }

    public string obtenerTiempo(string id) {
        for (int i = 0; i < matrizDeMesas.GetLength(0); i++) {
            if (matrizDeMesas[i, 0].Equals(id)) {
                return matrizDeMesas[i, 2];
            }
        }
        return "0";
    }

    void Update() {
        descontarTiempos();    
    }
}

public class MesaVirtual {

    private string estado;
    private string id;
    private double tiempo;

    public MesaVirtual(string id) {
        this.Id = id;
        Estado = "disponible";
        Tiempo = 0f; 
    }

    public string Estado {
        get {
            return estado;
        }

        set {
            estado = value;
        }
    }

    public double Tiempo {
        get {
            return tiempo;
        }

        set {
            tiempo = value;
        }
    }

    public string Id {
        get {
            return id;
        }

        set {
            id = value;
        }
    }
}
