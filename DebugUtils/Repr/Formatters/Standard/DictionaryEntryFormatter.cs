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