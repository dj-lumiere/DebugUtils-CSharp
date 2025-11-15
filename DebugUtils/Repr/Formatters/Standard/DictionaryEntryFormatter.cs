using System.Collections;
using System.Text.Json.Nodes;
using DebugUtils.Repr.Attributes;
using DebugUtils.Repr.Interfaces;
using DebugUtils.Repr.TypeHelpers;

namespace DebugUtils.Repr.Formatters;

[ReprFormatter(typeof(DictionaryEntry))]
[ReprOptions(needsPrefix: true)]
internal class DictionaryEntryFormatter : IReprFormatter, IReprTreeFormatter
{
    public string ToRepr(object obj, ReprContext context)
    {
        var dictEntry = (DictionaryEntry)obj;
        return
            $"{dictEntry.Key.ToRepr(context: context)}: {dictEntry.Value.ToRepr(context: context)}";
    }
    public JsonNode ToReprTree(object obj, ReprContext context)
    {
        var dictEntry = (DictionaryEntry)obj;
        return new JsonObject
        {
            { "type", "DictionaryEntry" },
            { "kind", "struct" },
            { "keyType", dictEntry.Key.GetReprTypeName() },
            { "valueType", dictEntry.Key.GetReprTypeName() },
            { "key", dictEntry.Key.ToReprTree(context: context) },
            { "value", dictEntry.Value.ToReprTree(context: context) }
        };
    }
}

[ReprOptions(needsPrefix: true)]
internal class KeyValuePairFormatter : IReprFormatter, IReprTreeFormatter
{
    public string ToRepr(object obj, ReprContext context)
    {
        var type = obj.GetType();
        var keyProp = type.GetProperty("Key")!;
        var valueProp = type.GetProperty("Value")!;
        var key = keyProp.GetValue(obj);
        var value = valueProp.GetValue(obj);

        return $"{key.Repr(context)}: {value.Repr(context)}";
    }
    
    public JsonNode ToReprTree(object obj, ReprContext context)
    {
        var type = obj.GetType();
        var keyProp = type.GetProperty("Key")!;
        var valueProp = type.GetProperty("Value")!;
        var key = keyProp.GetValue(obj);
        var value = valueProp.GetValue(obj);
        return new JsonObject
        {
            { "type", "KeyValuePair" },
            { "kind", "struct" },
            { "keyType", key.GetReprTypeName() },
            { "valueType", value.GetReprTypeName() },
            { "key", key.ToReprTree(context: context) },
            { "value", value.ToReprTree(context: context) }
        };
    }
}