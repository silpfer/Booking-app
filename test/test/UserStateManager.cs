using System;
using System.Collections.Generic;
using System.Text;

namespace test
{
    public static class UserStateManager
    {
        public static int CurrentUserId { get; private set; } = -1;

        public static void SetCurrentUser(int userId)
        {
            CurrentUserId = userId;
        }
    }
}
