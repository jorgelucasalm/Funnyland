using UnityEngine;

public class InimigoBase : MonoBehaviour
{
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerLife>()?.LoseLife();
            Debug.Log("Player collided with enemy: " + collision.gameObject.name);
        }
    }
}