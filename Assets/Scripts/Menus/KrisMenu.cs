using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class KrisMenu : MonoBehaviour 
{
    [SerializeField] private Vector3[] positions; //0 = down, 1 = up
    [SerializeField] private GameObject[] highlights;
    [SerializeField] private RectTransform krisUIRect;
    
    public GameObject selectSound;
    public GameObject moveInMenuSound;

    public static KrisMenu I;

    private int selectedOption = 0;
    private int state = 0;
    
    public void changeState()
    {
        state = (state + 1) % 2; //0-> 1,1->0
        StartCoroutine("moveUI");
    }

    private void Start()
    {
        I = this;
        changeState(); //Pop up on start
    }
    private void Update()
    {
        if (state == 0) { return; }

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
        {
            //Fight Button
            if(selectedOption == 0)
            {
                Instantiate(selectSound);
                displayText("You swipe at sans", true);
            }
        }

        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedOption = (selectedOption + 1) % 5;
            changeSelected();
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //The ternary (?) operator -> condition ? return_if_true : return_if_false
            selectedOption = selectedOption == 0 ? 4 : selectedOption - 1; //Unity doesn't treat negative moodulus like I was taught
            changeSelected();
        }
    }

    private void changeSelected()
    {
        //Only display highlighted
        for (int i = 0; i < highlights.Length; i++)
        {
            if(selectedOption == i)
            {
                highlights[i].SetActive(true);
            }
            else
            {
                highlights[i].SetActive(false);
            }
        }
    }

    private IEnumerator moveUI()
    {
        //Hide Highlight when going down
        if(state == 0)
        {
            for(int i = 0; i < highlights.Length; i++)
            {
                highlights[i].SetActive(false);
            }
        }

        while (transform.localPosition != positions[state])
        {
            krisUIRect.localPosition = Vector3.MoveTowards(krisUIRect.localPosition, positions[state], 200 * Time.deltaTime);
            yield return null;
        }

        //Have previous option be selected
        if(state == 1)
        {
            highlights[selectedOption].SetActive(true);
        }
    }

    #region Kris Health UI
    [Header("Kris Health")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI maxHealthText;
    [SerializeField] private Image damageBar;

    public void updateHealth()
    {
        //Updates the text and healthbar
        healthText.text = "" + KrisHealth.I.health;
        damageBar.fillAmount = 1 - ((float)KrisHealth.I.health / (float)KrisHealth.I.maxHealth);
    }
    #endregion

    #region Message Text
    [Header("Textbox")]
    [SerializeField] private TextMeshProUGUI boxText;

    

    //Shortcut function for displaying text;
    public void displayText(string inputStr, bool startBattle)
    {
        StartCoroutine("displayTextCoroutine", new textInput(inputStr, startBattle));
    }

    //Struct to allow for multiple parameters in the coroutine
    public struct textInput
    {
        public textInput(string inputStr, bool startBattle)
        {
            inputString = inputStr;
            changeToBattle = startBattle;
        }

        public string inputString;
        public bool changeToBattle;
    }

    //Displays text one character at a time
    public IEnumerator displayTextCoroutine(textInput myInput)
    {
        for(int i = 0; i < myInput.inputString.Length; i++)
        {
            boxText.text = myInput.inputString.Substring(0, i + 1);
            yield return new WaitForSeconds(0.05f);
        }

        //Are we switching to the battle box?
        if(myInput.changeToBattle == true)
        {
            changeState(); //Hides UI
            yield return new WaitForSeconds(2);
            BattleBox.I.updateBoxState(1); //Starts battle
            boxText.text = "";
        }
    }

    #endregion
}
