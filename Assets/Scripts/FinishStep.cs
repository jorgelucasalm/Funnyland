using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishStep : MonoBehaviour
{
    public GameObject textFinish;
    public AudioClip finishSound;
    // private AudioSource audioSource;

    private void Start()
    {
        textFinish.SetActive(false); // Ensure the text is hidden at the start
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {// Para parar o som do player:
            GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().Stop();

            // Para tocar o som deste componente:
            GetComponent<AudioSource>().PlayOneShot(finishSound);
            textFinish.SetActive(true);
            Time.timeScale = 0f; // Pauses or resumes the game
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Fase-1");
    }
}
