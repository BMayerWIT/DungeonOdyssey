using UnityEngine;

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
}