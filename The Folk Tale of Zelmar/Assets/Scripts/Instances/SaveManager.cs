using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class SaveManager : MonoBehaviour
{
    public SaveData activeSave;

    void Start()
    {

    }

    void Update()
    {

    }

  public void Save()
    {
        string dataPach = Application.persistentDataPath;

        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPach + "/" + activeSave.saveName + ".save", FileMode.Create);
        serializer.Serialize(stream, activeSave);
        stream.Close();
    }

    public void Load()
    {
        string dataPach = Application.persistentDataPath;

        if(System.IO.File.Exists(dataPach + "/" + activeSave.saveName + ".save"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPach + "/" + activeSave.saveName + ".save", FileMode.Open);
            activeSave = serializer.Deserialize(stream) as SaveData;
            stream.Close();
        }
    }
}


[System.Serializable]
public class SaveData
{
    [Header("Levels")]
    public bool in_Overworld;
    public bool in_SI_Hills;
    public bool in_SI;

    
   

    public string saveName;

    public bool key_Shesty_Open;
    public bool door_Open;

    public int small_Key_Count;
    
}
