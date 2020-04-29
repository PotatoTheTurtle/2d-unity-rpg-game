using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{

    public GameObject pausepanel;

    [Header("Objects to save/delete")]
    public List<ScriptableObject> objects = new List<ScriptableObject>();

    [Header("Reset chest runtime values")]
    public List<BoolValue> chests = new List<BoolValue>();

    [Header("Reset wait screen")]
    public float fadeWait;
    public GameObject panel;

    public void ResetScriptables()
    {
        Debug.Log(Application.persistentDataPath);
        for(int i = 0; i < objects.Count; i ++)
        {
            if(File.Exists(Application.persistentDataPath +
                string.Format("/{0}.dat", i)))
            {
                File.Delete(Application.persistentDataPath +
                    string.Format("/{0}.dat", i));
                Debug.Log("DELETE");
                Debug.Log(Application.persistentDataPath);
            }
        }

        for (int i = 0; i < chests.Count; i++)
        {
            chests[i].RuntimeValue = false;
        }
        pausepanel.SetActive(false);
        Time.timeScale = 1f;
        StartCoroutine(FadeCo());
    }

    public IEnumerator FadeCo()
    {
        if (panel != null)
        {
            Instantiate(panel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("SampleScene");
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    private void OnEnable()
    {
        LoadScriptables();
    }

    private void OnDisable()
    {
        SaveScriptables();
    }

    public void SaveScriptables()
    {
        for (int i = 0; i < objects.Count; i ++)
        {
            FileStream file = File.Create(Application.persistentDataPath +
                string.Format("/{0}.dat", i));
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(objects[i]);
            binary.Serialize(file, json);
            file.Close();
        }
    }

    public void LoadScriptables()
    { 
        for(int i = 0; i < objects.Count; i ++)
        { 
            if(File.Exists(Application.persistentDataPath +
                string.Format("/{0}.dat", i)))
            {
                FileStream file = File.Open(Application.persistentDataPath +
                    string.Format("/{0}.dat", i), FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file),
                    objects[i]);
                file.Close();
            }
        }

    }

}
