using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI blinkingText;

    private void Start()
    {
        StartCoroutine(blink());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Battle Scene");
        }
    }

    private IEnumerator blink()
    {
        while (true) //Infinite loop
        {
            while (blinkingText.color.a > 0.1f)
            {
                blinkingText.color = blinkingText.color - new Color(0, 0, 0, 0.75f * Time.deltaTime);
                yield return null;
            }
            while (blinkingText.color.a < 1f)
            {
                blinkingText.color = blinkingText.color + new Color(0, 0, 0, 0.75f * Time.deltaTime);
                yield return null;
            }
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
