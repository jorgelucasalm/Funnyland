using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float horizontalInput;
    private Rigidbody2D rb;

    [SerializeField] private int speed = 5;

    [SerializeField] private Transform pePersonagem;
    // Defina os valores conforme necessário
    public float largura;
    public float altura;
    [SerializeField] private LayerMask chaoLayer;

    private bool isGrounded;
    private Animator animator;

    private int movendoHash = Animator.StringToHash("movendo");
    private int saltandoHash = Animator.StringToHash("saltando");
    private int dancandoHash = Animator.StringToHash("dancando");
    private SpriteRenderer spriteRenderer;

    public int Cherries;
    public Text cherriesText;
    public int Lives = 3;

    public AudioSource audioS;
    public AudioClip[] Sounds;

    private Vector2 respawnPoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetCheckpoint(transform.position); // Salva o ponto de respawn inicial
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pePersonagem.position, new Vector3(largura, altura, 0));
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapBox(pePersonagem.position, new Vector2(largura, altura), 0f, chaoLayer);
        animator.SetBool(movendoHash, horizontalInput != 0);
        animator.SetBool(saltandoHash, !isGrounded);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * 700);
            audioS.clip = Sounds[0];
            audioS.Play();
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            animator.SetBool(dancandoHash, true);
        }

        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            animator.SetBool(dancandoHash, false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool(dancandoHash, true);
        }

        if (animator.GetBool(dancandoHash))
        {
            if (horizontalInput != 0 || Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool(dancandoHash, false);
            }
            return;
        }

        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
    }

    public void SetCheckpoint(Vector2 point)
    {
        respawnPoint = point;
        Debug.Log("Checkpoint salvo: " + point);
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
        // Resetar vida, animação ou efeitos se necessário
    }

    // This method is called when the collider other enters the trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // Se o personagem está caindo (pulando sobre o inimigo)
            if (rb.linearVelocity.y < 0)
            {
                // Mata o inimigo
                collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                Debug.Log("Colidiu com inimigo por cima");
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Zera Y antes do impulso
                rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                collision.gameObject.GetComponent<Animator>().SetTrigger("Death");
                audioS.clip = Sounds[1];
                audioS.Play();
                Destroy(collision.gameObject, 0.4f); // Dá tempo do som/anim tocar
            }
            else
            {
                // O personagem leva dano
                GetComponent<PlayerLife>().LoseLife();
            }
        }
        if (collision.gameObject.tag == "Plataform")
        {
            gameObject.transform.parent = collision.transform;
        }
        if (collision.gameObject.tag == "cherries")
        {
            Destroy(collision.gameObject);
            Cherries++;
            cherriesText.text = Cherries.ToString();
            if (Cherries % 2 == 0 && Cherries > 0)
            {
                GetComponent<PlayerLife>().GainLife();
            }
        }
    }
}

