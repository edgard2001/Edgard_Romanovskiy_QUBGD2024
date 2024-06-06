using UnityEditor.PackageManager;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private BallRoll ball;
    [SerializeField] private float maxDistance = 30f;
    [SerializeField] private bool alwaysRoll;
    private Vector3 lastVelocity = Vector3.zero;
    [SerializeField] private float speed = 20f;

    private Vector3 _velocity = Vector3.zero;
    public Vector3 Velocity { get { return playerRb.velocity; } }

    private Rigidbody playerRb;
    [SerializeField] private float jumpSpeedMultiplier = 1.5f;

    private Vector3 startPosition;
    private bool _jumping;
    private bool _grounded;
    private float _jumpCooldown = 0;
    [SerializeField] private float maxJumpCooldown = 5f;
    [SerializeField] private float jumpForce;

    [SerializeField] private Material material;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        playerRb = GetComponent<Rigidbody>();
        material.SetColor("_EmissionColor", Color.white);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _velocity = new Vector3(input.x, 0, input.y);

        if (!_grounded)
        {
            _velocity = lastVelocity;
        }
        else if (alwaysRoll)
        {
            if (_velocity.sqrMagnitude == 0)
                _velocity = lastVelocity;
            else
                _velocity.Normalize();
        }
        else
        {
            if (_velocity.sqrMagnitude > 1)
                _velocity.Normalize();
        }

        lastVelocity = _velocity;
        _velocity *= speed;

        if (_jumpCooldown > 0)
            _jumpCooldown -= Time.deltaTime;


        if (Physics.Raycast(transform.position, Vector3.down, 1f))
        {
            _grounded = true;
            if (Input.GetButtonDown("Jump") && _jumpCooldown <= 0)
            {
                _jumpCooldown = maxJumpCooldown;
                _jumping = true;
            }
        } 
        else
        {
            _grounded = false;
            _velocity *= jumpSpeedMultiplier;
        }

        //material.color = Color.Lerp(Color.white, Color.black, _jumpCooldown / maxJumpCooldown);
        if (_jumpCooldown >= 0)
            material.SetColor("_EmissionColor", Color.Lerp(Color.grey, Color.black, _jumpCooldown / maxJumpCooldown));
        else
            material.SetColor("_EmissionColor", Color.white);
    }

    void FixedUpdate()
    {
        playerRb.velocity = Vector3.zero + Vector3.up * playerRb.velocity.y;
        playerRb.velocity += _velocity;
        ball.SetDirection(playerRb.velocity);

        if (_jumping)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _jumping = false;
        }

        if (transform.position.y < -3)
            transform.position = startPosition;
    }
}
