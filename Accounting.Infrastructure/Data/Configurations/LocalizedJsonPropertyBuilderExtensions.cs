using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Accounting.Infrastructure.Data.Configurations;

public static class LocalizedJsonPropertyBuilderExtensions
{
    private const string EmptyJsonObject = "{}";

    public static PropertyBuilder<Dictionary<string, string>> HasLocalizedJson(
        this PropertyBuilder<Dictionary<string, string>> propertyBuilder)
    {
        var comparer = new ValueComparer<Dictionary<string, string>>(
            (left, right) => AreEqual(left, right),
            value => GetHashCode(value),
            value => Snapshot(value));

        var configured = propertyBuilder.HasConversion(
            value => Serialize(value),
            value => Deserialize(value));

        configured.Metadata.SetValueComparer(comparer);
        return configured;
    }

    private static string Serialize(Dictionary<string, string>? value)
        => value is null ? EmptyJsonObject : JsonSerializer.Serialize(value);

    private static Dictionary<string, string> Deserialize(string? value)
        => string.IsNullOrWhiteSpace(value)
            ? new Dictionary<string, string>()
            : JsonSerializer.Deserialize<Dictionary<string, string>>(value) ?? new Dictionary<string, string>();

    private static bool AreEqual(Dictionary<string, string>? left, Dictionary<string, string>? right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        if (left is null || right is null || left.Count != right.Count)
        {
            return false;
        }

        foreach (var pair in left)
        {
            if (!right.TryGetValue(pair.Key, out var value) || value != pair.Value)
            {
                return false;
            }
        }

        return true;
    }

    private static int GetHashCode(Dictionary<string, string>? value)
    {
        if (value is null || value.Count == 0)
        {
            return 0;
        }

        var hash = new HashCode();
        foreach (var pair in value.OrderBy(x => x.Key, StringComparer.Ordinal))
        {
            hash.Add(pair.Key, StringComparer.Ordinal);
            hash.Add(pair.Value, StringComparer.Ordinal);
        }

        return hash.ToHashCode();
    }

    private static Dictionary<string, string> Snapshot(Dictionary<string, string>? value)
        => value is null ? new Dictionary<string, string>() : value.ToDictionary(x => x.Key, x => x.Value, StringComparer.Ordinal);
}
