using System.Globalization;
using System.Net.Mail;

namespace BankSystem.Services.Helpers;

/// <summary>
/// Provides validation methods for currencies and emails.
/// </summary>
public static class ValidationService
{
    // Checks whether the string is an existing ISO currency code.
    public static bool IsCurrencyValid(this string currencyCode)
    {
        if (currencyCode is null)
        {
            return false;
        }

        // Each region knows its currency, so search all of them for a match.
        foreach (var culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
        {
            var region = new RegionInfo(culture.Name);
            if (region.ISOCurrencySymbol == currencyCode)
            {
                return true;
            }
        }

        return false;
    }

    // Checks whether the string is a valid email address.
    public static bool IsEmailValid(this string email)
    {
        if (email is null)
        {
            return false;
        }

        // Comparing the parsed address rejects forms like "John <john@mail.com>".
        if (MailAddress.TryCreate(email, out var address) && address.Address == email)
        {
            return true;
        }

        return false;
    }
}
