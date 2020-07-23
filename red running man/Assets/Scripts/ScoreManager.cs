using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    int score;
    public Text scoretxt;
    
    

   void OnTriggerEnter2D(Collider2D col)
    {
        score++;
        Destroy(col.gameObject);
        scoretxt.text = score.ToString();
        PlayerPrefs.SetInt("savedscore", score);
        if(score > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }

    private void Update()
    {
       

    }



}
