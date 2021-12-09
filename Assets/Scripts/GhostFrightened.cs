using UnityEngine;

/**
 * Ghost Frightened is a child of ghost behavior
 */
public class GhostFrightened : GhostBehavior
{
    /**
     * Will call the different parts of the ghost sprite
     */
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;

    public bool eaten { get; private set; }

    /**
     * When the ghost is frightened, it will enable the blue in order to show its frightened state then invoke a flash once the duration is almost done.
     */
    public override void Enable(float duration)
    {
        base.Enable(duration);

        this.body.enabled = false;
        this.eyes.enabled = false;
        this.blue.enabled = true;
        this.white.enabled = false;

        Invoke(nameof(Flash), duration/2.0f);
    }

    /**
     * When frightened mode is disabled, it will disable everything and return to its original state and turn off frightened sprites
     */
    public override void Disable()
    {
        base.Disable();
        this.body.enabled = true;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;
    }

    /**
     * When a ghost is eaten it will teleport the ghost back in its home state and change back to its scatter/chase state
     */
    private void Eaten()
    {
        this.eaten = true;

        //Will just teleport home
        Vector3 position = this.ghost.home.inside.position;
        position.z = this.ghost.transform.position.z;
        this.ghost.transform.position = position;

        this.ghost.home.Enable(this.duration);

        this.body.enabled = false;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;
    }

    /**
     * When the ghost has not yet been eaten, the ghost will start to flash when the duration is almost done
     */
    private void Flash()
    {
        if(!this.eaten)
        {
            this.blue.enabled = false;
            this.white.enabled = true;
            this.white.GetComponent<AnimatedSprite>().Restart();
        }
    }

    /**
     * When the ghost is frightened, it will slow the ghost down
     */
    private void OnEnable()
    {
        this.ghost.movement.speedMultiplier = 0.5f;
        this.eaten = false;
    }
    
    /**
     * When the ghost is no longer frightened it will return to its original speed
     */
    private void OnDisable()
    {
        this.ghost.movement.speedMultiplier = 1.0f;
        this.eaten = false;
    }
    
    /**
     * When the ghost collides with the pacman layer, the ghost will initate the eaten function
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if(this.enabled)
            {
                Eaten();
            }
        }
    }

    /**
     * When the ghost enters Frightened mode, it will recalculate a new method of movement to which it will find the farthest possible direction to go away from pacman.
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance = (this.ghost.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }

            this.ghost.movement.SetDirection(direction);
        }
    }
}
