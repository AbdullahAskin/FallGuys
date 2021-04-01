using UnityEngine;

public class Painting : TexturePainting
{
    private TextureControl _texContScript;
    public GameObject _brushCursor, _brushContainer;
    private ColorPicker _colorPicker;

    public void Initialize()
    {
        _texContScript = GetComponent<TextureControl>();
        _colorPicker = GameObject.Find("ColorPercentages").GetComponentInParent<ColorPicker>();
        _brushStroke = Resources.Load<GameObject>("Paint/Brush/SolidBrushStroke");
        _texContScript = GameObject.FindGameObjectWithTag("paintableObject").GetComponent<TextureControl>();
        StartCoroutine(_texContScript.SaveTexture(_brushCursor, _brushContainer, _colorPicker));
    }

    public void Paint(Vector3 target)
    {
        if (target.magnitude == 0 || _texContScript.saving)
        {
            _brushCursor.SetActive(false);
            return;
        }
        PaintTexture(target, _brushCursor, _brushContainer, _colorPicker._brushSizeSlider.value, _colorPicker._brushColor);
    }

}
