using Entitas;
using BitBots.BitBomber;
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
    public const int Synchronized = 16;
    public const int TilePosition = 17;
    public const int View = 18;

    public const int TotalComponents = 19;

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
        "Synchronized",
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
        typeof(BitBots.BitBomber.Features.Synchronized.SynchronizedComponent),
        typeof(BitBots.BitBomber.Features.Tiles.TilePositionComponent),
        typeof(BitBots.BitBomber.Features.View.ViewComponent)
    };
    public static Entity[,] ReadEntityGrid(System.IO.BinaryReader reader)
    {
        var a = reader.ReadInt32();
        var b = reader.ReadInt32();
        var result = new Entity[a, b];
        for (int i = 0; i < a * b; ++i)
            result[i % a, i / a] = Pools.core.GetEntityById(reader.ReadInt32());
        return result;
    }

    // Serialize Functions
    public static void Serialize(IComponent component, System.IO.BinaryWriter writer) 
    {

        if (component is BitBots.BitBomber.Features.Bomb.BombComponent)
        {
            writer.Write(Bomb); 
            writer.Write((component as BitBots.BitBomber.Features.Bomb.BombComponent).spread);
        }

        if (component is BitBots.BitBomber.Features.Collideable.CollideableComponent)
        {
            writer.Write(Collideable);
        }

        if (component is BitBots.BitBomber.Features.Damageable.DamageableComponent)
        {
            writer.Write(Damageable);
        }

        if (component is BitBots.BitBomber.Features.Damageable.DamagerComponent)
        {
            writer.Write(Damager); 
            writer.Write((component as BitBots.BitBomber.Features.Damageable.DamagerComponent).amount);
        }

        if (component is BitBots.BitBomber.Features.Destroyable.DestroyableComponent)
        {
            writer.Write(Destroyable);
        }

        if (component is BitBots.BitBomber.Features.Expireable.ExpireableComponent)
        {
            writer.Write(Expireable); 
            writer.Write((component as BitBots.BitBomber.Features.Expireable.ExpireableComponent).remainingTicksToLive);
        }

        if (component is BitBots.BitBomber.Features.GameBoard.GameBoardCacheComponent)
        {
            writer.Write(GameBoardCache); 
            
                    writer.Write((component as BitBots.BitBomber.Features.GameBoard.GameBoardCacheComponent).grid.GetLength(0));
                    writer.Write((component as BitBots.BitBomber.Features.GameBoard.GameBoardCacheComponent).grid.GetLength(1));
                    foreach (var e in (component as BitBots.BitBomber.Features.GameBoard.GameBoardCacheComponent).grid)
                        writer.Write(e.synchronized.id);
        }

        if (component is BitBots.BitBomber.Features.GameBoard.GameBoardComponent)
        {
            writer.Write(GameBoard); 
            writer.Write((component as BitBots.BitBomber.Features.GameBoard.GameBoardComponent).width); 
            writer.Write((component as BitBots.BitBomber.Features.GameBoard.GameBoardComponent).height);
        }

        if (component is BitBots.BitBomber.Features.GameBoard.GameBoardElementComponent)
        {
            writer.Write(GameBoardElement);
        }

        if (component is BitBots.BitBomber.Features.GameTick.GameTickComponent)
        {
            writer.Write(GameTick); 
            writer.Write((component as BitBots.BitBomber.Features.GameTick.GameTickComponent).turn);
        }

        if (component is BitBots.BitBomber.Features.Damageable.HealthComponent)
        {
            writer.Write(Health); 
            writer.Write((component as BitBots.BitBomber.Features.Damageable.HealthComponent).health);
        }

        if (component is BitBots.BitBomber.Features.Movement.MoveComponent)
        {
            writer.Write(Move); 
            UnityEngine.Debug.LogWarning("Cannot write type BitBots.BitBomber.Features.Movement.MoveDirection");
        }

        if (component is BitBots.BitBomber.Features.Owner.OwnerComponent)
        {
            writer.Write(Owner); 
            writer.Write((component as BitBots.BitBomber.Features.Owner.OwnerComponent).owner.synchronized.id);
        }

        if (component is BitBots.BitBomber.Features.PlayerAI.PlayerAIComponent)
        {
            writer.Write(PlayerAI); 
            UnityEngine.Debug.LogWarning("Cannot write type BitBots.BitBomber.Features.PlayerAI.BLScript");
        }

        if (component is BitBots.BitBomber.Features.Player.PlayerComponent)
        {
            writer.Write(Player); 
            writer.Write((component as BitBots.BitBomber.Features.Player.PlayerComponent).playerID);
        }

        if (component is BitBots.BitBomber.Features.View.PrefabComponent)
        {
            writer.Write(Prefab); 
            writer.Write((component as BitBots.BitBomber.Features.View.PrefabComponent).path);
        }

        if (component is BitBots.BitBomber.Features.Synchronized.SynchronizedComponent)
        {
            writer.Write(Synchronized); 
            writer.Write((component as BitBots.BitBomber.Features.Synchronized.SynchronizedComponent).id);
        }

        if (component is BitBots.BitBomber.Features.Tiles.TilePositionComponent)
        {
            writer.Write(TilePosition); 
            writer.Write((component as BitBots.BitBomber.Features.Tiles.TilePositionComponent).x); 
            writer.Write((component as BitBots.BitBomber.Features.Tiles.TilePositionComponent).y);
        }

        if (component is BitBots.BitBomber.Features.View.ViewComponent)
        {
            writer.Write(View); 
            UnityEngine.Debug.LogWarning("Cannot write type UnityEngine.GameObject");
        }
    }
    public static IComponent Deserialize(System.IO.BinaryReader reader, out int cId)
    {
        cId = reader.ReadInt32();

        if (cId == Bomb)
        {
            return new BitBots.BitBomber.Features.Bomb.BombComponent
            { 
                spread = reader.ReadInt32(),
            };
        }

        if (cId == Collideable)
        {
            return new BitBots.BitBomber.Features.Collideable.CollideableComponent
            {
            };
        }

        if (cId == Damageable)
        {
            return new BitBots.BitBomber.Features.Damageable.DamageableComponent
            {
            };
        }

        if (cId == Damager)
        {
            return new BitBots.BitBomber.Features.Damageable.DamagerComponent
            { 
                amount = reader.ReadInt32(),
            };
        }

        if (cId == Destroyable)
        {
            return new BitBots.BitBomber.Features.Destroyable.DestroyableComponent
            {
            };
        }

        if (cId == Expireable)
        {
            return new BitBots.BitBomber.Features.Expireable.ExpireableComponent
            { 
                remainingTicksToLive = reader.ReadInt32(),
            };
        }

        if (cId == GameBoardCache)
        {
            return new BitBots.BitBomber.Features.GameBoard.GameBoardCacheComponent
            { 
                grid = ReadEntityGrid(reader),
            };
        }

        if (cId == GameBoard)
        {
            return new BitBots.BitBomber.Features.GameBoard.GameBoardComponent
            { 
                width = reader.ReadInt32(), 
                height = reader.ReadInt32(),
            };
        }

        if (cId == GameBoardElement)
        {
            return new BitBots.BitBomber.Features.GameBoard.GameBoardElementComponent
            {
            };
        }

        if (cId == GameTick)
        {
            return new BitBots.BitBomber.Features.GameTick.GameTickComponent
            { 
                turn = reader.ReadInt32(),
            };
        }

        if (cId == Health)
        {
            return new BitBots.BitBomber.Features.Damageable.HealthComponent
            { 
                health = reader.ReadInt32(),
            };
        }

        if (cId == Move)
        {
            return new BitBots.BitBomber.Features.Movement.MoveComponent
            { 
                //Cannot read type BitBots.BitBomber.Features.Movement.MoveDirection
            };
        }

        if (cId == Owner)
        {
            return new BitBots.BitBomber.Features.Owner.OwnerComponent
            { 
                owner = Pools.core.GetEntityById(reader.ReadInt32()),
            };
        }

        if (cId == PlayerAI)
        {
            return new BitBots.BitBomber.Features.PlayerAI.PlayerAIComponent
            { 
                //Cannot read type BitBots.BitBomber.Features.PlayerAI.BLScript
            };
        }

        if (cId == Player)
        {
            return new BitBots.BitBomber.Features.Player.PlayerComponent
            { 
                playerID = reader.ReadInt32(),
            };
        }

        if (cId == Prefab)
        {
            return new BitBots.BitBomber.Features.View.PrefabComponent
            { 
                path = reader.ReadString(),
            };
        }

        if (cId == Synchronized)
        {
            return new BitBots.BitBomber.Features.Synchronized.SynchronizedComponent
            { 
                id = reader.ReadInt32(),
            };
        }

        if (cId == TilePosition)
        {
            return new BitBots.BitBomber.Features.Tiles.TilePositionComponent
            { 
                x = reader.ReadInt32(), 
                y = reader.ReadInt32(),
            };
        }

        if (cId == View)
        {
            return new BitBots.BitBomber.Features.View.ViewComponent
            { 
                //Cannot read type UnityEngine.GameObject
            };
        }
        return null;
    }
}