
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; 
    public static float currentHealth;

    public TMP_Text ScoreText;
    public static float Score = 0;

    public TMP_Text HealthText;

    public TMP_Text HelpText;

    public GameObject GameOver;

    private void Start()
    {
        currentHealth = maxHealth;
        HelpText.gameObject.SetActive(true);
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
        HelpText.gameObject.SetActive(false);   
    }
 

    private void Update()
    {
       
        HealthText.text = "Health : " + currentHealth.ToString();
        ScoreText.text = "Score : " + Score.ToString();
       
        if(currentHealth<= 0)
        {
            GameOver.gameObject.SetActive(true);
            currentHealth = 0;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

    }

   
}