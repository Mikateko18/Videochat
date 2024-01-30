using UnityEngine;
using UnityEngine.UI;

public class ClearButton : MonoBehaviour
{
    public TouchDraw touchDrawScript; // Reference to the TouchDraw script

    public void ClearDrawings()
    {
        if (touchDrawScript != null)
        {
            touchDrawScript.ClearTextures(touchDrawScript.RemoteTexture);
            touchDrawScript.ClearTextures(touchDrawScript.ClientTexture);
        }
    }
}
