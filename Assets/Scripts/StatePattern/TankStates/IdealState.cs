using UnityEngine;

public class IdealState : TankState
{
    private Complete.TankMovement _tankMovement;
    public IdealState(Transform _tank) : base(_tank)
    {
        _tankMovement = _tank.GetComponent<Complete.TankMovement>();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _tankMovement.MovementAudio.clip = _tankMovement.m_EngineIdling;
        _tankMovement.m_MovementAudio.Play();
    }
    public override void Tick()
    {
        SetEngineAudioPitch();
        // If there is an input (the tank is stationary)...
        if (!(Mathf.Abs(_tankMovement.MovementInputValue) < 0.1f && Mathf.Abs(_tankMovement.TurnInputValue) < 0.1f))
        {
            _tankMovement.SetState(new MoveState(_transform));
        }
    }
    private void SetEngineAudioPitch()
    {
        _tankMovement.m_MovementAudio.pitch = Random.Range(_tankMovement.OriginalPitch - _tankMovement.m_PitchRange, _tankMovement.OriginalPitch + _tankMovement.OriginalPitch - _tankMovement.m_PitchRange);
    }
}
