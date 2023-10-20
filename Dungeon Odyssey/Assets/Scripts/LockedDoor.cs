using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class LockedDoor : MonoBehaviour
{
    private const string LOCKED_DOOR = "LockedDoor";
    private GameObject doorLockedText;
    private GameObject interactText;
    private GameInput gameInput;
    private GameObject gameInputObject;
    private GameObject playerObject;
    private Player player;
    private GameObject interactCanvas;
    private TextHandlers textHandler;

    // Start is called before the first frame update
    void Start()
    {

        interactCanvas = GameObject.Find("InteractableText");
        textHandler = interactCanvas.GetComponent<TextHandlers>();
        doorLockedText = textHandler.GetDoorText();
        interactText = textHandler.GetFToOpenText();
        
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();

        gameInputObject = GameObject.Find("GameInput");
        gameInput = gameInputObject.GetComponent<GameInput>();

    }

    // Update is called once per frame
    void Update()
    {
        CheckDoorIsLocked();  
    }

    private void CheckDoorIsLocked()
    {
        RaycastHit hit = player.InteractableCheck();
        if (hit.collider != null)
        {
            int hitLayer = hit.collider.gameObject.layer;
            string layerName = LayerMask.LayerToName(hitLayer);

            if (layerName == LOCKED_DOOR)
            {
                
                interactText.SetActive(true);
                if (gameInput.Interacting())
                {
                    doorLockedText.SetActive(true);
                    Invoke(nameof(DoorIsLocked), 5);
                }
            }
            else
            {
                interactText.SetActive(false);
                doorLockedText.SetActive(false);
            }
        }
    }

    private void DoorIsLocked()
    {
        doorLockedText.SetActive(false);
    }

}
