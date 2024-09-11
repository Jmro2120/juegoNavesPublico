using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.NetWorking;


public IEnumerator RegistrarUsuario(Usuario datosRegistro)
    {
        var registroJSON = JsonUtility.ToJson(datosRegistro);

        var solicitud = new UnityWebRequest();
        solicitud.url = "http://localhost:3000/api/jugador/registrar";

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(registroJSON);
        solicitud.uploadHandler = new UploadHandlerRaw(bodyRaw);
        solicitud.downloadHandler = new DownloadHandlerBuffer();
        solicitud.method = UnityWebRequest.kHttpVerbPOST;
        solicitud.SetRequestHeader("Content-Type", "application/json");
        
        solicitud.timeout = 10;

        yield return solicitud.SendWebRequest();

        if (solicitud.result == UnityWebRequest.Result.ConnectionError)
        {
            respuestaRegistro.mensaje = "Conexion fallida";
        }
        else
        {           
            respuestaRegistro = (RespuestaRegistro)JsonUtility.FromJson(solicitud.downloadHandler.text, typeof(RespuestaRegistro));
        }
        solicitud.Dispose();
    }
