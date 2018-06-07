#include <SPI.h>
#include <WiFi101.h>
#include <ArduinoJson.h>
#include <Printers.h>
#include <XBee.h>
String caracter;
String mensaje;
XBee xbee = XBee();
int periodo = 100;
unsigned long actual = 0;
char ssid[] = "Ricardo Azopardo’s iPhone";
char pass[] = "12345678";

WiFiServer server(5005);
IPAddress myAddress;
int status = WL_IDLE_STATUS;
DynamicJsonBuffer jsonBuffer;

String mesas [2][3] = {
 {"mesa1","ocupada","0"},
 {"mesa2","disponible","0"}
};

String parseJSON(String mensaje){
  JsonObject& root = jsonBuffer.parseObject(mensaje);
  int codigo = root.get<unsigned int>("codigo");
  switch (codigo){
    case 0:
      for (int i = 0; i<2;i++){
        String idMesa = "mesa";
        String numeroMesa = String(i+1);
        idMesa = idMesa + numeroMesa;
        JsonArray& jArray = root.get<JsonArray>(idMesa);
        String reservado = jArray.get<String>(0);
        if(reservado.equals("reservada")){
          mesas[i][1] = reservado;
          mesas[i][2] = jArray.get<String>(1);
        }
      }
      delay(500);
      return "";
    case 1:
      JsonObject& nuevo = jsonBuffer.createObject();
      for (int i = 0; i<2;i++){ 
        JsonArray& jNArray = jsonBuffer.createArray();
        String idMesa = "mesa";
        String numeroMesa = String(i+1);
        idMesa = idMesa + numeroMesa;

        jNArray.add(666);
        jNArray.add(666);
        jNArray.add(666);
        
        jNArray.set(0, mesas[i][0]);
        jNArray.set(1, mesas[i][1]);
        jNArray.set(2, mesas[i][2]);
        nuevo.set(idMesa, jNArray);
      }
      String output;
      nuevo.printTo(output);
      nuevo.printTo(Serial);
      delay(500);
      return output;
  }
}

void leer(){
  if (Serial1.available() >= 22) {
    if(Serial1.read() == 0x7E){
      for(int i = 0; i < 15; i++){
        byte trash = Serial1.read();
      }  
      mensaje = "";
        do{
          byte dara_recive = (byte) Serial1.read();
          if(dara_recive == 0xFF)break;
          char dara_recive_b = dara_recive;
          mensaje = mensaje + String(dara_recive_b);
        }while(true);

        //Serial.println(mensaje);
        
        //Mesa1
        if( mensaje[0]=='1' ){
          if( mensaje[1]=='o' ){
            mesas[0][1]="ocupada";
          }
          if( mensaje[1]=='d' ){
            mesas[0][1]="disponible";
          }
        }
        //Mesa2
        if( mensaje[0]=='2' ){
          if( mensaje[1]=='o' ){
            mesas[1][1]="ocupada";
          }
          if( mensaje[1]=='d' ){
            mesas[1][1]="disponible";
          }
        }
        //Serial.println(mensaje);
        Serial.print(mesas[0][0]);
        Serial.println(mesas[0][1]);
        Serial.print(mesas[1][0]);
        Serial.println(mesas[1][1]);
    }
  }
}

void setup() {
  Serial.begin(9600);
  Serial1.begin(9600);
  xbee.setSerial(Serial1);
  while (!Serial) {
    ; // wait for serial port to connect. Needed for native USB port only
  }

  // check for the presence of the shield:
  if (WiFi.status() == WL_NO_SHIELD) {
    Serial.println("WiFi shield not present");
    // don't continue:
    while (true);
  }

  // attempt to connect to WiFi network:
  while (status != WL_CONNECTED) {
    Serial.print("Attempting to connect to SSID: ");
    Serial.println(ssid);
    // Connect to WPA/WPA2 network. Change this line if using open or WEP network:
    status = WiFi.begin(ssid, pass);

    // wait 10 seconds for connection:
    delay(5000);
  }
  server.begin();
  Serial.print("Connected to wifi. My address:");
  myAddress = WiFi.localIP();
  Serial.println(myAddress);
}

void loop() {
  if (millis()>actual+periodo){
    actual = millis();
    leer();
  }
  char* data;
  char c;
  long length;
  // listen for incoming clients
  WiFiClient client = server.available();
  if (client) {
    while (client.connected()) {
      Serial.println("Recibiendo: ");
        if (client.available()) {
         
         c = client.read();
         length = c & 0x7f;
         // allocate enough memory
         data = (char*)malloc(length + 1);
         
         for (int i = 0; i < length; i++) {
           data[i] = client.read();
         }

         String mensaje(data);

         String definitivo = "{" + mensaje + "\n";
         client.write(parseJSON(definitivo).c_str());
    }

    // close the connection:
    client.stop();
    }
  }
}
