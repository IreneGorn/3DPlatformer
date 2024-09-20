using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour, IPlayerController
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth;
    
    private Rigidbody _rb;
    private bool _isGrounded;
    private bool _canMove = true;
    
    public event UnityAction OnPlayerDeath;
    public event UnityAction OnPlayerWin;
    public event UnityAction<int> OnPlayerHealthChanged;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        ResetPlayer();
    }

    private void Update()
    {
        if (_canMove)
        {
            HandleMovement();
            HandleJump();
        }
    }

    private void DisableControls()
    {
        _canMove = false;
    }

    private void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        _rb.MovePosition(transform.position + movement * _moveSpeed * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isGrounded = true;

        Debug.Log("Player collided with " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Player's dead!");
            PlayerDeath();
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Player's win!");
            OnPlayerWin?.Invoke();
            DisableControls();
        }
    }

    public void ResetPlayer()
    {
        _currentHealth = _maxHealth;
        transform.position = new Vector3(0,5,0);
        
        OnPlayerHealthChanged?.Invoke(_currentHealth);
        
        _canMove = true;
    }

    public int GetHealth()
    {
        return _currentHealth;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        OnPlayerHealthChanged?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        OnPlayerDeath?.Invoke();
        DisableControls();
    }
}
