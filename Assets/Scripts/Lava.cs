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
                playerLife.Death();
                playerLife.vida = 0;
            }
        }
    }
}
