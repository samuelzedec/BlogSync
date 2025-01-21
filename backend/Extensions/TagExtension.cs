using backend.Models;

namespace backend.Extensions;

public static class TagExtension
{
    public static void CopyModelFrom(this Tag target, Tag source)
    {
        target.Slug = target.Slug;
        target.ModifiedAt = DateTime.UtcNow;
    }
}