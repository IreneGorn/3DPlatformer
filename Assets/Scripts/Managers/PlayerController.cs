using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour, IPlayerController
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private int maxHealth = 100;

    private int currentHealth;
    private Rigidbody rb;
    private bool isGrounded;
    
    public event UnityAction OnPlayerDeath;
    public event UnityAction<int> OnPlayerHealthChanged;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ResetPlayer();
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player collided with " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("Player's dead!");
            OnPlayerDeath?.Invoke();
        }
    }

    public void ResetPlayer()
    {
        currentHealth = maxHealth;
        transform.position = new Vector3(0,5,0);
        
        OnPlayerHealthChanged?.Invoke(currentHealth);
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        OnPlayerHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            OnPlayerDeath?.Invoke();
        }
    }
}
