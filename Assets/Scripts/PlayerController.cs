using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    [SerializeField] private float _speed;

    [SerializeField] private float _jumpHeight;
    [SerializeField] private int _jumpCount;
    [SerializeField] private int _jumpReset;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _checkRadius;

    private Vector2 _moveDirection;


    private Rigidbody2D _rb;

    private bool isOnGround;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _inputReader.MoveEvent += HandleMove;
        _inputReader.JumpEvent += Jump;

    }

    private void Update()
    {
        GroundCheck();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void HandleMove(Vector2 direction)
    {
        _moveDirection = direction;
    }

    private void Move()
    {
        if (_moveDirection == Vector2.zero)
        {
            return;
        }
        _rb.velocity = new Vector2(_moveDirection.x * _speed, _rb.velocity.y);
    }
    private void Jump()
    {
        if (_jumpCount > 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpHeight);
            _jumpCount--;
        }
    }

    private void GroundCheck()
    {
        isOnGround = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _groundLayer);
        if (isOnGround)
        {
            _jumpCount = _jumpReset;
        }
    }
}
