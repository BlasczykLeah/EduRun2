// this class is static
// the functions are static
// so we can call these methods everywhere

using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoad
{
    public static void SaveQuestionSet(List<Question_NH> questionSet)
    {
        // Set up to write a file
        var bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/questionData.txt", FileMode.OpenOrCreate);  // open or create is a must
        // save the data to the file
        bf.Serialize(file, questionSet);
        file.Close();
        Debug.Log("File Saved..");
    }

    public static List<Question_NH> LoadQuestionSet()
    {
        Debug.Log("loading save data");
        if (File.Exists(Application.persistentDataPath + "/questionData.txt"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/questionData.txt", FileMode.Open);
            List<Question_NH> questions = (List<Question_NH>)bf.Deserialize(file);

            file.Close();

            return questions;
        }
        else
        {
            Debug.Log("No prior saved PlayerData");
            var questions = new List<Question_NH>();
            return questions;
            //return null;
        }
    }

    public static void SavePlayerData(PlayerData data)
    {
        // Set up to write a file
        var bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/saveData.txt", FileMode.OpenOrCreate);  // open or create is a must
        // save the data to the file
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("File Saved..");
    }

    public static PlayerData LoadPlayerData()
    {
        Debug.Log("loading save data");
        if (File.Exists(Application.persistentDataPath + "/saveData.txt"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveData.txt", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);

            file.Close();

            return data;
        }
        else
        {
            Debug.Log("No prior saved PlayerData");
            var data = new PlayerData();
            data.ResetData(); // there should a function/interface on PlayerData 
            return data;
            //return null;
        }
    }
}
