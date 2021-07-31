using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour, IChangeableText
{
    [SerializeField] private Text redText, greenText, blueText;
    [HideInInspector] public Color brushColor;
    [SerializeField] private GameObject selectedColorBg;
    public Slider brushSizeSlider;


    private void Start()
    {
        brushColor = Color.red;
        brushColor.a = 1f;
    }

    public void ChangeTexts(float red, float blue, float green)
    {
        this.redText.text = "%" + red;
        this.blueText.text = "%" + green;
        this.greenText.text = "%" + blue;
    }

    public void RedColor()
    {
        brushColor = Color.red;
        brushColor.a = .7f;
        SelectedColorIcon(0);
    }

    public void GreenColor()
    {
        brushColor = Color.green;
        brushColor.a = 0.7f;
        SelectedColorIcon(1);
    }

    public void BlueColor()
    {
        brushColor = Color.blue;
        brushColor.a = 0.7f;
        SelectedColorIcon(2);
    }

    private void SelectedColorIcon(int whichChild)
    {
        selectedColorBg.transform.SetParent(transform.GetChild(1).GetChild(whichChild).transform, false);
    }
}