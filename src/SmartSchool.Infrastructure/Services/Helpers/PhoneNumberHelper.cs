namespace SmartSchool.Infrastructure.Services.WhatsApp.Helpers;

public static class PhoneNumberHelper
{
    public static string Normalize(string phone)
    {
        phone = phone.Trim();

        if (phone.StartsWith("+62"))
            phone = "62" + phone[3..];

        if (phone.StartsWith("08"))
            phone = "62" + phone[1..];

        if (phone.StartsWith("8"))
            phone = "62" + phone;

        return phone;
    }
}