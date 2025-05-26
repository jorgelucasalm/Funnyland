using UnityEngine;

public class PlataformCollider : MonoBehaviour
{
    public Sprite tileEquerda;
    public Sprite tileDireita;
    public float tamanho = 1f; // Quantidade de tiles no meio

    public float maxX = 1f; // Quantidade de tiles no meio
    public float minX = 1f; // Quantidade de tiles no meio

    public float maxY = 1f; // Quantidade de tiles no meio
    public float minY = 1f; // Quantidade de tiles no meio

    public float moveSpeed = 2f;
    public bool platform1, plataform2;
    public bool moveRight = true, moveUp = true;
    float locationX;
    float locationY;
    void Start()
    {
        locationX = transform.position.x;
        locationY = transform.position.y;

        CriarPlataforma();
        AjustarBoxCollider();
    }

    void AjustarBoxCollider()
    {
        // Adiciona o BoxCollider2D se não existir
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        if (box == null)
            box = gameObject.AddComponent<BoxCollider2D>();

        // Ajusta o tamanho e o offset do collider
        float largura = tamanho + 2f; // +2 para incluir as bordas esquerda e direita
        box.size = new Vector2(largura, 1f); // 1f é a altura, ajuste conforme necessário
        box.offset = new Vector2(largura / 2f, 0f); // Centraliza o collider na plataforma
    }

    void CriarPlataforma()
    {
        // Criar borda esquerda
        CriarTile(tileEquerda, tamanho);

        // Criar blocos do meio
        // for (int i = 1; 2; i++)
        // {
        //     CriarTile(tileDireita, i);
        // }

        // Criar borda direita
        CriarTile(tileDireita, tamanho + 0.96f);
    }

    void CriarTile(Sprite sprite, float posicaoX, bool flipX = false)
    {
        GameObject tileGO = new GameObject("Tile_" + posicaoX);
        tileGO.transform.parent = this.transform;
        tileGO.transform.localPosition = new Vector3(posicaoX, 0, 0);

        SpriteRenderer sr = tileGO.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        // sr.flipX = flipX; // Flipa a sprite horizontalmente se for borda direita
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (platform1)
        {
            if (transform.position.x > locationX + maxX)
            {
                moveRight = false;
            }
            else if (transform.position.x < locationX - minX)
            {
                moveRight = true;
            }

            if (moveRight)
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.right * -moveSpeed * Time.deltaTime);
            }
        }

        if (plataform2)
        {
            if (transform.position.y > maxY)
            {
                moveUp = false;
            }
            else if (transform.position.y < minY)
            {
                moveUp = true;
            }

            if (moveUp)
            {
                transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.up * -moveSpeed * Time.deltaTime);
            }
        }
    }
}
