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

    [SerializeField] private float _turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    private Camera _mainCamera;
    private Transform _camTransform;
    
    public event UnityAction OnPlayerDeath;
    public event UnityAction OnPlayerWin;
    public event UnityAction<int> OnPlayerHealthChanged;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _camTransform = _mainCamera.transform;
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

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;
        if (movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + _camTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
                _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _rb.MovePosition(transform.position + moveDir * _moveSpeed * Time.deltaTime);
        }
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
