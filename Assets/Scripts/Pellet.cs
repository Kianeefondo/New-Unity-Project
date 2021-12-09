using UnityEngine;

public class Pellet : MonoBehaviour
{
    //Pellets are initialized to give 10 points to the player for each pellet eaten
    public int points = 10;

    /**
     * The eat function is called in order to show that a pellet has been collided with and should disappear
     */
    protected virtual void Eat()
    {
        FindObjectOfType<GameManager>().PelletEaten(this);
    }

    /**
     * When the pellet is collided with pacman, it will call the eat function.
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }
}
