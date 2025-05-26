using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishStep : MonoBehaviour
{
    public GameObject textFinish;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            textFinish.SetActive(true);
            Invoke("LoadScene", 5f);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Fase-1");
    }
}
