namespace NZWalksAPI.Models.Domain
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        //NAvigation property
        public List<User_Role> UserRoles { get; set; }

    }
}
