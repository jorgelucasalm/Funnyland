using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

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
    public AudioClip lifeAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthLogic();
        if (isInvulnerable)
        {
            GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject inimigo in inimigos)
            {
                // Exemplo: desabilitar o collider
                Collider2D col = inimigo.GetComponent<Collider2D>();
                if (col != null)
                    col.enabled = false;
            }
        }
        if (!isInvulnerable)
        {
            GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject inimigo in inimigos)
            {
                // Exemplo: desabilitar o collider
                Collider2D col = inimigo.GetComponent<Collider2D>();
                if (col != null)
                    col.enabled = true;
            }
        }
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

        // Efeito visual opcional: piscar
        float elapsed = 0f;
        while (elapsed < invulnerableTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }
        spriteRenderer.enabled = true;

        isInvulnerable = false;
    }

    // No método de dano (exemplo em OnCollisionEnter2D ou LoseLife):
    private void OnTriggerEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvulnerable)
        {
            LoseLife();
        }
    }

    public void GainLife()
    {
        if (vida < vidaMaxima)
        {
            float originalVolume = GetComponent<Player>().audioS.volume;

            lifeAudio = GetComponent<Player>().Sounds[4];
            GetComponent<Player>().audioS.clip = lifeAudio;
            GetComponent<Player>().audioS.volume = 1.0f;
            GetComponent<Player>().audioS.Play();

            vida++;
            StartCoroutine(RestoreVolumeAfter(GetComponent<Player>().audioS, originalVolume, lifeAudio.length));
            Debug.Log("Vida aumentada! Vida atual: " + vida);
        }
        else
        {
            Debug.Log("Vida máxima já alcançada!");
        }
    }

    private IEnumerator RestoreVolumeAfter(AudioSource source, float volume, float delay)
    {
        yield return new WaitForSeconds(delay);
        source.volume = volume;
    }

    public void LoseLife()
    {
        if (!isInvulnerable)
        {
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
                // Aplique dano aqui (ex: reduzir vida)
                deathAudio = GetComponent<Player>().Sounds[1];
                GetComponent<Player>().audioS.clip = deathAudio;
                GetComponent<Player>().audioS.Play();

                FallPlataform[] plataformas = FindObjectsOfType<FallPlataform>();
                foreach (FallPlataform plataforma in plataformas)
                {
                    plataforma.ResetToOriginalState();
                }

                // Inicia invulnerabilidade
                StartCoroutine(Invulnerability());
            }
        }
    }

    public void Death()
    {
        Debug.Log("Game Over");
        GetComponent<Animator>().SetTrigger("Dead");
        deathAudio = GetComponent<Player>().Sounds[3];
        GetComponent<Player>().audioS.clip = deathAudio;
        GetComponent<Player>().audioS.Play();

        alive = false;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        // GetComponent<Player>().enabled = false;
        GetComponent<Animator>().SetBool("saltando", false);
        GetComponent<Player>().Lives = 0;
        Object.FindFirstObjectByType<PauseMenu>().DeathMenu();
    }

}

