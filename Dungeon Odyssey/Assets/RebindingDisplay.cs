using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindingDisplay : MonoBehaviour
{
    [SerializeField] private PlayerInputActions inputActions;
    [SerializeField] private InputActionReference Jump;
    [SerializeField] private TMP_Text bindingDisplay;
    [SerializeField] private GameObject startRebindObject;
    [SerializeField] private GameObject waitingForInputObject;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    private void Start()
    {
       
    }
    public void StartRebind()
    {
        startRebindObject.SetActive(false);
        waitingForInputObject.SetActive(true);

        rebindingOperation = Jump.action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete())
            .Start();

    }

    private void RebindComplete()
    {
        rebindingOperation.Dispose();

        startRebindObject.SetActive(true);
        waitingForInputObject.SetActive(false);
    }
}