using UnityEngine;
using UnityEngine.UI;

public class VistaListaMesas : ClientElement {

    public GameObject MesaPrefab;
    public VerticalLayoutGroup lista;
    private float refreshTime = 3.0f;

    public void actualizarMesas() {
        foreach (Transform child in lista.transform) {
            GameObject.Destroy(child.gameObject);
        }
        string[,] mesas = controlador.obtenerMesas();
        for (int i = 0; i < mesas.GetLength(0); i++) {
            GameObject nuevaMesa = Instantiate(MesaPrefab);
            string id = mesas[i, 0];
            nuevaMesa.GetComponentInChildren<Text>().text = id;
            nuevaMesa.GetComponentInChildren<botonCambiar>().setID(id);
            nuevaMesa.GetComponent<botonDisponibilidad>().establecerDisponibilidad(mesas[i,1]);
            nuevaMesa.transform.SetParent(lista.transform, false);
        }
    }

    void OnEnable() {
        actualizarMesas();
    }

    void Update() {
        if (refreshTime > 0) {
            refreshTime -= Time.deltaTime;
        }
        else {
            refreshTime = 5.0f;
            actualizarMesas();
        }
    }
}
