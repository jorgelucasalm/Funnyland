using UnityEngine;
using UnityEngine.SceneManagement;

public class Movimento : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float horizontalInput;
    private Rigidbody2D rb;

    [SerializeField] private int speed = 5;

    [SerializeField] private Transform pePersonagem;
    [SerializeField] private LayerMask chaoLayer;

    private bool isGrounded;
    private Animator animator;

    private int movendoHash = Animator.StringToHash("movendo");
    private int saltandoHash = Animator.StringToHash("saltando");
    private int dancandoHash = Animator.StringToHash("dancando");
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * 600);
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            animator.SetBool(dancandoHash, true);
        }

        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            animator.SetBool(dancandoHash, false);
        }

        isGrounded = Physics2D.OverlapCircle(pePersonagem.position, 0.2f, chaoLayer);

        animator.SetBool(movendoHash, horizontalInput != 0);
        animator.SetBool(saltandoHash, !isGrounded);


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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<SpriteRenderer>().flipY = true;
            collision.gameObject.GetComponent<Inimigo>().enabled = false;
            collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            Destroy(collision.gameObject, 1f);
        }
        if (collision.gameObject.tag == "Plataform")
        {
            gameObject.transform.parent = collision.transform;
        }
    }

    public void Death()
    {
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Movimento>().enabled = false;
        GetComponent<Animator>().SetTrigger("Dead");
        GetComponent<Animator>().SetBool("saltando", false);
        // Camera.main.GetComponent<Timer>().ResetTimer();
        Invoke("LoadScene", 1f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Fase-1");
    }
}
