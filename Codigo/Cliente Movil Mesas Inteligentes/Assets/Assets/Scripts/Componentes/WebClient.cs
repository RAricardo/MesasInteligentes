using System;
using System.IO;
using System.Net.Sockets;
using Boomlagoon.JSON;
using UnityEngine;

public class WebClient : ClientElement {
    public string ip = "172.20.10.3";
    public int port = 5005;
    private float refreshTime = 1.0f;

    public Mensajes mensajes;

    internal Boolean socket_ready = false;

    TcpClient tcp_socket;
    NetworkStream net_stream;

    StreamWriter socket_writer;
    StreamReader socket_reader;

    void OnApplicationQuit() {
        closeSocket();
    }

    public void setupSocket() {
        try {
            tcp_socket = new TcpClient(ip, port);
            net_stream = tcp_socket.GetStream();
            socket_writer = new StreamWriter(net_stream);
            socket_reader = new StreamReader(net_stream);

            socket_ready = true;
        }
        catch (Exception e) {
                if (UnityEditor.EditorApplication.isPlaying) {
                    UnityEditor.EditorApplication.isPlaying = false;
                } else {
                    Application.Quit();
                }
            Debug.Log("Socket error: " + e);
            Application.Quit();
            }
    }

    public void writeSocket(string line) {
        if (!socket_ready)
            return;

        line = line + "\r\n";
        socket_writer.Write(line);
        socket_writer.Flush();
    }

    public String readSocket() {
        if (!socket_ready) {
            return "";
        }

        //if (net_stream.DataAvailable) {
        string inp = socket_reader.ReadLine();
        //inp = socket_reader.ReadLine();
        if (inp == null) {
            return "";
        }
        return inp;
        //}
        //return "";
    }

    public void closeSocket() {
        if (!socket_ready)
            return;

        socket_writer.Close();
        socket_reader.Close();
        tcp_socket.Close();
        socket_ready = false;
    }

    public void enviarMatriz() {
        JSONObject jsonO = mensajes.enviarMatriz();
        string json = jsonO.ToString();
        Debug.Log(json);
        setupSocket();
        writeSocket(json);
        string dataIn = readSocket();
        closeSocket();
    }

    void Update() {
        if (refreshTime > 0) {
            refreshTime -= Time.deltaTime;
        }
        else {
            refreshTime = 1f;
            recibirMatriz();
        }
    }

    public void recibirMatriz() {
        JSONObject jsonO = mensajes.pedirMatriz();
        string json = jsonO.ToString();
        setupSocket();
        writeSocket(json);
        string dataIn = readSocket();
        closeSocket();
        if (dataIn != "" || dataIn == null) {
            JSONObject matriz = JSONObject.Parse(dataIn);
            try {
                for (int i = 0; i < controlador.obtenerMesas().GetLength(0); i++) {
                    string idMesa = (i + 1).ToString();
                    JSONArray array = matriz.GetArray("mesa" + idMesa);
                    controlador.obtenerMesas()[i, 0] = array[0].Str;
                    controlador.obtenerMesas()[i, 1] = array[1].Str;
                    controlador.obtenerMesas()[i, 2] = array[2].Str;
                }
            } catch (Exception e) {
                return;
            }
        }
    }
}
