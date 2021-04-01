using UnityEngine;

public class TexturePainting : MonoBehaviour
{
    [HideInInspector]
    public GameObject _brushStroke;

    public void PaintTexture(Vector3 target, GameObject _brushCursor, GameObject _brushContainer, float brushSize, Color _brushColor) // Brush color ,size buraya disaridan gonderilicek.
    {
        SetBrushProperties(target, _brushContainer, brushSize, _brushColor);
        UpdateBrushCursor(target, _brushCursor, _brushContainer, brushSize, _brushColor);
    }
    void SetBrushProperties(Vector3 target, GameObject _brushContainer, float brushSize, Color _brushColor) //Buraya daha sonra degistirilecek ozellikler gonderilicek
    {
        GameObject brushObj = Instantiate(_brushStroke);
        brushObj.GetComponent<SpriteRenderer>().color = _brushColor;
        brushObj.transform.parent = _brushContainer.transform;
        brushObj.transform.localPosition = target;
        brushObj.transform.localScale = Vector3.one * brushSize;
    }
    void UpdateBrushCursor(Vector3 target, GameObject _brushCursor, GameObject _brushContainer, float brushSize, Color _brushColor)
    {
        _brushCursor.SetActive(true);
        _brushCursor.transform.localScale = Vector3.one * brushSize;
        _brushCursor.transform.position = target + _brushContainer.transform.position - Vector3.forward / 6;
    }
}
