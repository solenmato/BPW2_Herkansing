using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth; 
    public Slider healthSlider; //game object, je hebt Unityengine UI nodig boven in
    public Image damageImage; 
    public AudioClip deathClip; //needs audio
    public float flashSpeed = 5f; //image blocks whole screen
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim; //anime componenent
    AudioSource playerAudio;
    PlayerMovement playerMovement;//ref to another script
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;


    void Awake ()
    {
        anim = GetComponent <Animator> (); 
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> (); 
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth; //awake is called at beginning
    }


    void Update () //flash the damage image
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false; //moment take damage, it sets it back
    }


    public void TakeDamage (int amount) //others can call this script 
    {
        damaged = true;

        currentHealth -= amount; 

        healthSlider.value = currentHealth; //gelinkt naar slider

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead) // het is extra dood
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true; 

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die"); //

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false; // geen beweging meer
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }
}
