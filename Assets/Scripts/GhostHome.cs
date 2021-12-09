using System.Collections;
using UnityEngine;

/**
 * Ghost home is a child of ghost behavior
 */
public class GhostHome : GhostBehavior
{

    /**
     * Declaration of variables
     */
    public Transform inside;
    public Transform outside;

    /**
     * When the ghost is at home, then it will stop all coroutines that it has in order to start the ghosthome coroutines
     */
    private void OnEnable()
    {
        StopAllCoroutines();
    }

    /**
     * When this ghost is no longer in home state, it will activate itself and initiate an exit transition which will also initiate the scatter state
     */
    private void OnDisable()
    {
        if(this.gameObject.activeSelf)
        {
            StartCoroutine(ExitTransition());
        }
    }

    /**
     * When it collides, it will force itself to move up and down mainly just to show movement in the home box.
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            this.ghost.movement.SetDirection(-this.ghost.movement.direction);
        }
    }

    /**
     * Exist transition is a force ienumerator which will force the ghost to move in a certain position in order for it to pass through the obsticle layer and into the map. The door has no purpose other than for aesthetic
     */
    private IEnumerator ExitTransition()
    {
        this.ghost.movement.SetDirection(Vector2.up, true);
        this.ghost.movement.rigidbody.isKinematic = true;
        this.ghost.movement.enabled = false;

        Vector3 position = this.transform.position;

        float duration = 0.5f;
        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, this.inside.position, elapsed/duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(this.inside.position, this.outside.position, elapsed / duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        this.ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);
        this.ghost.movement.rigidbody.isKinematic = false;
        this.ghost.movement.enabled = true;
    }
}
