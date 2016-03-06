public static class CoreComponentIds {
    public const int Prefab = 0;
    public const int TilePosition = 1;
    public const int View = 2;

    public const int TotalComponents = 3;

    public static readonly string[] componentNames = {
        "Prefab",
        "TilePosition",
        "View"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(BitBots.BitBomber.Features.View.PrefabComponent),
        typeof(BitBots.BitBomber.Features.Tiles.TilePositionComponent),
        typeof(BitBots.BitBomber.Features.View.ViewComponent)
    };
}