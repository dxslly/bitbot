using System;
using System.Collections.Generic;
using System.Linq;

namespace Entitas.CodeGenerator
{
    public class ComponentIndicesGenerator : IPoolCodeGenerator, IComponentCodeGenerator
    {

        // Important: This method should be called before Generate(componentInfos)
        // This will generate empty lookups for all pools.
        public CodeGenFile[] Generate(string[] poolNames)
        {
            var emptyInfos = new ComponentInfo[0];
            if (poolNames.Length == 0)
            {
                poolNames = new[] { string.Empty };
            }
            return poolNames
                .Select(poolName => poolName + CodeGenerator.DEFAULT_COMPONENT_LOOKUP_TAG)
                .Select(lookupTag => new CodeGenFile
                {
                    fileName = lookupTag,
                    fileContent = generateIndicesLookup(lookupTag, emptyInfos).ToUnixLineEndings()
                }).ToArray();
        }

        // Important: This method should be called after Generate(poolNames)
        // This will overwrite the empty lookups with the actual content.
        public CodeGenFile[] Generate(ComponentInfo[] componentInfos)
        {
            var orderedComponentInfos = componentInfos.OrderBy(info => info.typeName).ToArray();
            var lookupTagToComponentInfosMap = getLookupTagToComponentInfosMap(orderedComponentInfos);
            return lookupTagToComponentInfosMap
                .Select(kv => new CodeGenFile
                {
                    fileName = kv.Key,
                    fileContent = generateIndicesLookup(kv.Key, kv.Value.ToArray()).ToUnixLineEndings()
                }).ToArray();
        }

