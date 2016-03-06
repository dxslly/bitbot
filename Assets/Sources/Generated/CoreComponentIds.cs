public static class CoreComponentIds {
    public const int Bomb = 0;
    public const int Collideable = 1;
    public const int Damageable = 2;
    public const int Damager = 3;
    public const int Destroyable = 4;
    public const int Expireable = 5;
    public const int GameBoardCache = 6;
    public const int GameBoard = 7;
    public const int GameBoardElement = 8;
    public const int GameTick = 9;
    public const int Health = 10;
    public const int Move = 11;
    public const int Owner = 12;
    public const int PlayerAI = 13;
    public const int Player = 14;
    public const int Prefab = 15;
    public const int TilePosition = 16;
    public const int View = 17;

    public const int TotalComponents = 18;

    public static readonly string[] componentNames = {
        "Bomb",
        "Collideable",
        "Damageable",
        "Damager",
        "Destroyable",
        "Expireable",
        "GameBoardCache",
        "GameBoard",
        "GameBoardElement",
        "GameTick",
        "Health",
        "Move",
        "Owner",
        "PlayerAI",
        "Player",
        "Prefab",
        "TilePosition",
        "View"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(BitBots.BitBomber.Features.Bomb.BombComponent),
        typeof(BitBots.BitBomber.Features.Collideable.CollideableComponent),
        typeof(BitBots.BitBomber.Features.Damageable.DamageableComponent),
        typeof(BitBots.BitBomber.Features.Damageable.DamagerComponent),
        typeof(BitBots.BitBomber.Features.Destroyable.DestroyableComponent),
        typeof(BitBots.BitBomber.Features.Expireable.ExpireableComponent),
        typeof(BitBots.BitBomber.Features.GameBoard.GameBoardCacheComponent),
        typeof(BitBots.BitBomber.Features.GameBoard.GameBoardComponent),
        typeof(BitBots.BitBomber.Features.GameBoard.GameBoardElementComponent),
        typeof(BitBots.BitBomber.Features.GameTick.GameTickComponent),
        typeof(BitBots.BitBomber.Features.Damageable.HealthComponent),
        typeof(BitBots.BitBomber.Features.Movement.MoveComponent),
        typeof(BitBots.BitBomber.Features.Owner.OwnerComponent),
        typeof(BitBots.BitBomber.Features.PlayerAI.PlayerAIComponent),
        typeof(BitBots.BitBomber.Features.Player.PlayerComponent),
        typeof(BitBots.BitBomber.Features.View.PrefabComponent),
        typeof(BitBots.BitBomber.Features.Tiles.TilePositionComponent),
        typeof(BitBots.BitBomber.Features.View.ViewComponent)
    };
}