using UnityEngine;

namespace CoreSystem.StateMachine
{
    public abstract class BaseStateMachine : MonoBehaviour
    {
        protected IBaseState _currState;
        
        public void SetState(IBaseState newState)
        {
            if (_currState != null) _currState.OnStateExit(this);
            _currState = newState;
            _currState.OnStateEnter(this);
        }

        private void Update()
        {
            if (_currState != null)
            {
                _currState.UpdateState(this);
            }
        }
    }
}