using UnityEngine;

public class CountdownChallenge : MonoBehaviour
{
    //Added SerializeField to make quick changes in the editor possible if needed later
    // How long to wait between each countdown tick (1 second)
    [SerializeField] private float waitTime = 1f;

    // Total number of seconds for the countdown
    [SerializeField] private int startSeconds = 10;

    // Reference to the cube's Renderer (for changing color)
    [SerializeField] private Renderer cubeRenderer;

    // Reference to the cube's Transform (for movement)
    [SerializeField] private Transform cubeTransform;

    // How far the cube moves each second (small amount)
    [SerializeField] private float moveDistance = 0.1f;

    private float timer;
    private int secondsLeft;

    void Start()
    {
        // Set the timer to our wait time
        timer = waitTime;

        // Set how many seconds we have left
        secondsLeft = startSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        // Add the time passed since the last frame to our timer
        timer -= Time.deltaTime;

        // Check if the timer has reached our target (1 second)
        if (timer <= 0f)
        {
            // Print how much time is left to show the countdown
            Debug.Log("Time left: " + secondsLeft);

            // Change the cube's color each second
            cubeRenderer.material.color = Random.ColorHSV();

            // Get a random direction that is not downward
            Vector3 randomDirection = Random.onUnitSphere;
            randomDirection.y = Mathf.Abs(randomDirection.y);

            // Move the cube a small amount in that direction
            cubeTransform.position += randomDirection * moveDistance;

            // Decrease the amount of time left
            secondsLeft--;

            // Reset the timer
            timer = waitTime;
        }

        // Check if the countdown has finished
        if (secondsLeft < 0)
        {
            Debug.Log("TIMES UP");

            // Disable the script so Update stops running
            enabled = false;
        }
    }
}