        static Dictionary<string, ComponentInfo[]> getLookupTagToComponentInfosMap(ComponentInfo[] componentInfos)
        {
            var currentIndex = 0;
            var orderedComponentInfoToLookupTagsMap = componentInfos
                .Where(info => info.generateIndex)
                .ToDictionary(info => info, info => info.ComponentLookupTags())
                .OrderByDescending(kv => kv.Value.Length);

            return orderedComponentInfoToLookupTagsMap
                .Aggregate(new Dictionary<string, ComponentInfo[]>(), (map, kv) =>
                {
                    var info = kv.Key;
                    var lookupTags = kv.Value;
                    var incrementIndex = false;
                    foreach (var lookupTag in lookupTags)
                    {
                        if (!map.ContainsKey(lookupTag))
                        {
                            map.Add(lookupTag, new ComponentInfo[componentInfos.Length]);
                        }

                        var infos = map[lookupTag];
                        if (lookupTags.Length == 1)
                        {
                            // Component has only one lookupTag. Insert at next free slot.
                            for (int i = 0; i < infos.Length; i++)
                            {
                                if (infos[i] == null)
                                {
                                    infos[i] = info;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            // Component has multiple lookupTags. Set at current index in all lookups.
                            infos[currentIndex] = info;
                            incrementIndex = true;
                        }
                    }
                    if (incrementIndex)
                    {
                        currentIndex++;
                    }
                    return map;
                });
        }

        static string generateIndicesLookup(string lookupTag, ComponentInfo[] componentInfos)
        {
            return addUsings()
                    + addClassHeader(lookupTag)
                    + addIndices(componentInfos)
                    + addComponentNames(componentInfos)
                    + addComponentTypes(componentInfos)
                    + addReadEntityGrid()
                    + addSerilize(componentInfos)
                    + addDeserialize(componentInfos)
                    + addCloseClass();
        }

        static string addUsings()
        {
            return "using Entitas;\r\nusing BitBots.BitBomber;\r\n";
        }

        static string getWriteTypeFormat(ComponentFieldInfo field)
        {
            switch (field.type)
            {
                case "System.Int32":
                case "int":
                case "string":
                    return "writer.Write((component as {0}).{1});";
                case "Entitas.Entity":
                    return "writer.Write((component as {0}).{1}.synchronized.id);";
                case "Entitas.Entity[,]":
                    return @"
                    writer.Write((component as {0}).{1}.GetLength(0));
                    writer.Write((component as {0}).{1}.GetLength(1));
                    foreach (var e in (component as {0}).{1})
                        writer.Write(e.synchronized.id);";
            }
            return "UnityEngine.Debug.LogWarning(\"Cannot write type {2}\");";
        }
        static string addReadEntityGrid()
        {
            return @"
    public static Entity[,] ReadEntityGrid(System.IO.BinaryReader reader)
    {
        var a = reader.ReadInt32();
        var b = reader.ReadInt32();
        var result = new Entity[a, b];
        for (int i = 0; i < a * b; ++i)
            result[i % a, i / a] = Pools.core.GetEntityById(reader.ReadInt32());
        return result;
    }";
        }
        static string getReadTypeFormat(ComponentFieldInfo field)
        {
            switch (field.type)
            {
                case "System.Int32":
                case "int":
                    return "{0} = reader.ReadInt32(),";
                case "string":
                    return "{0} = reader.ReadString(),";
                case "Entitas.Entity":
                    return "{0} = Pools.core.GetEntityById(reader.ReadInt32()),";
                case "Entitas.Entity[,]":
                    return @"{0} = ReadEntityGrid(reader),";
            }
            return "//Cannot read type " + field.type;
        }
        static string addDeserialize(ComponentInfo[] componentInfos)
        {
            try
            {
                List<string> cases = new List<string>();
                for (int i = 0; i < componentInfos.Length; ++i)
                {
                    string values = "";
                    foreach (var field in componentInfos[i].fieldInfos)
                    {
                        values += string.Format(@" 
                " + getReadTypeFormat(field), field.name);

                    }
                    cases.Add(string.Format(@"
        if (cId == {1})
        {{
            return new {0}
            {{{2}
            }};
        }}", componentInfos[i].fullTypeName, componentInfos[i].typeName.RemoveComponentSuffix(), values));
                }
                return string.Format(@"
    public static IComponent Deserialize(System.IO.BinaryReader reader, out int cId)
    {{
        cId = reader.ReadInt32();
{0}
        return null;
    }}", string.Join("\r\n", cases.ToArray()));
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError("addDeserialize threw:" + ex.Message);
                return "";
            }
        }
        static string addSerilize(ComponentInfo[] componentInfos)
        {
            try
            {
                List<string> cases = new List<string>();
                for (int i = 0; i < componentInfos.Length; ++i)
                {
                    string values = "";
                    foreach (var field in componentInfos[i].fieldInfos)
                    {
                        values += string.Format(@" 
            " + getWriteTypeFormat(field), componentInfos[i].fullTypeName, field.name, field.type);

                    }
                    cases.Add(string.Format(@"
        if (component is {0})
        {{
            writer.Write({1});{2}
        }}", componentInfos[i].fullTypeName, componentInfos[i].typeName.RemoveComponentSuffix(), values));
                }
                return string.Format(@"

    // Serialize Functions
    public static void Serialize(IComponent component, System.IO.BinaryWriter writer) 
    {{
{0}
    }}", string.Join("\r\n", cases.ToArray()));
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError("addSerialize threw:" + ex.Message);
                return "";
            }
        }

        static string addClassHeader(string lookupTag)
        {
            return string.Format("public static class {0} {{\n", lookupTag);
        }

        static string addIndices(ComponentInfo[] componentInfos)
        {
            const string fieldFormat = "    public const int {0} = {1};\n";
            const string totalFormat = "    public const int TotalComponents = {0};";
            var code = string.Empty;
            for (int i = 0; i < componentInfos.Length; i++)
            {
                var info = componentInfos[i];
                if (info != null)
                {
                    code += string.Format(fieldFormat, info.typeName.RemoveComponentSuffix(), i);
                }
            }

            var totalComponents = string.Format(totalFormat, componentInfos.Count(info => info != null));
            return code + "\n" + totalComponents;
        }

        static string addComponentNames(ComponentInfo[] componentInfos)
        {
            const string format = "        \"{1}\",\n";
            var code = string.Empty;
            for (int i = 0; i < componentInfos.Length; i++)
            {
                var info = componentInfos[i];
                if (info != null)
                {
                    code += string.Format(format, i, info.typeName.RemoveComponentSuffix());
                }
            }
            if (code.EndsWith(",\n"))
            {
                code = code.Remove(code.Length - 2) + "\n";
            }

            return string.Format(@"

    public static readonly string[] componentNames = {{
{0}    }};", code);
        }

        static string addComponentTypes(ComponentInfo[] componentInfos)
        {
            const string format = "        typeof({1}),\n";
            var code = string.Empty;
            for (int i = 0; i < componentInfos.Length; i++)
            {
                var info = componentInfos[i];
                if (info != null)
                {
                    code += string.Format(format, i, info.fullTypeName);
                }
            }
            if (code.EndsWith(",\n"))
            {
                code = code.Remove(code.Length - 2) + "\n";
            }

            return string.Format(@"

    public static readonly System.Type[] componentTypes = {{
{0}    }};", code);
        }

        static string addCloseClass()
        {
            return "\n}";
        }
    }
}