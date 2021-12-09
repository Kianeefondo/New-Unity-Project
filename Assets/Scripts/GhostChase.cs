using UnityEngine;

/**
 * Ghost chase is a child of Ghost behavior
 */
public class GhostChase : GhostBehavior
{

    /**
     * When chase mode is disabled, it will enable scatter mode
     */
    private void OnDisable()
    {
        this.ghost.scatter.Enable();
    }

    /**
     * When the ghost enters chase mode, it will find Pacman in the least amount of distance every time it enters a new node
     * It will also set directions to a direction that is available
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled && !this.ghost.frightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach(Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance = (this.ghost.target.position - newPosition).sqrMagnitude;

                if(distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            this.ghost.movement.SetDirection(direction);
        }
    }
}
