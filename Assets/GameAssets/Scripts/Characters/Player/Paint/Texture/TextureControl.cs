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

    public IEnumerator SaveTexture(GameObject brushCursor, GameObject brushContainer, IChangeableText changeableText)
    {
        saving = true;
        PaintPointerOff(brushCursor);
        yield return new WaitForSeconds(.2f);
        Save(brushContainer);
        TextureColorRates(changeableText);
        saving = false;
        yield return new WaitForSeconds(4f);
        StartCoroutine(SaveTexture(brushCursor, brushContainer, changeableText));
    }

    private void Save(GameObject brushContainer)
    {
        _baseMaterial.mainTexture = RtToTex2DConverter();
        foreach (Transform child in brushContainer.transform)
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

    private void TextureColorRates(IChangeableText changeableTextScr)
    {
        Color[] pixels = RtToTex2DConverter().GetPixels();
        float redCounter = 0, greenCounter = 0, blueCounter = 0;
        CalculateColorPercentages(pixels, ref redCounter, ref greenCounter, ref blueCounter);
        changeableTextScr.ChangeTexts(redCounter, greenCounter, blueCounter);
    }

    private static void CalculateColorPercentages(Color[] pixelColors, ref float redCounter, ref float greenCounter,
        ref float blueCounter)
    {
        foreach (var pixelColor in pixelColors)
        {
            if (pixelColor.a < .5)
                continue;
            if (!(pixelColor.r > 0) && !(pixelColor.g > 0) && !(pixelColor.b > 0)) continue;
            if (pixelColor.r > pixelColor.g)
            {
                if (pixelColor.r > pixelColor.b)
                    redCounter += pixelColor.r * pixelColor.a;
                else
                    blueCounter += pixelColor.b * pixelColor.a;
            }
            else if (pixelColor.g > pixelColor.b)
                greenCounter += pixelColor.g * pixelColor.a;
            else if (pixelColor.b > pixelColor.g)
                blueCounter += pixelColor.b * pixelColor.a;
        }

        redCounter = Convert.ToSingle(Math.Round(redCounter * 100 / pixelColors.Length, 1));
        greenCounter = Convert.ToSingle(Math.Round(greenCounter * 100 / pixelColors.Length, 1));
        blueCounter = Convert.ToSingle(Math.Round(blueCounter * 100 / pixelColors.Length, 1));
    }

    private static void PaintPointerOff(GameObject _brushCursor)
    {
        _brushCursor.SetActive(false);
    }
}