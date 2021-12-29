using System;

namespace TW.Vault.Model.Convert
{
    public static class UserConvert
    {
        public static JSON.User ModelToJson(Scaffold.User user, String playerName = null, String tribeName = null)
        {
            var result = new JSON.User();
            result.PlayerId = user.PlayerId;
            result.IsAdmin = user.PermissionsLevel >= (short)Security.PermissionLevel.Admin;
            result.PlayerName = playerName;
            result.TribeName = tribeName;

            if (user.PermissionsLevel < (short)Security.PermissionLevel.System)
                result.Key = user.AuthToken.ToString();

            return result;
        }
    }
}
