using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class InteractableLever : MonoBehaviour
{
    [SerializeField] private GameObject objectToUpdate;
    [SerializeField] private bool isUnlocked;
    

    private Animator animator;
    private Animator doorAnimator;
    private GameObject playerObject;
    private Player player;
    private GameObject interactText;
    private GameObject interactCanvas;
    private TextHandlers textHandler;



    private const string LEVER = "Interactable";



    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();

        interactCanvas = GameObject.Find("InteractableText");
        textHandler = interactCanvas.GetComponent<TextHandlers>();
        interactText = textHandler.GetFText();

        animator = GetComponent<Animator>();
        doorAnimator = objectToUpdate.GetComponent<Animator>();
    }

    private void Update()
    {
        Interact();
    }

    public void Interact()
    {
        RaycastHit hit = player.InteractableCheck();
        if (hit.collider != null)
        {
            int hitLayer = hit.collider.gameObject.layer;
            string layerName = LayerMask.LayerToName(hitLayer);

            if (layerName == LEVER)
            {
                interactText.SetActive(true);
                if (GameInput.inputInstance.Interacting())
                {
                    LeverFlick();
                    
                }
            }
            else
            {
                interactText.SetActive(false);
            }
        }
    }

    private void LeverFlick()
    {
        animator.SetBool("leverFlicked", true);
        doorAnimator.SetBool("openDoor", true);
    }


    public void CheckDoorUnlocked()
    {
        if (isUnlocked)
        {
            // Open door
        }
        else if (!isUnlocked)
        {
            //TODO
            // if has key unlock door and play animation


        }
    }
}
//if (hit.transform.TryGetComponent<InteractableLever>(out InteractableLever lever))
//{
//    lever.Interact();
//}