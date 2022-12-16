using UnityEngine;

public class GameManager : MonoBehaviour
{
    /**
     * Declaration of variables
     * ghostMultiplier is the points multiplier
     */
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;

    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }
    
    /**
     * Once the game starts, it will invoke the "NewGame()" function as written in the string, and wait for 2 seconds
     */
    private void Start()
    {
        Invoke("NewGame",0);
    }

    /**
     * Every frame, the game will check if your lives are greater than or equal to 0. Once 3 deaths are done, then it will require an input key to restart the game
     */
    private void Update()
    {
        if (this.lives <= 0 && Input.GetKeyDown(KeyCode.P)) NewGame();
    }

    /**
     * New Game function is called to start the new game and to restart any values, such as lives and score. It will also start the round
     */
    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    /**
     * New Round is called and sets every pellet to be true.
     */
    private void NewRound()
    {
        foreach(Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    /**
     * Whenever pacman dies, it will reset state of every ghost, and pacman's position in order to start a new round.
     */
    private void ResetState()
    {
        ResetGhostMultiplier();

        for (int i = 0; i < this.ghosts.Length; ++i)
        {
            this.ghosts[i].ResetState();
        }

        this.pacman.ResetState();
    }

    /**
     * Initiating a game over will turn off all ghosts and pacman
     */
    private void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; ++i)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);
    }

    /**
     * Setting the score everytime a pellet is eaten and restarting the score once the game starts.
     */
    private void SetScore(int score)
    {
        this.score = score;
    }

    /**
     * Setting the live everytime a life is lost or once a game is restarted
     */
    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    /**
     * When a ghost is eaten it will increase the points depending on the ghost multiplier, set the score everytime and increment the ghost multiplier.
     */
    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.score + points);
        this.ghostMultiplier++;
    }

    /**
     * When pacman collides with a ghost, it will turn pacman off and reduce once of his lives by 1. It will restart the game if there are still any remaining lives, but initiate a game over once there is no more.
     */
    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);

        SetLives(this.lives - 1);

        if(this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f); //Will wait 3 seconds before starting a new round.
        }
        else
        {
            GameOver();
        }
    }

    /**
     * Counts all the pellets that are active, and will turn each pellet inactive once it is eaten or collided with pacman. It will also increate the score of pacman
     * Once the pellets are all gone, it will set Pacman off, and initiate a new round in 3 seconds using the Invoke.
     */
    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(this.score + pellet.points);

        if(!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    /**
     * When a power pellet has been eaten, it will iterate through all ghosts and change their state to the frightened state as long as the pellet's duration.
     * It will cancel any previous invoke in order to do another invoke statement when another power pellet has been eaten in order to reset the frightened state rather than extending it (or adding on to the current duration).
     */
    public void PowerPelletEaten(PowerPellet pellet)
    {
        for(int i = 0; i<this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(pellet.duration);
        }

        
        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    /**
     * Will check for all pellets in order to see if the game ends or not.
     */
    private bool HasRemainingPellets()
    {
        foreach(Transform pellet in this.pellets)
        {
            if(pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    /**
     * When a ghost has been eaten it will incremend the ghost multiplier, and this function is used to reset it back to 1 once the power pellet is done.
     */
    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }
}


