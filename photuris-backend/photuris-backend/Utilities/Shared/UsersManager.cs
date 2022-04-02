using Microsoft.EntityFrameworkCore;
using photuris_backend.DbContext;
using photuris_backend.DbContext.Entities;

namespace photuris_backend.Utilities.Shared
{
    public class UsersManager
    {
        private readonly Repository _repository;

        public UsersManager(Repository repository)
        {
            _repository = repository;
        }

        public async Task<User> GetUser(string sessionToken)
        {
            var currentUserSession = await _repository.Sessions.FirstOrDefaultAsync(s => s.Token == sessionToken);
            if (currentUserSession == null) throw new Exception("user session not found.");

            var currentUser = await _repository.Users.FirstOrDefaultAsync(u => u.Id == currentUserSession.UserId);
            if (currentUser == null) throw new Exception("session expired.");

            return currentUser;
        }
    }
}
