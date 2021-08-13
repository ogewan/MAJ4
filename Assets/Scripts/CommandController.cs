using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommandController : MonoBehaviour
{
    public TMP_InputField entry;
    public TextMeshProUGUI display;
    public Scrollbar displaySB;

    public void AddToChatOutput(string newText)
    {
        // Clear Input Field
        entry.text = string.Empty;

        string response = Console.instance.Process(newText);
        if (display != null)
        {
            // No special formatting for first entry
            // Add line feed before each subsequent entries
            display.text += $"{(display.text == string.Empty ? "" : "\n")}{response}";
        }

        // Keep Chat input field active
        entry.ActivateInputField();

        // Set the scrollbar to the bottom when next text is submitted.
        displaySB.value = 0;
    }
    private void OnEnable()
    {
        entry.onSubmit.AddListener(AddToChatOutput);
    }

    private void OnDisable()
    {
        entry.onSubmit.RemoveListener(AddToChatOutput);
    }
}