using System;
using UnityEngine;

public class Painting : TexturePainting
{
    private TextureControl _textureControlScr;
    public GameObject brushCursor, brushContainer;
    public ColorPicker _colorPicker;

    private void Start()
    {
        _textureControlScr = GetComponent<TextureControl>();
        _textureControlScr = GameObject.FindGameObjectWithTag("paintableObject").GetComponent<TextureControl>();
    }

    public void Initialize()
    {
        StartCoroutine(_textureControlScr.SaveTexture(brushCursor, brushContainer, _colorPicker));
    }

    public void Paint(Vector3 target)
    {
        if (target.magnitude == 0 || _textureControlScr.saving)
        {
            brushCursor.SetActive(false);
            return;
        }

        PaintTexture(target, brushCursor, brushContainer, _colorPicker.brushSizeSlider.value,
            _colorPicker.brushColor);
    }
}