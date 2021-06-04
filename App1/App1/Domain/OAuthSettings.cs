using System;
using System.Collections.Generic;
using System.Text;

namespace Approagro.Domain
{
    public static class OAuthSettings
    {
        public const string ApplicationId = "c6661989-d798-4868-95cd-1d660b45ca1a";
        public const string Scopes = "User.Read MailboxSettings.Read Calendars.ReadWrite User.Read Calendars.Read Files.ReadWrite.AppFolder";
        public const string RedirectUri = "msauth://com.aproagro.approagro";
    }
}
