using UnityEngine;

public class Ghost : MonoBehaviour
{
    /**
     * Will call the different classes that a ghost will take
     */
    public Movement movement { get; private set; }
    public GhostHome home { get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostFrightened frightened { get; private set; }
    public GhostBehavior initialBehavior;
    public Transform target;

    /**
     * Initial points of a ghost every time it is eaten the first time.
     */
    public int points = 200;

    /**
     * Awake function means when the ghost is set true, it will be automatically be implemented with the different behaviors
     */
    private void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.home = GetComponent<GhostHome>();
        this.scatter = GetComponent<GhostScatter>();
        this.chase = GetComponent<GhostChase>();
        this.frightened = GetComponent<GhostFrightened>();
    }

    /**
     * Starting the round will call reset state in order to keep each ghost in its original spot
     */
    private void Start()
    {
        ResetState();
    }

    /**
     * Reset state will call the initial behaviors at which a ghost is at, and disabling and enabling different behaviors
     */
    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();

        this.frightened.Disable();
        this.chase.Disable();
        this.scatter.Enable();
        
        if(this.home != this.initialBehavior)
        {
            this.home.Disable();
        }
        if(this.initialBehavior != null)
        {
            this.initialBehavior.Enable();
        }
    }

    /**
     * Once the two layers collide such as Ghosts to Pacman, depending on what state the ghost is on, it will either eat Pacman or will be eaten.
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.frightened.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }
}
