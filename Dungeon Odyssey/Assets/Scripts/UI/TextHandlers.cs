using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHandlers : MonoBehaviour
{
    [SerializeField] private GameObject fToInteractText;
    [SerializeField] private GameObject doorIsLockedText;
    [SerializeField] private GameObject doorFToOpen;

    public GameObject GetFText() { return fToInteractText; }
    public GameObject GetDoorText() {  return doorIsLockedText; }
    public GameObject GetFToOpenText() { return doorFToOpen; }
}
