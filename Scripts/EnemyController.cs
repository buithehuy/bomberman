using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController : MonoBehaviour
{
    public float speed = 5.0f;
    public float changeDirectionTime = 0.5f;
    public Tilemap indestructibleTilemap;
    public Tilemap destructibleTilemap;
    public Sprite[] indestructibleSprites;
    public Sprite[] destructibleSprites;
    private Sprite[] sprites;
    private Vector3 startPosition;
    private float timeLeft;
    private Vector2 direction;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;
    private Collider2D col2d;
    private Tilemap tilemap;

    void Start()
    {
        startPosition = transform.position;
        timeLeft = changeDirectionTime;
        direction = Vector2.up;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
        sprites = new Sprite[indestructibleSprites.Length + destructibleSprites.Length];
        indestructibleSprites.CopyTo(sprites, 0);
        destructibleSprites.CopyTo(sprites, indestructibleSprites.Length);
        if (sprites.Length == 0)
        {
            Debug.LogError("Sprites array is empty.");
        }
        tilemap = destructibleTilemap; // Set the tilemap initially to the destructible tilemap
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            direction = GetRandomDirection();
            spriteRenderer.sprite = sprites[GetRandomSpriteIndex()];
            timeLeft = changeDirectionTime;
        }

        rb2d.velocity = direction * speed;

        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
        TileBase tile = tilemap.GetTile(cellPosition);

        if (tile != null && tile.name == "Block") // Check if the enemy balloon hits a block
        {
            direction *= -1; // Reverse direction if hit a block
        }
    }

    private Vector2 GetRandomDirection()
    {
        int randomDirection = Random.Range(0, 4);
        Vector2 newDirection = Vector2.zero;
        switch (randomDirection)
        {
            case 0:
                newDirection = Vector2.up;
                break;
            case 1:
                newDirection = Vector2.down;
                break;
            case 2:
                newDirection = Vector2.left;
                break;
            case 3:
                newDirection = Vector2.right;
                break;
        }

        Vector3Int cellPosition = tilemap.WorldToCell(transform.position + new Vector3(newDirection.x, newDirection.y, 0));
        TileBase tile = tilemap.GetTile(cellPosition);

        if (tile == null || tile.name != "Block") // Check if the new direction hits a block
        {
            return newDirection;
        }
        else
        {
            return direction; // Return the current direction if the new direction hits a block
        }
    }

    private int GetRandomSpriteIndex()
    {
        int randomIndex = Random.Range(0, sprites.Length);
        if(randomIndex >= sprites.Length)
        {
            randomIndex = sprites.Length - 1;
        }
        return randomIndex;
    }

    // This method is called when the enemy enters a new tilemap
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DestructibleTilemap"))
        {
            tilemap = destructibleTilemap;
        }
        else if (other.CompareTag("IndestructibleTilemap"))
        {
            tilemap = indestructibleTilemap;
        }
    }
}
