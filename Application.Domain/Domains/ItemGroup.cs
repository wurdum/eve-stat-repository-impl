namespace Application.Domain.Domains
{
    public class ItemGroup : BaseDomainEntity
    {
        public RootGroup RootGroup { get; set; }
        public int IdRootGoup { get; set; }
        public string Name { get; set; }

        public ItemGroup() {}

        #region Equality members

        public bool Equals(ItemGroup other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Name, Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (ItemGroup)) return false;
            return Equals((ItemGroup) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        #endregion
    }
}