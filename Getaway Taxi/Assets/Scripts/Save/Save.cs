using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Save
{
    [Header("Save Paths")]
    public static string saveGameLoc = "/getAway1.save";//where the scrapBook settings get saved
    public static string saveSettingsLoc = "/getAwaySettings1.save";//where the settings get saved

    ///Game data
    public static void saveGameData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + saveGameLoc;//persistant path depends on the platform but for windows its here : %userprofile%\AppData\LocalLow\
        FileStream stream =  new FileStream(path,FileMode.Create);
        GameData data = new GameData();
        formatter.Serialize(stream,data);//converts the data to be encrypted
        stream.Close();
    }

    public static GameData loadGameData()//call on start so to load in the saved data
    {
        string path = Application.persistentDataPath + saveGameLoc;
        if(File.Exists(path))//if the file exist
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);
            GameData data = formatter.Deserialize(stream) as GameData;//decrypts the saved data
            stream.Close();
            return data;//returns the data from the saved BookData class
        }else{
            return null;          
        }
    }

    ///Setting data
    public static void saveSettingData(OptionController oData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + saveSettingsLoc;//persistant path depends on the platform but for windows its here : %userprofile%\AppData\LocalLow\
        FileStream stream =  new FileStream(path,FileMode.Create);
        SettingData data = new SettingData(oData);
        formatter.Serialize(stream,data);//converts the data to be encrypted
        stream.Close();
    }

    public static SettingData loadSettingData()//call on start so to load in the saved data
    {
        string path = Application.persistentDataPath + saveSettingsLoc;
        if(File.Exists(path))//if the file exist
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);
            SettingData data = formatter.Deserialize(stream) as SettingData;//decrypts the saved data
            stream.Close();
            return data;//returns the data from the saved BookData class
        }else{
            return null;          
        }
    }


}
