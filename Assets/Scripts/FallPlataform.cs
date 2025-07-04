using UnityEngine;
using System.Collections; // Adicione esta linha

public class FallPlataform : MonoBehaviour
{
    private Coroutine fallCoroutine;
    private Rigidbody2D rb;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private RigidbodyType2D initialBodyType;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 1f;

        // Salva o estado original
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialBodyType = rb.bodyType;
    }

    public void ResetToOriginalState()
    {
        // Restaura posição e rotação
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // Restaura Rigidbody2D
        rb.bodyType = initialBodyType;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
            if (fallCoroutine == null)
                fallCoroutine = StartCoroutine(FallAfterDelay());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
            if (fallCoroutine != null)
            {
                StopCoroutine(fallCoroutine);
                fallCoroutine = null;
            }
        }
    }

    private IEnumerator FallAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        // Remove o player como filho antes de cair
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Player"))
            {
                child.SetParent(null);
            }
        }

        rb.bodyType = RigidbodyType2D.Dynamic; // Plataforma começa a cair
                                               // Ignorar colisão com outras plataformas
        Collider2D myCollider = GetComponent<Collider2D>();
        GameObject[] plataformas = GameObject.FindGameObjectsWithTag("Plataform");
        foreach (GameObject plataforma in plataformas)
        {
            if (plataforma != this.gameObject)
            {
                Collider2D otherCollider = plataforma.GetComponent<Collider2D>();
                if (otherCollider != null)
                {
                    Physics2D.IgnoreCollision(myCollider, otherCollider, true);
                }
            }
        }
    }
}
