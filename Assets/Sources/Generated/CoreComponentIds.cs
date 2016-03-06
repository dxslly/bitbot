public static class CoreComponentIds {
    public const int Collideable = 0;
    public const int Prefab = 1;
    public const int TilePosition = 2;
    public const int View = 3;

    public const int TotalComponents = 4;

    public static readonly string[] componentNames = {
        "Collideable",
        "Prefab",
        "TilePosition",
        "View"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(BitBots.BitBomber.Features.Collideable.CollideableComponent),
        typeof(BitBots.BitBomber.Features.View.PrefabComponent),
        typeof(BitBots.BitBomber.Features.Tiles.TilePositionComponent),
        typeof(BitBots.BitBomber.Features.View.ViewComponent)
    };
}