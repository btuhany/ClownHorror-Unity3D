//AiStateId ===> Enum
//AiStateIdArr ===> Enum array
public class AiStateMachine 
{
    private AiEnemy Ai;
    private AiState[] _states; //Enum's array that can used to switch between states.
    private AiStateId _currentState;  //Assigned at ChangeState
    public AiStateMachine(AiEnemy enemy)   //Constructed at AiEnemy (gameobject).
    {
        Ai = enemy;
        CreateEnumArray();
    }
    public void ChangeState(AiStateId newState) 
    {
        GetState(_currentState)?.Exit(Ai);  //trigger the exit function of the current state.
        _currentState = newState;           //assign the new state
        GetState(_currentState)?.Enter(Ai); //trigger the enter function of the current state.
    }
    public void RegisterState(AiState state) //used in the AiEnemy (gameobject). Example: _stateMachine.RegisterState(new AiChasePlayerState());
    {
        int index = (int)state.GetId();           // state.GetId() ==> returns enum. 
        _states[index] = state;                   // Find the state from _states array.
    }
    public void Update()
    {
        GetState(_currentState)?.Update();
    }
    private AiState GetState(AiStateId stateId)  //From the given enum, returns the matching state in the class from arr
    {
        int index = (int)stateId;
        return _states[index];
    }
    private void CreateEnumArray()
    {
        int numStates = System.Enum.GetNames(typeof(AiStateId)).Length;
        _states = new AiState[numStates];
    }
}
