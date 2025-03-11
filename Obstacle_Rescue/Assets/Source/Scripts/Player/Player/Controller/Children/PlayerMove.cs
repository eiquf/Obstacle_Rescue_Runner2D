using System;
using UnityEngine;

[Serializable]
public sealed class PlayerMove : PlayerSystem
{
    private float _height { get; set; }
    private float _acceleration = 10f;
    private float _holdJumpTimer = 0.0f;
    private float _maxHoldJumpTime = 0.4f;

    private bool _isHoldingJump = false;
    private GroundFall fall;

    private bool _collected;

    public PlayerMove(Player player) : base(player) { }
    public override void Execute(Transform transform)
    {
        _pos = transform.position;
        Move();
        _player.SetVelocity(_velocity);
        transform.position = _pos;
    }
    private void Move()
    {
        Run();
        Jump();
    }
    #region Run
    private void Run()
    {
        GroundCheck();

        OnTheGround();
        NotOnGround();
    }
    private void OnTheGround()
    {
        if (_isGrounded) ChangePosX();
    }
    private void GroundCheck()
    {

        Vector2 rayOrigin = new(_pos.x, _pos.y - 0.1f);
        Vector2 rayDirection = Vector2.down;
        float rayDistance = 0.2f;
        RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, _player.MovementSettings.GroundLayerMask);

        if (hit2D.collider != null)
        {
            if (hit2D.collider.TryGetComponent<Ground>(out var ground))
            {
                if (ground is GroundTrap trap)
                {
                    trap.Trap();
                    _player.OnStop?.Invoke(true);
                    _player.OnNotify?.Invoke(PlayerStates.Stop);
                    _collected = true;
                }
                else
                {
                    _collected = false;
                    _player.OnStop?.Invoke(false);
                    _player.Animation.IsStop?.Invoke(false);
                }

                _isGrounded = true;
                _height = ground.Height;
                _pos.y = _height;

                _player.SetYPos(_height, _height + _player.MovementSettings.JumpVelocity * _maxHoldJumpTime);
            }
        }
        else
            _isGrounded = false;
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
            Vector2 rayDirection = Vector2.down;
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
        _player.OnNotify?.Invoke(PlayerStates.Fall);
    }
    private void WallNotGroundedCheck()
    {
        Vector2 wallOrigin = new(_pos.x, _pos.y);
        Vector2 wallDir = new Vector2(1f, -0.5f).normalized;

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
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    _isGrounded = false;
                    _velocity.y = _player.MovementSettings.JumpVelocity;
                    _isHoldingJump = true;
                    _holdJumpTimer = 0;

                    if (fall != null)
                    {
                        fall.Initialize(_player);
                        fall = null;
                        _player.OnNotify?.Invoke(PlayerStates.Move);
                    }
                    _player.Animation.IsJump?.Invoke(true);
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    _isHoldingJump = false;
                }
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