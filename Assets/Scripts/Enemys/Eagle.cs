using UnityEngine;
public class Eagle : InimigoBase
{
    public float speed = 2f;
    public float amplitude = 1f;
    public float frequency = 2f;
    public bool isVertical = true;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (isVertical)
        {
            // Movimento vertical senoide, posição X constante
            float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
            transform.position = new Vector3(startPos.x, newY, startPos.z);
        }
        else
        {
            // Movimento horizontal senoide, posição Y constante
            float newX = startPos.x + Mathf.Sin(Time.time * frequency) * amplitude;
            transform.position = new Vector3(newX, startPos.y, startPos.z);
        }
    }
}