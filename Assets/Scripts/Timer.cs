using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{


    public Text timeText;
    public float timeCount; // Tempo limite em segundos
    public float tempoInicial; // Tempo limite em segundos
    public bool timeOver = false;

    private void Update()
    {
        tempoInicial = timeCount;
        TimeCount();
    }

    public void ResetTimer()
    {
        // Supondo que você tenha uma variável float chamada "timer"
        timeCount = tempoInicial; // tempoInicial é o valor inicial do tempo
    }

    public void RefreshScreen()
    {
        timeText.text = timeCount.ToString("F0");
    }

    void TimeCount()
    {
        if (timeOver) return;

        timeCount -= Time.deltaTime;
        if (timeCount <= 0)
        {
            timeCount = 0;
            GameObject.Find("Player").GetComponent<Movimento>().Death();
            timeOver = true;
        }
        RefreshScreen();
    }
}
