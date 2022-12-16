using UnityEngine;

public class Passage : MonoBehaviour
{
    // Calls the connection
    public Transform connection;

    //When the trigger is called, it will teleport pacman to the other connecting passage.
    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 position = other.transform.position;
        position.x = this.connection.position.x;
        position.y = this.connection.position.y;
        other.transform.position = position;
    }


}
