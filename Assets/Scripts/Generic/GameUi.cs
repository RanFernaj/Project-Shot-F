using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUi : MonoBehaviour
{
    public enum GameState { MainMenu, Paused, Playing, GameOver, Upgrade, Win, Instructions }
    public GameState currentState;

    private PlayerController pc;
    private Weapon gun;
    private SpellCooldown cooldown;
    private Bullet bullet;


    [Header("Main Game UI")]
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] Slider expBar;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Slider hpBar;
    // [SerializeField] Slider dashBar;
    [SerializeField] GameObject dashCD_GO;
    [SerializeField] AudioManager audioManager;


    [Header("For Upgrade screen")]
    [SerializeField] GameObject[] upgradesPositions;
    [SerializeField] GameObject[] upgradeButtons;
    public List<int> TakeList = new List<int>();
    private int randomNo;



    // public Image redKeyUI, blueKeyUI, yellowKeyUI;
    [Header("Different UI's")]
    [SerializeField] GameObject allGameUI;
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject pauseMenuPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject titleText;
    [SerializeField] GameObject UpgradeScreen;
    [SerializeField] GameObject WinScreen;
    [SerializeField] GameObject InstructionScreen;

    // Start is called before the first frame update
    private void Start()
    {
       
        if(currentState == GameState.Playing)
        {
            pc = GameObject.Find("Player").GetComponent<PlayerController>();
            cooldown = GameObject.FindObjectOfType<SpellCooldown>();


        }
    }
    private void Awake() 
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            CheckGameState(GameState.MainMenu);
        }
        else if(SceneManager.GetActiveScene().name == "Scene1")
        {
            CheckGameState(GameState.Playing);
        }

    }
    // Update is called once per frame
    private void Update()
    {
        if(currentState == GameState.Playing && pc != null)
        {
            // All of the UI is updated here
            CheckInputs();
            UpdateHealth(Manager.health);
            UpdateAmmo();
            UpdateExp(Manager.currentExp);
            UpdateLevel(Manager.level);
            
            if(pc.GetDashDupgrade())
            {
                UpdateDashBar();
            }
            else
            {
                dashCD_GO.SetActive(false);
            }
            
        }
        
    }

    public void CheckGameState(GameState newGameState)
    {
        currentState = newGameState;
        switch (currentState)
        {
            case GameState.MainMenu:
                MainMenuState();
                break;
            case GameState.Paused:
                PausedState();
                Manager.gamePaused = true;
                break;
            case GameState.Playing:
                GameActiveState();
                Manager.gamePaused = false;
                break;
            case GameState.GameOver:
                GameOverState();
                Manager.gamePaused = true;
                break;
            case GameState.Upgrade:
                UpgradeMenuState();
                Manager.gamePaused = true;
                break;
            case GameState.Win:
                GameWonState();
                Manager.gamePaused = true;
                break;
            case GameState.Instructions:
                InstructionState();
                Manager.gamePaused = true;
                break;
        }
    }

    public void MainMenuState()
    {
        allGameUI.SetActive(false);
        mainMenuPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        titleText.SetActive(true);
        UpgradeScreen.SetActive(false);
        WinScreen.SetActive(false);
        InstructionScreen.SetActive(false);
    }

    public void GameActiveState()
    {
        Time.timeScale = 1f;

        allGameUI.SetActive(true);
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        titleText.SetActive(false);
        UpgradeScreen.SetActive(false);
        WinScreen.SetActive(false);
        InstructionScreen.SetActive(false);
    }

    public void PausedState()
    {
        Time.timeScale = 0f;

        allGameUI.SetActive(true);
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        titleText.SetActive(false);
        UpgradeScreen.SetActive(false);
        WinScreen.SetActive(false);
        InstructionScreen.SetActive(false);
    }


    public void GameOverState()
    {
        Time.timeScale = 0f;

        allGameUI.SetActive(false);
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        titleText.SetActive(false);
        UpgradeScreen.SetActive(false);
        WinScreen.SetActive(false);
        InstructionScreen.SetActive(false);
        audioManager.AudioPlayerDied();
    }
    public void GameWonState()
    {
        Time.timeScale = 0f;

        allGameUI.SetActive(false);
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        titleText.SetActive(false);
        UpgradeScreen.SetActive(false);
        WinScreen.SetActive(true);
        InstructionScreen.SetActive(false);
        
    }
    public void InstructionState()
    {
        Time.timeScale = 0f;

        allGameUI.SetActive(false);
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        titleText.SetActive(false);
        UpgradeScreen.SetActive(false);
        WinScreen.SetActive(false);
        InstructionScreen.SetActive(true);
    }



    public void UpgradeMenuState()
    {
        Time.timeScale = 0f;

        allGameUI.SetActive(false);
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        titleText.SetActive(false);
        UpgradeScreen.SetActive(true);
        WinScreen.SetActive(false);
        InstructionScreen.SetActive(false);
        // Make a Waiting time for some simple animations for leveling up
        ShowUpgrades();

    }

    

    void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
            {
                CheckGameState(GameState.Paused);
            }
            else if (currentState == GameState.Paused)
            {
                CheckGameState(GameState.Playing);
            }
        }
    }

    

    public void StartGame()
    {
        SceneManager.LoadScene("Scene1");
        CheckGameState(GameState.Playing);
    }

    public void EndScreen()
    {
        SceneManager.LoadScene("WinScreen");
        CheckGameState(GameState.Win);
    }

    public void Paused()
    {
        CheckGameState(GameState.Paused);
    }

    public void RestartGame()
    {
        CheckGameState(GameState.GameOver);
    }

    public void ResumeGame()
    {
        CheckGameState(GameState.Playing);
    }

    public void GoToMainMenu()
    {
        CheckGameState(GameState.MainMenu);
        SceneManager.LoadScene("MainMenu");
        
    }
    public void GoToInstructionScreen()
    {
        CheckGameState(GameState.Instructions);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    

    void UpdateDashBar()
    {
        dashCD_GO.SetActive(true);
        cooldown.SetCoolDownTime(pc.GetDashingCooldown());
    }

    void UpdateAmmo()
    {
        gun = GameObject.Find("Player").GetComponentInChildren<Weapon>();
        ammoText.text = "Ammo: " + gun.currentAmmo.ToString() + "/" + gun.GetMaxAmmo().ToString();
    }

    void UpdateExp(int exp)
    {
        // ExpText.text = "Exp: " + exp.ToString();
        expBar.maxValue = Manager.maxExp;
        expBar.value = exp;
    }

    void UpdateLevel(int level)
    {

        levelText.text = "Level: " + level.ToString();
    }
    void UpdateHealth(int health)
    {
        hpBar.maxValue = Manager.maxHealth;
        hpBar.value = health;
    }

    void ShowUpgrades()
    {

        TakeList = new List<int>(new int[upgradesPositions.Length]);
        for (int i = 0; i < upgradesPositions.Length; i++)
        {
            randomNo = UnityEngine.Random.Range(1, (upgradeButtons.Length) + 1);
                while(TakeList.Contains(randomNo))
                {
                    randomNo = UnityEngine.Random.Range(1, (upgradeButtons.Length) + 1);
                }
            TakeList[i] = randomNo;
            Instantiate(upgradeButtons[TakeList[i] - 1], upgradesPositions[i].transform);

            

        }
        
    }





    // public void UpdateKeys(Manager.DoorKeyColours keyColours)
    // {
    //     switch (keyColours)
    //     {
    //         case Manager.DoorKeyColours.Red:
    //             redKeyUI.GetComponent<Image>().color = Color.red;
    //             break;

    //         case Manager.DoorKeyColours.Blue:
    //             blueKeyUI.GetComponent<Image>().color = Color.blue;
    //             break;

    //         case Manager.DoorKeyColours.Yellow:
    //             yellowKeyUI.GetComponent<Image>().color = Color.yellow;
    //             break;
    //     }
    // }


}
