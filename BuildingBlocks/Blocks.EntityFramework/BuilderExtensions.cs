/* 
PSEUDOCODE / PLAN:
- Modify the JSON deserialization lambda to avoid throwing an exception when JsonSerializer.Deserialize returns null.
- Steps:
  1. Attempt to deserialize the input string (or "[]" when input is null) into TCollection.
  2. If the deserialized result is non-null, return it.
  3. If the deserialized result is null, return default(TCollection) (using null-forgiving operator to satisfy nullable reference types).
  4. Ensure the method signature and returned ValueConverter remain unchanged.
- Rationale:
  - This keeps behavior safe (no exceptions on null deserialization) and avoids risky reflection/instance creation for arbitrary TCollection.
*/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace Blocks.EntityFrameworkCore
{
    public static class BuilderExtensions
    {
        public static PropertyBuilder<TEnum> HasEnumConversion<TEnum>(this PropertyBuilder<TEnum> builder)
            where TEnum : struct, Enum
        {
            return builder.HasConversion(
                v => v.ToString(),
                v => Enum.Parse<TEnum>(v)
            );
        }

        public static PropertyBuilder<T> HasJsonCollectionConversion<T>(this PropertyBuilder<T> builder)
        {
            return builder.HasConversion(BuildJsonCollectionConvertor<T>());
        }

        public static ValueConverter<TCollection, string> BuildJsonCollectionConvertor<TCollection>()
        {
            Func<TCollection, string> serializeFunc = v => JsonSerializer.Serialize(v);
            Func<string, TCollection> deserializeFunc = v => JsonSerializer.Deserialize<TCollection>(v ?? "[]") ?? default!;

            return new ValueConverter<TCollection, string>(
                v => serializeFunc(v),
                v => deserializeFunc(v));
        }

        public static PropertyBuilder<TProperty> HasColunmNameSameAsProperty<TProperty>(this PropertyBuilder<TProperty> builder)
            => builder.HasColumnName(builder.Metadata.PropertyInfo?.Name);
    }
}