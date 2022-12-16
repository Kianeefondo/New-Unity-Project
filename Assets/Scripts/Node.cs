using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    /**
     * Calls the nodes and directions
     */
    public LayerMask obstacleLayer;
    public List<Vector2> availableDirections { get; private set; }

    /**
     * Checks at which node has the certain available direction
     */
    private void Start()
    {
        this.availableDirections = new List<Vector2>();
        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);
    }

    /**
     * Checking for the available direction depending on a box cast which will be used to tell the ghosts or pacman where they are able to turn or move.
     */
    private void CheckAvailableDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.5f, 0.0f, direction, 0.4f, this.obstacleLayer);
        if(hit.collider == null)
        {
            this.availableDirections.Add(direction);
        }
    }
}
