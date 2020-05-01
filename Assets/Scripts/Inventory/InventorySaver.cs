using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class InventorySaver : MonoBehaviour
{
    [SerializeField] private PlayerInventory myInventory;
    [SerializeField] private ScriptableObject myInventoryObject;

    private void OnEnable()
    {
        Debug.Log("INVENTORYSAVER.CS ENABLED");
        myInventory.myInventory.Clear();
        LoadScriptables();
    }

    private void OnDisable()
    {
        SaveScriptables();
    }

    public void ResetScriptables()
    {
        myInventory.myInventory.Clear();
        int i = 0;
        while (File.Exists(Application.persistentDataPath +
            string.Format("/{0}.inv", i)))
        {
            File.Delete(Application.persistentDataPath +
                string.Format("/{0}.inv", i));
            i++;
        }
        
    }

    public void SaveScriptables()
    {
        FileStream file = File.Create(Application.persistentDataPath + "/inventory.inv");
        BinaryFormatter binary = new BinaryFormatter();
        var json = JsonUtility.ToJson(myInventoryObject);
        Debug.Log(myInventoryObject);
        binary.Serialize(file, json);
        file.Close();

        /*for (int i = 0; i < myInventory.myInventory.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath +
                string.Format("/{0}.inv", i));
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(myInventory.myInventory[i]);
            Debug.Log(myInventory.myInventory[i]);
            binary.Serialize(file, json);
            file.Close();
        }*/
    }

    public void LoadScriptables()
    {

        if (File.Exists(Application.persistentDataPath + "/inventory.inv"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/inventory.inv", FileMode.Open);
            BinaryFormatter binary = new BinaryFormatter();
            JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file),
                myInventoryObject);
            file.Close();
        }
        else
        {
            myInventory.myInventory.Clear();
        }

        /*int i = 0;
        while (File.Exists(Application.persistentDataPath +
            string.Format("/{0}.inv", i)))
        {
            var temp = ScriptableObject.CreateInstance<InventoryItem>();

            FileStream file = File.Open(Application.persistentDataPath +
                string.Format("/{0}.inv", i), FileMode.Open);

            BinaryFormatter binary = new BinaryFormatter();

            JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file),
                temp);

            file.Close();
            Debug.Log(JsonUtility.ToJson(temp));
            myInventory.myInventory.Add(temp);
            i++;
        }*/

    }
}
