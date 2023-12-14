using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class SkillButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    
    public Skill skill;
    public Image icon;
    private ColorBlock colours;
    private Button button;
    public Image bar;
    public bool hasBar;
    public bool isUnlockable;
    public List<Skill> requiredSkills = new List<Skill>();

    private void Start()
    {
        icon = GetComponent<Image>();
        icon.sprite = skill.skillIcon;
        colours = GetComponent<Button>().colors;
        button = GetComponent<Button>();
    }

    private void Update()
    {
        CheckIfUnlocked();
        CheckIfUnlockable();
    }
    public void OnSelect(BaseEventData eventData)
    {
        SkillInfoManager.instance.currentSkill = skill;
        
    }

    public void OnDeselect(BaseEventData eventData)
    {
        // Check if the pointer is over the separate panel
        if (!IsPointerOverSkillTreeInfoWindow())
        {
            SkillInfoManager.instance.currentSkill = null;
        }
    }

    private bool IsPointerOverSkillTreeInfoWindow()
    {
        // Check if the pointer is over a UI element
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Get the pointer data
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;

            // Raycast to check if the pointer is over the SkillTreeInfoWindow panel
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            // Check each result to see if it's the SkillTreeInfoWindow panel
            foreach (RaycastResult result in results)
            {
                
                if (result.gameObject.tag == "EquipButton") // Replace with the actual name or tag of your panel
                {
                    return true; // The pointer is over the SkillTreeInfoWindow panel
                }
            }
        }

        return false; // The pointer is not over the SkillTreeInfoWindow panel
    }

    private void CheckIfUnlocked()
    {
        if (skill.isUnlocked)
        {
            if (hasBar)
            {
                Color color = new Color(0f, 0.89f, 1f, 0.5f); 
                bar.color = color;
            }
            
            colours.normalColor = Color.white;
        }
        else if (!skill.isUnlocked) 
        {
            if (hasBar)
            {
                Color color = new Color(1f, 1f, 1f, 0.5f);
                bar.color = color;
            }
            
            colours.normalColor = Color.gray;
        }

        button.colors = colours;
    }

    private void CheckIfUnlockable()
    {
        if (requiredSkills.Count > 0)
        {
            int count = 0;
            for (int i = 0; i < requiredSkills.Count; i++)
            {
                if (requiredSkills[i].isUnlocked)
                {
                    count++;
                    if (count == requiredSkills.Count)
                    {
                        skill.isUnlockable = true;
                    }
                }
            }
        }
        else
        {
            skill.isUnlockable = true;
        }
        
    }
}
