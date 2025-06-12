using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishStep : MonoBehaviour
{
    public GameObject textFinish;
    public AudioClip finishSound;
    private AudioSource audioSource;

    private void Start()
    {
        textFinish.SetActive(false); // Ensure the text is hidden at the start
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioSource>().Stop();
            textFinish.SetActive(true);
            GetComponent<AudioSource>().clip = finishSound;
            GetComponent<AudioSource>().Play();
            Time.timeScale = 0f; // Pauses or resumes the game
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Fase-1");
    }
}
