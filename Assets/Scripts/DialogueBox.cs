using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Kinetix;
using System.Collections;

public class DialogueBox : MonoBehaviour
{
    public UnityEvent DialogueClosed;
    public UnityEvent ObjectiveCompleted;

    public bool IsShown => gameObject.activeSelf;

    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private float delayBeforeDialogue = 1.0f;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button[] choiceButtons;

    private DialogueSO currentDialogue;

    /// <summary>
    /// Shows the dialogue box with the correct name, text and choices 
    /// </summary>
    /// <param name="dialogue"></param>
    public void ShowDialogue(DialogueSO dialogue)
    {
        if (!dialogue)
        {
            Debug.LogError("Can't show a null Dialogue!");
            return;
        }

        // Hide the dialogue box
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;

        currentDialogue = dialogue;
        nameText.text = dialogue.speakerName;
        dialogueText.text = dialogue.text;

        // Play the contextual emote of this dialogue
        if (!string.IsNullOrWhiteSpace(dialogue.contextualEmoteName))
        {
            KinetixCore.Context.PlayContext(dialogue.contextualEmoteName);
        }

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            // Hide any unused choice button
            if (i > dialogue.choices.Length - 1)
            {
                choiceButtons[i].gameObject.SetActive(false);
                continue;
            }

            // Set the text and show the choice button
            choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = $"{i + 1}. {dialogue.choices[i].text}";
            choiceButtons[i].gameObject.SetActive(true);
        }

        // Set the end of the demo
        if (dialogue.completesObjective)
        {
            // Mark the objective as complete when the player closed the dialogue box
            DialogueClosed.AddListener(() => ObjectiveCompleted.Invoke());
        }

        // Activate the dialogue box
        gameObject.SetActive(true);

        StartCoroutine(WaitAndShowBox(delayBeforeDialogue));
    }

    private IEnumerator WaitAndShowBox(float delayInSeconds)
    {
        yield return new WaitForSecondsRealtime(delayInSeconds);

        // Show the dialogue box
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
    }

    /// <summary>
    /// When the player selected a choice
    /// </summary>
    /// <param name="index">index of the choice</param>
    public void SelectedChoice(int index)
    {
        DialogueSO.DialogueChoice choice = currentDialogue.choices[index];
        if (!choice.dialogue)
        {
            Close();
            return;
        }

        ShowDialogue(currentDialogue.choices[index].dialogue);
    }

    /// <summary>
    /// Close the dialogue box
    /// </summary>
    public void Close()
    {
        currentDialogue = null;
        DialogueClosed.Invoke();
        gameObject.SetActive(false);
    }
}
