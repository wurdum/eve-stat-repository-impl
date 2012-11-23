﻿namespace Application.Domain.Domains
{
    public class RootGroup:BaseDomainEntity
    {
        public string Name { get; set; }

        public RootGroup()
        {
        }

        #region equality members

        public bool Equals(RootGroup other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Name, Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (RootGroup)) return false;
            return Equals((RootGroup) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        #endregion
    }
}