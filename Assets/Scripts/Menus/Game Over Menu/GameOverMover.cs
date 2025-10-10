using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMover : MonoBehaviour
{
    [SerializeField] private Image gameOverImage;
    [SerializeField] private TextMeshProUGUI[] optionTexts;
    [SerializeField] private Vector3[] destinations;
    [SerializeField] private AudioSource beep;
    [SerializeField] private AudioSource bgm;
    private int selected = 0;
    private bool canInteract = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine("moveText");
        bgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(canInteract == false) { return; }
        if(selected == 0 && Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            selected = 1;
            optionTexts[0].color = Color.white;
            optionTexts[1].color = Color.yellow;
            beep.Play();
        }
        else if (selected == 1 && Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selected = 0;
            optionTexts[1].color = Color.white;
            optionTexts[0].color = Color.yellow;
            beep.Play();
        }
        else if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
        {
            if (selected == 0)
            {
                //Return to game
                SceneManager.LoadScene("Battle Scene");
            }
            else
            {
                //Quit
                Application.Quit();
            }
        }
    }

    public IEnumerator moveText()
    {
        yield return new WaitForSeconds(1);
        while (gameOverImage.transform.localPosition != destinations[0])
        {
            gameOverImage.transform.localPosition = Vector3.MoveTowards(gameOverImage.transform.localPosition, destinations[0], 100 * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        while (optionTexts[0].transform.localPosition != destinations[1])
        {
            optionTexts[0].transform.localPosition = Vector3.MoveTowards(optionTexts[0].transform.localPosition, destinations[1], 50 * Time.deltaTime);
            optionTexts[1].transform.localPosition = Vector3.MoveTowards(optionTexts[1].transform.localPosition, destinations[2], 50 * Time.deltaTime);
            yield return null;
        }
        //Activate buttons
        canInteract = true;
        optionTexts[0].color = Color.yellow;
    }
}
