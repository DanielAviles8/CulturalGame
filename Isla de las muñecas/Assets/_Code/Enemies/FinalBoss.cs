using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalBoss : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject Doll;
    [SerializeField] private GameObject Demon;
    [SerializeField] private GameObject Explosion;

    [SerializeField] private Image fadeImage;

    public float EndTimer = 2;
    int levelIndex = 2;
    public float Health = 1;
    public bool bossDefeated;

    void Start()
    {
        bossDefeated = false;
        Explosion.SetActive(false);
        Demon.SetActive(false);

        if (fadeImage != null)
        {
            var color = fadeImage.color;
            color.a = 0f;
            fadeImage.color = color;
        }
    }

    public void DoDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            bossDefeated = true;
            Doll.SetActive(false);
            Explosion.SetActive(true);
            Demon.SetActive(true);
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(EndTimer);

        float fadeDuration = 2f;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(t / fadeDuration);

            if (fadeImage != null)
            {
                var color = fadeImage.color;
                color.a = normalizedTime;
                fadeImage.color = color;
            }

            yield return null;
        }

        LoadLevel(levelIndex);
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
