using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveAndLoad : MonoBehaviour
{
    public static SaveAndLoad instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public SkillDatabase skillSlotData;
    public Skill[] LoadedSkills;

    private void Start()
    {
        LoadedSkills = skillSlotData.savedSkills;
    }

    public static void SaveStats()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/levelStats.stats";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelStatsData data = new LevelStatsData();
        
        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static LevelStatsData LoadStats()
    {
        string path = Application.persistentDataPath + "/levelStats.stats";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelStatsData data = formatter.Deserialize(stream) as LevelStatsData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void ResetLevelAndSP()
    {
        LevelStatsData data = new LevelStatsData(0, 0, 0);
        
        
    }


    public void ResetMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}