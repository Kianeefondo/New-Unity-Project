using UnityEngine;

/**
 * Requires a sprite to be shown in order to animate
 */
[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    /**
     * Variables that affect the sprite
     */
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] sprites;
    public float animationTime = 0.25f;
    public int animationFrame { get; private set; }
    public bool loop = true;

    /**
     * If the sprite is awake, it will always be animating with the different sprites it is given to animate with
     */
    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /**
     * Invoke call to repeat the animation frames in a set amount of time
     */
    private void Start()
    {
        InvokeRepeating(nameof(Advance), this.animationTime, this.animationTime);
    }

    /**
     * Advance is the advancing of sprites between each frame it is moving
     */
    private void Advance()
    {
        if(!this.spriteRenderer.enabled)
        {
            return;
        }
        
        this.animationFrame++;

        if(this.animationFrame >= this.sprites.Length && this.loop)
        {
            this.animationFrame = 0;
        }

        if(this.animationFrame >= 0 && this.animationFrame < this.sprites.Length)
        {
            this.spriteRenderer.sprite = this.sprites[this.animationFrame];
        }
    }

    /**
     * When the loop is done, it will call the restart function in order to restart the animation
     */
    public void Restart()
    {
        this.animationFrame = -1;

        Advance();
    }
}
