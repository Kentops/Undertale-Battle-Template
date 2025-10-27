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
    
    public AudioSource selectSound;
    public AudioSource damageEnemySound;
    public AudioSource attackSound;
    public AudioSource moveInMenuSound;

    public static KrisMenu I;

    private int selectedOption = 0;
    private int state = 0;
    
    public void changeState()
    {
        state = (state + 1) % 2; //0-> 1,1->0
        StartCoroutine("moveUI");
    }

    private void Awake()
    {
        I = this;
        changeState(); //Pop up on start
    }
    private void Update()
    {
        if (state == 0) { return; }

        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return)) && menuOptionsActive == false)
        {
            //Oh gosh this is such an unoptimal way to do this, this is due in a few days aaaaaaaaaaaa
            //Kris Menu
            //Fight Button
            StopCoroutine("displayTextCoroutine");
            if(selectedOption == 0)
            {
                selectSound.Play();
                StartCoroutine("attackMenu");
            }
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            if(menuOptionsActive == false)
            {
                //Options in the kris UI
                selectedOption = (selectedOption + 1) % 5;
                changeSelected();
            }
            else
            {
                //Options in the big text box
                menuOption = (menuOption + 1) % totalMenuOptions;
                changeBoxMenuSelected();
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(menuOptionsActive == false)
            {
                //The ternary operator (?) -> condition ? return_if_true : return_if_false
                selectedOption = selectedOption == 0 ? 4 : selectedOption - 1; //Unity doesn't treat negative moodulus like I was taught
                changeSelected();
            }
            else
            {
                menuOption = menuOption == 0 ? totalMenuOptions - 1 : menuOption - 1;
                changeBoxMenuSelected();
            }
           
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
        StartCoroutine("displayTextCoroutine", new textInput("* " + inputStr, startBattle));
    }

    public void displayPreviousText()
    {
        //Yoink the previous message   
        boxText.text = "* " + BattleLines.I.lines[(BattleLines.I.currentMessage - 1) == -1 ? BattleLines.I.lines.Length-1 : BattleLines.I.currentMessage - 1];
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

    #region Menu Choices
    [Header("Textbox menu items")]
    private int menuOption = 0;
    private int totalMenuOptions = 0;
    private bool menuOptionsActive = false;
    [SerializeField] private GameObject[] options;
    [SerializeField] private GameObject selectedIcon;
    [SerializeField] private Vector3[] heartPositions;
    [SerializeField] private KrisController krisAnim;
    [SerializeField] private EnemyManager enemy;

    [Header("Option Specifics")]
    [SerializeField] private FillBar enemyHealth;
    [SerializeField] private FillBar mercy;
    [SerializeField] private GameObject slash;



    private void changeBoxMenuSelected()
    {
        selectedIcon.SetActive(true);
        selectedIcon.transform.localPosition = heartPositions[menuOption];
    }
    private IEnumerator attackMenu()
    {
        //Initialize menu options
        totalMenuOptions = 1;
        menuOption = 0;
        menuOptionsActive = true;

        boxText.text = "";
        options[0].SetActive(true);
        options[0].GetComponent<TextMeshProUGUI>().text = "Sans";
        enemyHealth.updateProgress((int)((float)enemy.health / enemy.maxHealth * 100));
        mercy.updateProgress(enemy.mercy);
        changeBoxMenuSelected();

        bool keepGoing = true;
        yield return new WaitForSeconds(0.1f);
        while (keepGoing)
        {
            if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
            {
                if(menuOption == 0)
                {
                    keepGoing = false;
                    menuOptionsActive = false;

                    options[0].SetActive(false);
                    selectedIcon.SetActive(false);

                    //Fight
                    changeState();
                    krisAnim.state = 2;
                    yield return new WaitForSeconds(0.4f);
                    Instantiate(slash, enemy.transform.position, Quaternion.identity); //Throw the slash onto the enemy
                    enemy.health -= 8; //Damage the enemy
                    BattleBox.I.updateBoxState(1);
                    krisAnim.state = 1;
                    StartCoroutine(playDelayedSound(.05f, attackSound));
                    StartCoroutine(playDelayedSound(.25f, damageEnemySound));
                }
            }
            if(Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Backspace))
            {
                //return to menu
                keepGoing = false;
                menuOptionsActive = false;
                enemyHealth.gameObject.SetActive(false);
                mercy.gameObject.SetActive(false);
                options[0].SetActive(false);
                selectedIcon.SetActive(false);
                selectSound.Play();

                displayPreviousText();
            }
            yield return null;
        }
    }
    //for playing sounds such as the damage sound after a certain amount of time
    private IEnumerator playDelayedSound(float waitTime, AudioSource sound)
    {
        yield return new WaitForSeconds(waitTime);
        sound.Play();
        yield return null;
    }
    #endregion
}
