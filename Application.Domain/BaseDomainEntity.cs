namespace Application.Domain
{
    public abstract class BaseDomainEntity
    {
        public int ID { get; set; }

        protected BaseDomainEntity()
        {
            ID = -1;
        }

        public bool IsNew()
        {
            return ID == -1;
        }
    }
}