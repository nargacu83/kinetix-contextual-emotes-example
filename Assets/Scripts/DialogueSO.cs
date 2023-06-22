using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Demo/Dialogue")]
public class DialogueSO : ScriptableObject
{
    [System.Serializable]
    public class DialogueChoice
    {
        /// <summary>
        /// Text of the choice
        /// </summary>
        public string text;

        /// <summary>
        /// Dialogue assigned to the choice, if null, it will close the UI.
        /// </summary>
        public DialogueSO dialogue;
    }

    /// <summary>
    /// Name of the character speaking
    /// </summary>
    public string speakerName;

    /// <summary>
    /// The text of what the character is saying
    /// </summary>
    [TextArea(3, 6)]
    public string text;

    /// <summary>
    /// Choices the player can make
    /// </summary>
    public DialogueChoice[] choices;

    [Header("Emote")]
    public string contextualEmoteName;

    [Header("Objective")]
    /// <summary>
    /// Does this choice complete the objective of the demo?
    /// </summary>
    public bool completesObjective;
}
