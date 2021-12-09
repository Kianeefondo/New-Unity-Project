using UnityEngine;

/**
 * Requires ghost behavior to be attatched to a ghost in order to set behaviors to an object
 */
[RequireComponent(typeof(Ghost))]
public abstract class GhostBehavior : MonoBehaviour
{
    /**
     * Initialization of the ghost and its duration for certain behaviors
     */
    public Ghost ghost { get; private set; }
    public float duration;

    /**
     * Awake function is to set each ghost
     */
    private void Awake()
    {
        this.ghost = GetComponent<Ghost>();
        this.enabled = false;
    }

    /**
     * Enable is to enable different behaviors
     */
    public void Enable()
    {
        Enable(this.duration);
    }

    /**
     * This enable function is overwritten depending on the behavior of the ghost
     */
    public virtual void Enable(float duration)
    {
        this.enabled = true;
        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    /**
     * Disable cancels previous invokes called by the virtual enable function and disables any behaviors given the duration
     */
    public virtual void Disable()
    {
        this.enabled = false;
        CancelInvoke();
    }
}
