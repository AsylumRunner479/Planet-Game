using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerHandler : MonoBehaviour
{
    [System.Serializable]
    //create a public struct called PlayerStats with a string called name and an int called value
    public struct PlayerStats
    {
        public string name;
        public int value;
    };

    [Header("Value Variables")]
    //create a public float called curHealth
    public float curHealth;
    //create public floats called curMana, maxHealth, maxMana, maxStamina and healrate
    public float curMana, maxHealth, maxMana, maxStamina, healRate;
    //create public static float called curStamina
    public static float curStamina;
    //create PlayerStats called stats
    //public PlayerStats[] stats;
    [Header("Value Variables")]
    //crete a public slider called healthBar, manaBar and staminaBar
    //public Slider healthBar;
    //public Slider manaBar, staminaBar;
    [Header("Damage Effect Variables")]
    //create public images for damage and death
    //public Image damageImage;
   // public Image deathImage;
    //create public text called text
    public Text text;
    //create an array of texts called power
    public Text[] power;
    //create a sudioclip clip called deathClip
    //public AudioClip deathClip;
    //create a public float called flashspeed and make it equal 5
    public float flashSpeed = 5f;
    //create a public color called flashColour and make it red
    public Color flashColour = new Color(1, 0, 0, 0.2f);
    //create a private audioSource and call it playerAudio
    AudioSource playerAudio;
    //create a static bool and call it isDead
    public static bool isDead;
    //access the Customisation script and call it StatsNumbers
    //public Customisation StatsNumbers;
    //create a private bool and call it damaged
    bool damaged;
    //create a private bool and call it canHeal
    bool canHeal;
    //create a private float and call it healTimer
    float healTimer;
    [Header("Check Point")]
    //create a Transformand and call it curCheckPoint
    public Transform curCheckPoint;
    [Header("Save")]
    //access the PlayerSaveAndLoad
    //public PlayerSaveAndLoad saveAndLoad;
    [Header("Custom")]
    //create a public bool and call it custom
    public bool custom;
    //create public ints and call them skinIndex, eyesIndex, mouthIndex, hairIndex, clothesIndex, armourIndex
    public int skinIndex, eyesIndex, mouthIndex, hairIndex, clothesIndex, armourIndex;
    //create a CharacterClass and call it characterClass
    //public CharacterClass characterClass;
    //create a CharacterRace and call it characterRace
    //public CharacterRace characterRace;
    //create a public string and call it characterName
    public string characterName;
    //create a public string call firstCheckPointName and desiginate it as FirstCheckPoint
    public string firstCheckPointName = "First CheckPoint";
    //create a function that activates at the start of the level
    private void Start()
    {
        //set isDead to false
        isDead = false;
        //set playerAudio to the AudioSource Component
        playerAudio = GetComponent<AudioSource>();
        //set maxMana to ten times the stat 3 value (intelligence)
        maxMana = 10f;
        //set maxStamina to ten times the stat 1 value (dexterity)
        maxStamina = 10f ;
        //set maxHealth to ten time the stat 4 value (wisdom) + 60
        maxHealth = 70f;
        //make curHealth equal maxhealth
        curHealth = maxHealth;
        //set curMana to maxMana
        curMana = maxMana;
        //set curStamina to maxStamina
        curStamina = maxStamina;
        //set power texts to equal the stats so its displayed
        for (int s = 0; s < power.Length; s++)
        {
            //power[s].text = stats[s].name + ": " + (stats[s].value);
        }
    }
    //create a private function that activates on update 
    void Update()
    {
        //if not in the custom menu
        if (!custom)
        {
            //Display Health
            //if healthbar slider doesn't equal curHealth divided by maxHealth
            //then make the healthBar equal curHealth divided by maxHealth
            //if (healthBar.value != Mathf.Clamp01(curHealth / maxHealth))
            {
                curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
                //healthBar.value = Mathf.Clamp01(curHealth / maxHealth);
            }
            //display mana
            //if manabar slider doesn't equal curmana divided by maxmana
            //then make the manaBar equal curmana divided by maxmana
            //if (manaBar.value != Mathf.Clamp01(curMana / maxMana))
            {
                curMana = Mathf.Clamp(curMana, 0, maxMana);
                //manaBar.value = Mathf.Clamp01(curMana / maxMana);
            }
            //display stamina
            //if staminabar slider doesn't equal curstamina divided by maxstamina
            //then make the staminaBar equal curstamina divided by maxstamina
            //if (staminaBar.value != Mathf.Clamp01(curStamina / maxStamina))
            {
                curStamina = Mathf.Clamp(curStamina, 0, maxStamina);
                //staminaBar.value = Mathf.Clamp01(curStamina / maxStamina);
            }
            //dies when running out of health
            //when player is not dead and curhealth is less then or equal to 0
            //activate the Death function
            if (curHealth <= 0 && !isDead)
            {
                Death();
            }



            //shows when the character gets damaged
            //when the player is damaged and is not dead
            //set damaged to false
            //change damageimage color to flashcolour
            if (damaged && !isDead)
            {
                //damageImage.color = flashColour;
                damaged = false;
            }
            //make the damageImage fade over time to clear
            else
            {
                //damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);

            }
            //can heal under certain circumstances
            //if canHeal is false, curHealth is less then maxHealth and curHealth is greater then 0
            //increase healTimer over time
            //if healTimer is greater then 5 then set canHeal to true
            if (!canHeal && curHealth < maxHealth && curHealth > 0)
            {
                healTimer += Time.deltaTime;
                if (healTimer >= 5)
                {
                    canHeal = true;
                }
            }
        }
    }
    //create a function that updates after the update
    private void LateUpdate()
    {
        //heals after checking if you can heal
        //if curHealth is less then maxHealth, curHealth is greater then 0 and canHeal
        //activate the function HealOverTime
        if (curHealth < maxHealth && curHealth > 0 && canHeal)
        {
            HealOverTime();
        }
    }
    //your character dies
    //create a function called Death
    void Death()
    {
        //Set the death flag to this function isnt called again
        //set isDead to true
        isDead = true;
        //make the text called text blank
        text.text = "";

        //Set the AudioSource to play the death clip
       // playerAudio.clip = deathClip;
        playerAudio.Play();
        //make the Animator activate the trigger isDead
        //deathImage.gameObject.GetComponent<Animator>().SetTrigger("isDead");
        //after 2 seconds activate the DeathText function
        Invoke("DeathText", 2f);
        //after 6 seconds activate the ReviveText function
        Invoke("ReviveText", 6f);
        //after 9 seconds activate the Revive function
        Invoke("Revive", 9f);
    }
    void Revive()
    {
        //set the text to blank
        text.text = "";
        //set isDead to false
        isDead = false;
        //set curhealth to maxhealth
        curHealth = maxHealth;
        //set curmana to maxmana
        curMana = maxMana;
        //set curstamina to maxstamina
        curStamina = maxStamina;

        //more and rotate to spawn location
        this.transform.position = curCheckPoint.position;
        this.transform.rotation = curCheckPoint.rotation;

        //deathImage.gameObject.GetComponent<Animator>().SetTrigger("Revive");
    }
    void DeathText()
    {
        text.text = "You've Fallen in Battle...";
    }
    void ReviveText()
    {
        text.text = "...But the Gods have decided it isn't your time...";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            curCheckPoint = other.transform;
            healRate = 5;
           // saveAndLoad.Save();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            healRate = 0;
        }
    }
    public void DamagePlayer(float damage)
    {
        damaged = true;
        curHealth -= damage;
        canHeal = false;
        healTimer = 0;
    }
    //create a function called HealOverTime
    //make the curHealth increase over time by a rate based on healRate + stats 2 value (constuition)
    public void HealOverTime()
    {
        curHealth += Time.deltaTime * (healRate);
    }
}
