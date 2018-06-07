using UnityEngine;

public class ClientElement : MonoBehaviour {
    public Controlador controlador { get { return GameObject.FindObjectOfType<Controlador>(); } }
}
