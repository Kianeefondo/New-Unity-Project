using UnityEngine;

public class PowerPellet : Pellet
{
    /**
     * Power pellets are registered to last for a contstant time
     */
    public float duration = 8.0f;

    /**
     * There is a different effect when this eat function is called
     */
    protected override void Eat()
    {
        FindObjectOfType<GameManager>().PowerPelletEaten(this);
    }

}
