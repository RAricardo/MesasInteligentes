using Boomlagoon.JSON;

public class Mensajes : ClientElement {

    public JSONObject enviarMatriz() {
        string[,] mesas = controlador.obtenerMesas();
        JSONObject jsonEnviar = new JSONObject();
        jsonEnviar.Add("codigo", 0);
        for (int i = 0; i < mesas.GetLength(0); i++) {
            JSONArray array = new JSONArray();
            array.Add(mesas[i, 1]);
            array.Add(mesas[i, 2]);
            jsonEnviar.Add("mesa" + (i+1), array);
        }
        return jsonEnviar;
    }

    public JSONObject pedirMatriz() {
        JSONObject jsonEnviar = new JSONObject();
        jsonEnviar.Add("codigo", 1);
        return jsonEnviar;
    }
}
