public static class CoreComponentIds {
    public const int Collideable = 0;
    public const int Damageable = 1;
    public const int GameBoardCache = 2;
    public const int GameBoardElement = 3;
    public const int GameTick = 4;
    public const int Health = 5;
    public const int Move = 6;
    public const int PlayerAI = 7;
    public const int Player = 8;
    public const int Prefab = 9;
    public const int TilePosition = 10;
    public const int View = 11;

    public const int TotalComponents = 12;

    public static readonly string[] componentNames = {
        "Collideable",
        "Damageable",
        "GameBoardCache",
        "GameBoardElement",
        "GameTick",
        "Health",
        "Move",
        "PlayerAI",
        "Player",
        "Prefab",
        "TilePosition",
        "View"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(BitBots.BitBomber.Features.Collideable.CollideableComponent),
        typeof(BitBots.BitBomber.Features.Damageable.DamageableComponent),
        typeof(BitBots.BitBomber.Features.GameBoard.GameBoardCacheComponent),
        typeof(BitBots.BitBomber.Features.GameBoard.GameBoardElementComponent),
        typeof(BitBots.BitBomber.Features.GameTick.GameTickComponent),
        typeof(BitBots.BitBomber.Features.Damageable.HealthComponent),
        typeof(BitBots.BitBomber.Features.Movement.MoveComponent),
        typeof(BitBots.BitBomber.Features.PlayerAI.PlayerAIComponent),
        typeof(BitBots.BitBomber.Features.Player.PlayerComponent),
        typeof(BitBots.BitBomber.Features.View.PrefabComponent),
        typeof(BitBots.BitBomber.Features.Tiles.TilePositionComponent),
        typeof(BitBots.BitBomber.Features.View.ViewComponent)
    };
}