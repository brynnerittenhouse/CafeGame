using System;
using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using NUnit.Framework;
using TMPro;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class Npc : MonoBehaviour, IInteractable
{
    public string ID {get; private set;} 
    public NpcDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive, isDoneTalking;

    // Eventually can be an item that an object drops when interacted with
    //public GameObject itemPrefab;

    public GameObject interactionIcon;

    public Sprite interactedSprite;

    void Start()
    {
        interactionIcon.SetActive(false);
        ID ??= GlobalHelper.GenerateUniqueID(gameObject);
        isDoneTalking = false;
    }

    void Update()
    {
        
    }

    // check if we are interacting already
    public bool CanInteract()
    {
        return !isDialogueActive && !isDoneTalking;
    }

    public void Interact()
    {
        if (dialogueData == null || isDoneTalking)//|| PauseController.isPaused)
            return;

        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }

        //SetInteracted(true);
    }


    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        nameText.SetText(dialogueData.npcName);
        portraitImage.sprite = dialogueData.npcPortrait;

        dialoguePanel.SetActive(true);
        PauseController.Pause(true);

        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        Debug.Log("Next Line");
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");

        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        // Autoprogress certian lines after delay
        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    public void EndDialogue()
    {
        Debug.Log("End dialogue");
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
        PauseController.Pause(false);

        isDoneTalking = true;
    }

    public void DropItem()
    {
        //if (itemPrefab)
        //{
        //    GameObject droppedItem = Instantiate(itemPrefab, transform.position + Vector3.down, Quaternion.identity);
            // start item bounce animation
        //}
    }

    public void SetInteracted(bool interacted)
    {
        //if (HasInteracted = interacted)
        //{
        //    GetComponent<SpriteRenderer>().sprite = interactedSprite;
        //}
    }

    public void ShowInteractionIcon(bool showIcon)
    {
        interactionIcon.SetActive(showIcon);
    }
}
