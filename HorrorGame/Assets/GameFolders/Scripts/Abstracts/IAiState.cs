public interface IAiState
{
    AiStateId StateId { get; }
    void Enter();
    void Update();
    void Exit();

}
