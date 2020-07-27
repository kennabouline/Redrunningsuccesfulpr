using Boo.Lang.Environments;
using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
    
    
{
    public Slider healthSlider;
    const float MAXHEALTH = 100f;
    public float health;
    public Text mylife;
    private int Life;
    
   private void Start()
   {
        health = MAXHEALTH;
        healthSlider.value = health / MAXHEALTH;
        mylife.text = health.ToString("00");


    }




    void Die()
    {
        GetComponent<CharacterControl2D>().enabled = false;
        GetComponent<Animator>().SetBool("Dead", true);
    }
    public void TakeDamage(float amount)
    {
    
        health -= amount;

        if (health <= 0)
        {
            health = 0;
            Die();
        }


        healthSlider.value = health / MAXHEALTH;
        mylife.text = health.ToString("00");
        GetComponent<AudioSource>().Play();



    }


    public void AddLife()
    {
        health += 1;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("potion"))
        {
            health += 10;
            healthSlider.value = health / MAXHEALTH;
            mylife.text = health.ToString("00");
            Destroy(collision.gameObject);

        }
    }













}
