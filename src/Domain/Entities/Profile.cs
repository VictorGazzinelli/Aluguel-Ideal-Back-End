using System;

namespace AluguelIdeal.Domain.Entities
{
    public class Profile
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public object AsTableRow() =>
            new { user_id = UserId, role_id = RoleId };
    }
}
