using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Accounting.Infrastructure.Data.Configurations;

public static class LocalizedJsonPropertyBuilderExtensions
{
    public static PropertyBuilder<Dictionary<string, string>> HasLocalizedJson(
        this PropertyBuilder<Dictionary<string, string>> propertyBuilder)
    {
        var comparer = new ValueComparer<Dictionary<string, string>>(
            (left, right) => Serialize(left) == Serialize(right),
            value => Serialize(value).GetHashCode(),
            value => Deserialize(Serialize(value)));

        var configured = propertyBuilder.HasConversion(
            value => Serialize(value),
            value => Deserialize(value));

        configured.Metadata.SetValueComparer(comparer);
        return configured;
    }

    private static string Serialize(Dictionary<string, string>? value)
        => JsonSerializer.Serialize(value ?? new Dictionary<string, string>());

    private static Dictionary<string, string> Deserialize(string? value)
        => string.IsNullOrWhiteSpace(value)
            ? new Dictionary<string, string>()
            : JsonSerializer.Deserialize<Dictionary<string, string>>(value) ?? new Dictionary<string, string>();
}
