using Entitas;
using System;
using UnityEngine;

namespace BitBots.BitBomber.Features.PlayerAI
{
    public interface IPlayerAI
    {
        void OnGameTick();
    }
}