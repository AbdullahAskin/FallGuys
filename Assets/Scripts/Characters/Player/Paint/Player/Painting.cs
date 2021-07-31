using UnityEngine;

public class Painting : TexturePainting
{
    private TextureControl _texContScript;
    public GameObject brushCursor, brushContainer;
    private ColorPicker _colorPicker;

    public void Initialize()
    {
        _texContScript = GetComponent<TextureControl>();
        _colorPicker = GameObject.Find("ColorPercentages").GetComponentInParent<ColorPicker>();
        brushSpriteRen = Resources.Load<SpriteRenderer>("Paint/Brush/SolidBrushStroke");
        _texContScript = GameObject.FindGameObjectWithTag("paintableObject").GetComponent<TextureControl>();
        StartCoroutine(_texContScript.SaveTexture(brushCursor, brushContainer, _colorPicker));
    }

    public void Paint(Vector3 target)
    {
        if (target.magnitude == 0 || _texContScript.saving)
        {
            brushCursor.SetActive(false);
            return;
        }

        PaintTexture(target, brushCursor, brushContainer, _colorPicker._brushSizeSlider.value,
            _colorPicker._brushColor);
    }
}