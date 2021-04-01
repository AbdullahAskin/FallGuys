using System;
using System.Collections;
using UnityEngine;

public class TextureControl : MonoBehaviour
{
    private RenderTexture _canvasTexture;
    private Material _baseMaterial;
    [HideInInspector] public bool saving = false;

    private void Start()
    {
        _canvasTexture = Resources.Load<RenderTexture>("Paint/PaintableObject/CanvasTexture");
        _baseMaterial = Resources.Load<Material>("Paint/PaintableObject/BaseMaterial");
    }

    public IEnumerator SaveTexture(GameObject _brushCursor, GameObject _brushContainer, IChangeableText _changeableText)
    {
        saving = true;
        PaintPointerOff(_brushCursor);
        yield return new WaitForSeconds(.2f);
        Save(_brushContainer);
        TextureColorRates(_changeableText);
        saving = false;
        yield return new WaitForSeconds(4f);
        StartCoroutine(SaveTexture(_brushCursor, _brushContainer, _changeableText));
    }

    private void Save(GameObject _brushContainer)
    {
        _baseMaterial.mainTexture = RtToTex2DConverter();
        foreach (Transform child in _brushContainer.transform)
            Destroy(child.gameObject);
    }
    private Texture2D RtToTex2DConverter()
    {
        RenderTexture.active = _canvasTexture;
        Texture2D texture2D = new Texture2D(_canvasTexture.width, _canvasTexture.height, TextureFormat.RGB24, false);
        texture2D.ReadPixels(new Rect(0, 0, _canvasTexture.width, _canvasTexture.height), 0, 0);
        texture2D.Apply();
        RenderTexture.active = null;
        return texture2D;
    }

    private void TextureColorRates(IChangeableText _changeableText)
    {
        Color[] pixels = RtToTex2DConverter().GetPixels();
        float redCounter = 0, greenCounter = 0, blueCounter = 0;
        ColorCalculater(pixels, ref redCounter, ref greenCounter, ref blueCounter);
        _changeableText.ChangeTexts(redCounter, greenCounter, blueCounter);
    }

    void ColorCalculater(Color[] pixels, ref float redCounter, ref float greenCounter, ref float blueCounter)
    {
        foreach (Color _color in pixels)
        {
            if (_color.a < .5)
                continue;
            if (_color.r > 0 || _color.g > 0 || _color.b > 0)
                if (_color.r > _color.g)
                {
                    if (_color.r > _color.b)
                        redCounter += _color.r * _color.a;
                    else
                        blueCounter += _color.b * _color.a;
                }
                else if (_color.g > _color.b)
                    greenCounter += _color.g * _color.a;
                else if (_color.b > _color.g)
                    blueCounter += _color.b * _color.a;
        }
        redCounter = Convert.ToSingle(Math.Round(redCounter * 100 / pixels.Length, 1));
        greenCounter = Convert.ToSingle(Math.Round(greenCounter * 100 / pixels.Length, 1));
        blueCounter = Convert.ToSingle(Math.Round(blueCounter * 100 / pixels.Length, 1));
    }

    private void PaintPointerOff(GameObject _brushCursor)
    {
        _brushCursor.SetActive(false);
    }

}
