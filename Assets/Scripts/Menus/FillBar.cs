using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private TextMeshProUGUI percentText;

    public void updateProgress(int percent)
    {
        bar.fillAmount = 1 - ((float)percent / 100);
        Debug.Log(1 - ((float)percent / 100));
        percentText.text = percent + "%";
    }
}
