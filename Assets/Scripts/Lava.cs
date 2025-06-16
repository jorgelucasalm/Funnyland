using UnityEngine;
using UnityEngine.SceneManagement;

public class Lava : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerLife playerLife = collision.gameObject.GetComponent<PlayerLife>();
            if (playerLife != null)
            {
                collision.gameObject.GetComponent<PlayerLife>()?.LoseLife();
                if(playerLife.alive)
                    collision.gameObject.GetComponent<Player>().Respawn();
            }
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
