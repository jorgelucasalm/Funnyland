using UnityEngine;

public class HeadCollider : MonoBehaviour
{
    public AudioSource audioS;
    public AudioClip Sound;
    private Rigidbody2D rb;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Colidiu com inimigo");
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<Animator>().SetTrigger("Death");
            // Remove apenas o inimigo que colidiu
            if (collision.gameObject.CompareTag("Player"))
            {
                // Desativa este script (do inimigo que colidiu)
                this.enabled = false;
            }
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            audioS.clip = Sound;
            audioS.Play();
            Destroy(collision.gameObject, 1f);
        }
    }
}