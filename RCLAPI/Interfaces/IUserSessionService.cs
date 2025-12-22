using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RCLAPI.DTO;

namespace RCLAPI.Interface;

public interface IUserSessionService
{
    Task Login(Token token);
    Task Logout();
    Task<Token?> GetToken();
    Task<bool> IsUserLoggedIn();
}