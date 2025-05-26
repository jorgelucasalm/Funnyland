using UnityEngine;
public class Inimigo : MonoBehaviour
{
    public float speed;
    private bool isGrounded = true;

    [SerializeField] private Transform peBear;
    [SerializeField] private LayerMask chaoLayer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        isGrounded = Physics2D.OverlapCircle(peBear.position, 0.2f, chaoLayer);

        if (isGrounded == false)
        {
            Flip();
        }
    }

    void Flip()
    {
        speed *= -1;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<Movimento>().Death();
        }
    }

}
