using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour, IChangeableText
{
    [SerializeField]
    private Text _r, _g, _b;
    [HideInInspector]
    public Color _brushColor;
    [SerializeField]
    private GameObject _selectedColorBg;
    public Slider _brushSizeSlider;


    private void Start()
    {
        _brushColor = Color.red;
        _brushColor.a = 1f;
    }

    public void ChangeTexts(float r, float b, float g)
    {
        _r.text = "%" + r;
        _b.text = "%" + g;
        _g.text = "%" + b;
    }

    public void RedColor()
    {
        _brushColor = Color.red;
        _brushColor.a = .7f;
        SelectedColorIcon(0);
    }
    public void GreenColor()
    {
        _brushColor = Color.green;
        _brushColor.a = 0.7f;
        SelectedColorIcon(1);
    }
    public void BlueColor()
    {
        _brushColor = Color.blue;
        _brushColor.a = 0.7f;
        SelectedColorIcon(2);
    }

    private void SelectedColorIcon(int whichChild)
    {
        _selectedColorBg.transform.SetParent(transform.GetChild(1).GetChild(whichChild).transform, false);
    }


}
