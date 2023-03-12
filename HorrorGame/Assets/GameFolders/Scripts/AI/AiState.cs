using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AiState
{
    AiStateId GetId(); // [SerializeField] ?
    void Enter(AiEnemy enemy);
    void Update(AiEnemy enemy);
    void Exit(AiEnemy enemy);

}
