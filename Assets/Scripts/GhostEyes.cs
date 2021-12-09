using UnityEngine;

public class GhostEyes : MonoBehaviour
{
    /**
     * Calls the different directions of the sprite
     */
    public SpriteRenderer spriteRenderer { get; private set; }
    public Movement movement { get; private set; }
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    /**
     * When the ghost is awake, it will render the eyes and the direction from the movement class
     */
    public void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.movement = GetComponentInParent<Movement>();
    }

    /**
     * Everytime a direction is called, everyframe it will change the sprite of the eyes to the direction.
     */
    private void Update()
    {
        if(this.movement.direction == Vector2.up)
        {
            this.spriteRenderer.sprite = this.up;
        }
        else if (this.movement.direction == Vector2.down)
        {
            this.spriteRenderer.sprite = this.down;
        }
        else if (this.movement.direction == Vector2.left)
        {
            this.spriteRenderer.sprite = this.left;
        }
        else if (this.movement.direction == Vector2.right)
        {
            this.spriteRenderer.sprite = this.right;
        }
    }
}
