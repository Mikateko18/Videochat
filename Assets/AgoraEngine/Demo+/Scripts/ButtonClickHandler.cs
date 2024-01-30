using UnityEngine;
using UnityEngine.UI;

public class ButtonClickHandler : MonoBehaviour
{
    public Button markupButton; // Assign your MarkupButton to this field in the Inspector
    public TouchDraw touchDrawScript; // Assign your TouchDraw script to this field in the Inspector

    private void Start()
    {
        if (markupButton != null)
        {
            markupButton.onClick.AddListener(ToggleDrawing);
        }
    }

    private void ToggleDrawing()
    {
        if (touchDrawScript != null)
        {
            touchDrawScript.ToggleDrawing();
        }
    }
}
