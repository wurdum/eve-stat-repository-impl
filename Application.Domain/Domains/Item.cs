namespace Application.Domain.Domains
{
    public class Item : BaseDomainEntity
    {
        public ItemGroup ItemGroup { get; set; }
        public int IdItemGroup { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Volume { get; set; }
        public string Image { get; set; }

        public Item()
        {
        }

        #region Equality members

        public bool Equals(Item other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Name, Name) && other.IdItemGroup == IdItemGroup &&
                   Equals(other.Description, Description) && other.Volume.Equals(Volume) && Equals(other.Image, Image);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Item)) return false;
            return Equals((Item) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (Name != null ? Name.GetHashCode() : 0);
                result = (result*397) ^ IdItemGroup;
                result = (result*397) ^ (Description != null ? Description.GetHashCode() : 0);
                result = (result*397) ^ Volume.GetHashCode();
                result = (result*397) ^ (Image != null ? Image.GetHashCode() : 0);
                return result;
            }
        }

        #endregion
    }
}