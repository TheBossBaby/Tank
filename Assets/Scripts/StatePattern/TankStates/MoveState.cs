using UnityEngine;

public class MoveState : TankState
{
    private Complete.TankMovement _tankMovement;

    public MoveState(Transform _tank) : base(_tank)
    {
        _tankMovement = _tank.GetComponent<Complete.TankMovement>();
    }
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        // When the tank is turned on, make sure it's not kinematic.
        _tankMovement.Rigidbody.isKinematic = false;
        _tankMovement.MovementAudio.clip = _tankMovement.m_EngineDriving;
        _tankMovement.m_MovementAudio.Play();
    }
    public override void FixedTick()
    {
        base.FixedTick();
        Move();
        Turn();
    }
    public override void Tick()
    {
        if (Mathf.Abs(_tankMovement.MovementInputValue) < 0.1f && Mathf.Abs(_tankMovement.TurnInputValue) < 0.1f)
        {
            _tankMovement.SetState(new IdealState(_transform));
        }
        SetEngineAudioPitch();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
    private void SetEngineAudioPitch()
    {
        _tankMovement.m_MovementAudio.pitch = Random.Range(_tankMovement.OriginalPitch - _tankMovement.m_PitchRange, _tankMovement.OriginalPitch + _tankMovement.m_PitchRange);
    }
    private void Move()
    {
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = _transform.forward * _tankMovement.MovementInputValue * _tankMovement.m_Speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        _tankMovement.Rigidbody.MovePosition(_tankMovement.Rigidbody.position + movement);
    }


    private void Turn()
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = _tankMovement.TurnInputValue * _tankMovement.m_TurnSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation.
        _tankMovement.Rigidbody.MoveRotation(_tankMovement.Rigidbody.rotation * turnRotation);
    }
}
