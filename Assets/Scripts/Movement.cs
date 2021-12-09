using UnityEngine;

/**
 * Movement is required to be attatched to a body in order for it to move, rather than a scene element or an empty game object that has no movement.
 */
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{

    /**
     * Declaration of properties of movement
     */
    public float speed = 8.0f;
    public float speedMultiplier = 1.0f;
    public new Rigidbody2D rigidbody { get; private set; }
    public Vector2 initialDirection;
    public LayerMask obstacleLayer;
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }

    /**
     * Once the game starts, the game will initiate this constructor in order to set the properties of a certain asset
     */
    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.startingPosition = this.transform.position;
    }

    /**
     * Once the game starts, it will initialize the settings using reset state
     */
    private void Start()
    {
        ResetState();
    }

    /**
     * ResetState initializes all values before starting the game
     */
    public void ResetState()
    {
        this.speedMultiplier = 1.0f;
        this.direction = this.initialDirection;
        this.nextDirection = Vector2.zero;
        this.transform.position = this.startingPosition;
        this.rigidbody.isKinematic = false;
        this.enabled = true;
    }

    /**
     * At every frame, a direction will be chosen and set direction is used to set the future, or the current direction
     */
    private void Update()
    {
        if(this.nextDirection != Vector2.zero)
        {
            SetDirection(this.nextDirection);
        }
        // Debug.DrawRay(transform.position, direction, Color.red); - Shows the direction at which pacman is facing
    }

    /**
     * FixedUpdate uses the properties of the current object in order to move and change any of its properties in any case of change in any property
     */
    private void FixedUpdate()
    {
        Vector2 position = this.rigidbody.position;
        Vector2 translation = this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;
        this.rigidbody.MovePosition(position + translation);
    }

    /**
     * Set direction will set the direction everytime an input is called
     */
    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            this.nextDirection = Vector2.zero;
        }
        else
        {
            this.nextDirection = direction;
        }
    }

    /**
     * Occupied recognizes that there is space availabile for pacman to enter and recognizes areas where pacman is not allowed to turn.
     */
    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 0.9f, this.obstacleLayer);
        return hit.collider != null;
    }
}