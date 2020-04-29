using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeartManager : MonoBehaviour {

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;

    [Header("Respawn")]
    public float fadeWait;
    public GameObject panel;

    private void FixedUpdate()
    {
        UpdateHearts();
        if (playerCurrentHealth.RuntimeValue <= 0f) {
            Debug.Log("YOU HAVE DIED");
            playerCurrentHealth.RuntimeValue = 8f;
            StartCoroutine(FadeCo());
        }
    }

    public IEnumerator FadeCo()
    {
        if (panel != null)
        {
            Instantiate(panel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        //ResetCameraBounds();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("SampleScene");
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    public void InitHearts()
    {
        Debug.Log("INIT HEARTS");
        for (int i = 0; i < heartContainers.RuntimeValue; i ++)
        {
            if (i < hearts.Length)
            {
                hearts[i].gameObject.SetActive(true);
                hearts[i].sprite = fullHeart;
            }
        }
    }

    public void UpdateHearts()
    {
        InitHearts();
        Debug.Log("INIT HEARTS 2");
       
        float tempHealth = playerCurrentHealth.RuntimeValue / 2;
        Debug.Log(tempHealth);
        for (int i = 0; i < heartContainers.RuntimeValue; i ++)
        {
            if(i <= tempHealth-1)
            {
                //Full Heart
                hearts[i].sprite = fullHeart;
            }else if( i >= tempHealth)
            {
                //empty heart
                hearts[i].sprite = emptyHeart;
            }else{
                //half full heart
                hearts[i].sprite = halfFullHeart;
            }
        }
    }

}
