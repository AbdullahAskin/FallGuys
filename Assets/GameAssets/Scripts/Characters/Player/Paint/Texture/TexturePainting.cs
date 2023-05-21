using UnityEngine;

public class TexturePainting : MonoBehaviour
{
    public SpriteRenderer brushSpriteRen;

    protected void PaintTexture(Vector3 target, GameObject brushCursorGo, GameObject brushContainerGo, float brushSize,
        Color brushColor) // Brush color ,size buraya disaridan gonderilicek.
    {
        SetBrushProperties(target, brushContainerGo, brushSize, brushColor);
        UpdateBrushCursor(target, brushCursorGo, brushContainerGo, brushSize);
    }

    private void SetBrushProperties(Vector3 target, GameObject brushContainerGo, float brushSize,
        Color brushColor) //Buraya daha sonra degistirilecek ozellikler gonderilicek
    {
        var newBrushSpriteRen = Instantiate(brushSpriteRen, brushContainerGo.transform, true);

        newBrushSpriteRen.color = brushColor;
        var newBrushTrans = newBrushSpriteRen.transform;
        newBrushTrans.localPosition = target;
        newBrushTrans.localScale = Vector3.one * brushSize;
    }

    private static void UpdateBrushCursor(Vector3 target, GameObject brushCursorGo, GameObject brushContainerGo,
        float brushSize)
    {
        brushCursorGo.SetActive(true);
        brushCursorGo.transform.localScale = Vector3.one * brushSize;
        brushCursorGo.transform.position = target + brushContainerGo.transform.position - Vector3.forward / 6;
    }
}