using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [Header("Player stuff")]
    public static int playerAmmo;
    [SerializeField] public static int health;
    [SerializeField] public static int maxHealth = 10;
    public static bool gamePaused, playerDead;
    public static Manager instance;



    [SerializeField] public static int currentExp;
    public static int maxExp = 10;
    // public int expGain;

    public static int level = 1;



    static GameUi gameUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }

        gameUI = FindObjectOfType<GameUi>();

        health = maxHealth;
        currentExp = 0;
        level = 1;
        

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public static void AddHealth(int inputHealth)
    {
        health += inputHealth;
        

        if(health < 1)
        {
            gameUI.CheckGameState(GameUi.GameState.GameOver);
            currentExp = 0;
        }

    }

    public static void AddXP(int xp)
    {


        currentExp += xp;

        if (currentExp >= maxExp)
        {

            currentExp = maxExp - currentExp;
            level++;
            gameUI.CheckGameState(GameUi.GameState.Upgrade);
            maxExp *= maxExp/4;

        }
    }

    public static void SetMaxHealth(int moreHealth)
    {
        maxHealth += moreHealth;
        health = maxHealth;
    }
    
}
