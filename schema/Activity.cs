namespace Models
{
    public partial class Activity
    {
        public bool ShouldSerializeSchema()
        {
            return false;
        }
    }
    public partial class UserInfo
    {
        public bool ShouldSerializeSchema()
        {
            return false;
        }
    }
    public partial class DealTicket
    {
        public bool ShouldSerializeSchema()
        {
            return false;
        }
    }
}