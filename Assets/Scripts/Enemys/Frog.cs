using UnityEngine;
public class Frog : InimigoBase
{
    public float jumpForce = 8f;
    public float detectionRange = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Transform player;

    private Rigidbody2D rb;
    private bool isFacingRight = true;

    private float jumpCooldown = 2f; // 2 segundos
    private float jumpTimer = 0f;

    public Sprite spriteSubindo;
    public Sprite spriteCaindo;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;


        if (!IsGrounded())
        {
            GetComponent<Animator>().enabled = false;
            spriteRenderer.sprite = spriteCaindo;
        }
        else
        {
            GetComponent<Animator>().enabled = true;
        }

        if (jumpTimer > 0f)
            jumpTimer -= Time.deltaTime;

        if (rb.linearVelocity.y > 0.1f)
        {
            spriteRenderer.flipX = true;
            spriteRenderer.sprite = spriteSubindo;
        }
        else if (rb.linearVelocity.y < -0.1f)
        {
            spriteRenderer.flipX = true;
            spriteRenderer.sprite = spriteCaindo;
        }


        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= detectionRange && IsGrounded() && jumpTimer <= 0f)
        {
            FlipTowardsPlayer();
            JumpTowardsPlayer();
            jumpTimer = jumpCooldown;
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    void JumpTowardsPlayer()
    {
        float direction = player.position.x < transform.position.x ? -1f : 1f;
        rb.linearVelocity = new Vector2(direction * 2f, jumpForce);
    }

    void FlipTowardsPlayer()
    {
        if ((player.position.x < transform.position.x && isFacingRight) ||
            (player.position.x > transform.position.x && !isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}