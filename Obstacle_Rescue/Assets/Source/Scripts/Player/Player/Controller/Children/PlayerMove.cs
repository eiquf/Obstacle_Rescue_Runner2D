using System;
using UnityEngine;

[Serializable]
public sealed class PlayerMove : PlayerSystem
{
    private float _height;
    private float _acceleration = 10f;
    private float _holdJumpTimer = 0.0f;
    private float _maxHoldJumpTime = 0.4f;

    private bool _isHoldingJump = false;
    private readonly GameCamera _mainCamera;

    private GroundFall fall;

    public PlayerMove
        (Player player,
        GameCamera mainCamera) : base(player)
    {
        _mainCamera = mainCamera;
    }

    public override void Execute(Transform transform)
    {
        if (transform.position.y > -20)
        {
            _pos = transform.position;
            Move();
            _player.SetVelocity(_velocity);
            transform.position = _pos;
        }
        else _player.OnNotify(PlayerStates.Dead);
    }
    private void Move()
    {
        Run();
        Jump();
    }
    #region Run
    private void Run()
    {
        OnTheGround();
        NotOnGround();
    }
    private void OnTheGround()
    {
        if (_isGrounded)
        {
            ChangePosX();
            GroundCheck();
        }
    }
    private void GroundCheck()
    {
        Vector2 rayOrigin = new(_pos.x, _pos.y - 0.1f);
        Vector2 rayDirection = Vector2.down;
        float rayDistance = 0.5f;
        RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, _player.MovementSettings.GroundLayerMask);

        if (hit2D.collider != null)
        {
            if (hit2D.collider.TryGetComponent<Ground>(out var ground))
            {
                _isGrounded = true;
                _height = ground.Height;
                _pos.y = _height;
                _player.Animation.IsStop?.Invoke(false);
            }
        }
        else
            _isGrounded = false;

        Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);
    }

    private void ChangePosX()
    {
        float velocityRatio = _velocity.x / _player.MovementSettings.MaxXVelocity;
        _acceleration = _player.MovementSettings.MaxAcceleration * (1 - velocityRatio);
        _maxHoldJumpTime = _player.MovementSettings.MaxMaxHoldJumpTime * velocityRatio;

        _velocity.x += _acceleration * Time.fixedDeltaTime;
        if (_velocity.x >= _player.MovementSettings.MaxXVelocity)
            _velocity.x = _player.MovementSettings.MaxXVelocity;
    }
    private void NotOnGround()
    {
        if (!_isGrounded)
        {
            Vector2 rayOrigin = new(_pos.x + 0.7f, _pos.y - (_pos.y / 2));
            Vector2 rayDirection = Vector2.up;
            float rayDistance = _velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, _player.MovementSettings.GroundLayerMask);
            if (hit2D.collider != null)
            {
                if (hit2D.collider.TryGetComponent<Ground>(out var ground))
                {
                    if (_pos.y >= ground.Height)
                    {
                        _height = ground.Height;
                        _pos.y = _height;
                        _velocity.y = 0;
                        _isGrounded = true;
                    }

                    if (ground.TryGetComponent(out fall))
                        Fall();
                }
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);

            WallNotGroundedCheck();
        }
    }


    private void Fall()
    {
        fall.Initialize(_player);
        _height -= fall.FallAmount;
        _mainCamera.IsShaking?.Invoke(true);
    }
    private void WallNotGroundedCheck()
    {
        Vector2 wallOrigin = new(_pos.x, _pos.y);
        Vector2 wallDir = Vector2.right;
        RaycastHit2D wallHit = Physics2D.Raycast(wallOrigin, wallDir, _velocity.x * Time.fixedDeltaTime, _player.MovementSettings.GroundLayerMask);
        if (wallHit.collider != null)
        {
            if (wallHit.collider.TryGetComponent<Ground>(out var ground))
            {
                if (_pos.y < ground.Height)
                    _velocity.x = 0;
            }
        }
    }

    #endregion
    #region Jump
    private void Jump()
    {
        _height = Mathf.Abs(_pos.y - _height);
        JumpInput();
        HoldJump();
    }
    private void JumpInput()
    {
        if (_isGrounded || _height <= _player.MovementSettings.JumpGroundThreshold)
        {
            _player.Animation.IsJump?.Invoke(false);

            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Stationary)
                {
                    _isGrounded = false;
                    _velocity.y = _player.MovementSettings.JumpVelocity;
                    _isHoldingJump = true;
                    _holdJumpTimer = 0;

                    if (fall != null)
                    {
                        fall.Initialize(null);
                        fall = null;
                        _mainCamera.IsShaking?.Invoke(false);
                    }
                    _player.Animation.IsJump?.Invoke(true);
                }
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    _isHoldingJump = false;
            }
        }
    }
    private void HoldJump()
    {
        if (!_isGrounded)
        {
            if (_isHoldingJump)
            {
                _holdJumpTimer += Time.fixedDeltaTime;
                if (_holdJumpTimer >= _maxHoldJumpTime)
                    _isHoldingJump = false;
            }
            else _velocity.y += _player.MovementSettings.gravity * Time.fixedDeltaTime;

            _pos.y += _velocity.y * Time.fixedDeltaTime;
        }
    }
    #endregion
}