using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    public bool isVulnerable = true;
    public float invulnerableTime = 1f;
    public int currentLives;
    public GameEvent OnPlayerHurt;
    public GameEvent OnEndGame;

    public List<GameObject> Lives;

    public void Setup()
    {
        currentLives = maxLives;
    }

    public void LoseLife()
    {
        if (isVulnerable && currentLives > 0)
        {
            Lives[currentLives - 1].SetActive(false);
            currentLives--;

            if (currentLives > 0)
            {
                OnPlayerHurt.Raise();
                StartCoroutine(InvulnerableTime());
            }
        }

        if (currentLives <= 0)
        {
            OnEndGame.Raise();
        }
    }

    private void Start()
    {
        Setup();
    }

    private IEnumerator InvulnerableTime()
    {
        var tempTimer = invulnerableTime;
        isVulnerable = false;

        while (tempTimer > 0f)
        {
            tempTimer -= Time.deltaTime;
            yield return null;
        }

        isVulnerable = true;
        yield return null;
    }
}
