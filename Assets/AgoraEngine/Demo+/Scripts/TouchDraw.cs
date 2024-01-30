using UnityEngine;
using UnityEngine.UI;

public class TouchDraw : MonoBehaviour
{
    public RawImage remoteViewRawImage;
    public RawImage clientViewRawImage;
    public Button markupButton;
    public Text markupButtonText;

    public int textureWidth = 512;
    public int textureHeight = 512;
    public Color brushColor = Color.black;
    public int brushSize = 5;

    private Texture2D _remoteTexture;
    private Texture2D _clientTexture;

    public Texture2D RemoteTexture
    {
        get { return _remoteTexture; }
    }

    public Texture2D ClientTexture
    {
        get { return _clientTexture; }
    }

    private bool canDraw = false;

    private void Start()
    {
        _remoteTexture = new Texture2D(textureWidth, textureHeight);
        _clientTexture = new Texture2D(textureWidth, textureHeight);

        ClearTextures(_remoteTexture);
        ClearTextures(_clientTexture);

        remoteViewRawImage.texture = _remoteTexture;
        clientViewRawImage.texture = _clientTexture;

        if (markupButton != null)
        {
            markupButton.onClick.AddListener(ToggleDrawing);
        }
    }

    public void ToggleDrawing()
    {
        canDraw = !canDraw;

        if (markupButtonText != null)
        {
            markupButtonText.text = canDraw ? "Stop" : "Markup";
        }
    }

    public void ClearTextures(Texture2D texture)
    {
        Color[] colors = new Color[textureWidth * textureHeight];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.clear;
        }
        texture.SetPixels(colors);
        texture.Apply();
    }

    private void FixedUpdate()
    {
        if (canDraw)
        {
            Vector2 mousePosition = Input.mousePosition;
            DrawOnTexture(_remoteTexture, mousePosition);
            DrawOnTexture(_clientTexture, mousePosition);
        }
    }

   private void DrawOnTexture(Texture2D texture, Vector2 position)
{
    if (canDraw)
    {
        RectTransform rectTransform = remoteViewRawImage.rectTransform; // Use the RectTransform of your RawImage
        Vector2 localPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, position, null, out localPosition))
        {
            int x = (int)((localPosition.x + rectTransform.rect.width / 2) / rectTransform.rect.width * texture.width);
            int y = (int)((localPosition.y + rectTransform.rect.height / 2) / rectTransform.rect.height * texture.height);

            for (int i = x - brushSize; i < x + brushSize; i++)
            {
                for (int j = y - brushSize; j < y + brushSize; j++)
                {
                    if (i >= 0 && i < texture.width && j >= 0 && j < texture.height)
                    {
                        texture.SetPixel(i, j, brushColor);
                    }
                }
            }

            texture.Apply();
        }
    }
}

}
