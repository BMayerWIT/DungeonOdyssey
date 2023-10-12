using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Custom/Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public float attackDistance;
    public int attackDamage;
    public float attackSpeed;
    public float attackDelay;
    public Vector3 cameraOffsetPosition;
    public LayerMask attackLayer;
}
