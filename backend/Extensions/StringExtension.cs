namespace backend.Extensions;

public static class StringExtension
{
    public static string GetSlug(this string title)
    {
        var slug = title.ToLowerInvariant();
        slug = slug.Replace(" ", "-");

        // Remove caracteres inválidos
        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9\-]", "");

        // Remove múltiplos hífens consecutivos
        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"\-{2,}", "-");

        // Remove hífens no início ou final (se houver)
        slug = slug.Trim('-');

        return slug;
    }
}