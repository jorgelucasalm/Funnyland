using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerLife : MonoBehaviour
{
    public bool alive = true;

    public int vida;
    public int vidaMaxima;

    public Image[] vidas;
    public Sprite cheia;
    public Sprite vazio;

    public float invulnerableTime = 2f;
    public float blinkInterval = 0.1f;
    private bool isInvulnerable = false;
    private SpriteRenderer spriteRenderer;
    
    public AudioClip deathAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthLogic();
    }

    void HealthLogic()
    {

        for (int i = 0; i < vidas.Length; i++)
        {
            if (i < vida)
            {
                vidas[i].sprite = cheia;
            }
            else
            {
                vidas[i].sprite = vazio;
            }

            if (i < vidaMaxima)
            {
                vidas[i].enabled = true;
            }
            else
            {
                vidas[i].enabled = false;
            }
        }
    }

    private IEnumerator Invulnerability()
    {
        isInvulnerable = true;

        float timer = 0f;

        while (timer < invulnerableTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // pisca
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        spriteRenderer.enabled = true; // garante que fique visível
        isInvulnerable = false;
    }

    public void GainLife()
    {
        if (vida < vidaMaxima)
        {
            vida++;
            Debug.Log("Vida aumentada! Vida atual: " + vida);
        }
        else
        {
            Debug.Log("Vida máxima já alcançada!");
        }
    }

    public void LoseLife()
    {
        if (!isInvulnerable)
        {
            // Aplique dano aqui (ex: reduzir vida)
            Debug.Log("Tomou dano!");
            deathAudio = GetComponent<Player>().Sounds[1];
            GetComponent<Player>().audioS.clip = deathAudio;
            GetComponent<Player>().audioS.Play();

            // Inicia invulnerabilidade
            StartCoroutine(Invulnerability());
            vida--;
            if (vida == 0)
            {
                deathAudio = GetComponent<Player>().Sounds[3];
                GetComponent<Player>().audioS.clip = deathAudio;
                GetComponent<Player>().audioS.Play();
                Death();
            }
            else
            {
                Debug.Log("Player is taking damage.");
            }
        }
    }

    public void Death()
    {
        deathAudio = GetComponent<Player>().Sounds[3];
        GetComponent<Player>().audioS.clip = deathAudio;
        GetComponent<Player>().audioS.Play();

        alive = false;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Player>().enabled = false;
        GetComponent<Animator>().SetTrigger("Dead");
        GetComponent<Animator>().SetBool("saltando", false);
        Debug.Log("Game Over");
        GetComponent<Player>().Lives = 0;
        Object.FindFirstObjectByType<PauseMenu>().DeathMenu();
        
    }

}

