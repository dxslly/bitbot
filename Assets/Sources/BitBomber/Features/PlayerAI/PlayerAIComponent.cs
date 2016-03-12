using Entitas;
using MoonSharp.Interpreter;

    

namespace BitBots.BitBomber.Features.PlayerAI
{
    public enum Command : int { INVALID, MoveUp, MoveDown, MoveRight, MoveLeft, PlantBomb, Nothing };
    public class BLScript
    {
        public BLScript()
        {
            Script = new MoonSharp.Interpreter.Script(MoonSharp.Interpreter.CoreModules.Preset_HardSandbox);
        }
        public Script Script { get; set; }

        public void SetGlobal(string name, object value)
        {
            Script.Globals[name] = value;
        }

        private static void log(string str)
        {
            UnityEngine.Debug.Log(str);
        }

        public void LoadScript(string code)
        {
            try
            {
                // Add Hooks to PlayerAI
                SetGlobal("log", (System.Action<string>)log);
                SetGlobal("MoveUp", (int)Command.MoveUp);
                SetGlobal("MoveDown", (int)Command.MoveDown);
                SetGlobal("MoveRight", (int)Command.MoveRight);
                SetGlobal("MoveLeft", (int)Command.MoveLeft);
                SetGlobal("PlantBomb", (int)Command.PlantBomb);
                SetGlobal("Nothing", (int)Command.Nothing);

                Script.DoString(code);
            }
            catch
            {
                // TODO flag script as invalid
                UnityEngine.Debug.Log("Invalid script loaded");
            }
        }
    }

    [CoreAttribute]
    public class PlayerAIComponent : IComponent
    {
        public BLScript engine;
    }
}