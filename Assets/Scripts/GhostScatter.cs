using UnityEngine;

/**
 * Ghost scatter is a child of ghost behavior
 */
public class GhostScatter : GhostBehavior
{
    /**
     * When the ghost's scatter mode duration is over, it will enable chase mode.
     */
    private void OnDisable()
    {
        this.ghost.chase.Enable();
    }

    /**
     * When the ghost enters this state, it will move in random directions at each node it passes.
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if(node != null && this.enabled && !this.ghost.frightened.enabled) 
        {
            int index = Random.Range(0, node.availableDirections.Count);

            if(node.availableDirections[index] == -this.ghost.movement.direction && node.availableDirections.Count > 1)
            {
                index++;

                if(index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }

            this.ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }


}
