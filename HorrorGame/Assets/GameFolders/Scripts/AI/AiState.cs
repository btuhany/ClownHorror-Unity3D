public interface AiState
{
    AiStateId GetId(); 
    void Enter(AiEnemy enemy);
    void Update();
    void Exit(AiEnemy enemy);

}
