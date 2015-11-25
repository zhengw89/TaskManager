using System.Web;

namespace TaskManager.Helper
{
    public static class ContextHelper
    {
        private const string UserIdSessionKey = "USER_ID";
        private const string UserName = "USER_NAME";

        #region Public Method

        public static void SetCurrentUserId(string userId)
        {
            SetSession(UserIdSessionKey, userId, true);
        }

        public static string GetCurrentUserId()
        {
            return GetSession(UserIdSessionKey);
        }

        public static void SetCurrentUserName(string userName)
        {
            HttpContext.Current.Session[UserName] = userName;
        }

        public static string GetCurrentUserName()
        {
            var obj = HttpContext.Current.Session[UserName];
            return (obj as string);
        }

        public static void CleanCurrentSession()
        {
            RemoveSession(UserIdSessionKey);
            RemoveSession(UserName);
        }

        #endregion

        #region Private Method

        private static void SetSession(string key, string value, bool canOverride)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            if (!canOverride && HttpContext.Current.Session[key] != null)
            {
                return;
            }
            HttpContext.Current.Session[key] = value;
        }

        private static string GetSession(string key)
        {
            var obj = HttpContext.Current.Session[key];
            return obj == null ? null : obj.ToString();
        }

        private static void RemoveSession(string key)
        {
            if (HttpContext.Current.Session[key] != null)
            {
                HttpContext.Current.Session.Remove(key);
            }
        }

        #endregion
    }
}