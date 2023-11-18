using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    WeaponSlotManager weaponSlotManager;
    public WeaponItem[] weaponItems;
    public WeaponItem currentWeapon;
    
    
    int currentWeaponIndex = 0;
    Dictionary<string, WeaponItem> weaponDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
    }

    private void Start()
    {
        weaponDictionary = new Dictionary<string, WeaponItem>();

        for (int i = 0; i < weaponItems.Length; i++)
        {
            // Use the "weapon" + index (i) as the key.
            string weaponKey = "weapon" + i;

            // Add the weapon item to the dictionary with the key.
            weaponDictionary.Add(weaponKey, weaponItems[i]);

        }

        currentWeapon = weaponItems[0];
        weaponSlotManager.LoadWeaponOnSlot(currentWeapon, false);
       
    }

        

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Get the key for the current weapon using the index.
            string weaponKey = "weapon" + currentWeaponIndex;
            currentWeapon = weaponDictionary[weaponKey];
            // Load the weapon from the dictionary into the weapon slot manager.
            weaponSlotManager.LoadWeaponOnSlot(currentWeapon, false);

            // Increment the index and loop back to the start if needed.
            currentWeaponIndex = (currentWeaponIndex + 1) % weaponDictionary.Count;
        }
    }

}
