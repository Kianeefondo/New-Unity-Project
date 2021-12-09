using UnityEngine;

/**
 * Pacman is able to move using the movement class and requires this class to read movements
 */
[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    //Movement variable
    public Movement movement { get; private set; }

    /**
     * When this object is alive, the different movements will be registered at every interval
     */
    private void Awake()
    {
        this.movement = GetComponent<Movement>();
    }
    
    /**
     * At every frame, it will read a certain input and direct pacman to a certain turn or movement. If the direction is not available, it will turn at the next available direction.
     */
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.movement.SetDirection(Vector2.right);
        }

        float angle = Mathf.Atan2(this.movement.direction.y, this.movement.direction.x);
        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }
    
    /**
     * Reset state is called when pacman dies and has to orginate back to its original spot and direction.
     */
    public void ResetState()
    {
        this.movement.ResetState();
        this.gameObject.SetActive(true);
    }
}
