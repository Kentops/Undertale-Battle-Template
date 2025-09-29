using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KrisMenu : MonoBehaviour 
{
    [SerializeField] private Vector3[] positions; //0 = down, 1 = up
    [SerializeField] private GameObject[] highlights;
    
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
                changeState();
                Instantiate(selectSound);
                /*temp for debug*/
                BattleBox.I.updateBoxState(1);
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
            GetComponent<RectTransform>().localPosition = Vector3.MoveTowards(GetComponent<RectTransform>().localPosition, positions[state], 200 * Time.deltaTime);
            yield return null;
        }

        //Have previous option be selected
        if(state == 1)
        {
            highlights[selectedOption].SetActive(true);
        }
    }

    #region Kris Health UI
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
}
