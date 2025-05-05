using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPSPlayerController : MonoBehaviour
{
    // UI Elements
    public Text countText;
    public Text winText;

    // Collection variables
    private int count = 0;
    public int totalPickups = 6;  // Set this to match your number of pickups

    // Reference to the first-person camera (will be found automatically)
    private Camera playerCamera;

    // Collection parameters
    public float pickupRange = 3.0f;

    void Start()
    {
        // Find the camera in the FPSController children
        playerCamera = GetComponentInChildren<Camera>();
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        // Initialize count
        count = 0;

        // Initialize UI
        UpdateCountText();
        if (winText != null)
        {
            winText.text = "";
        }
    }

    void Update()
    {
        // Check for pickup objects in front of the player when they click or press E
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            CheckForPickup();
        }
    }

    void CheckForPickup()
    {
        if (playerCamera == null) return;

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Pick Up"))
            {
                CollectPickup(hit.collider.gameObject);
            }
        }
    }

    // Handle trigger-based pickups (when walking into objects)
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pick Up"))
        {
            CollectPickup(other.gameObject);
        }
    }

    // Handle direct collisions with the character controller
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Pick Up"))
        {
            CollectPickup(hit.gameObject);
        }
    }

    // Central method to collect a pickup
    void CollectPickup(GameObject pickup)
    {
        // Destroy the pickup object
        Destroy(pickup);

        // Increment counter
        count++;

        // Update UI
        UpdateCountText();

        // Check for win condition
        if (count >= totalPickups)
        {
            if (winText != null)
            {
                winText.text = "You Win!";
            }
        }
    }

    // Update the count display
    void UpdateCountText()
    {
        if (countText != null)
        {
            countText.text = "Count: " + count + " / " + totalPickups;
        }
    }
}