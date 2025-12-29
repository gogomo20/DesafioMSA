namespace DesafioMSA.Domain.Shared
{
    public abstract class BaseEntity
    {
        public virtual long Id { get; protected set;  }
        public virtual DateTime DateCreation { get; protected set; }
        public virtual bool Deleted { get; protected set; } = false;
        public virtual void Delete()
        {
            Deleted = true;
        }
        public virtual void Created()
        {
            DateCreation = DateTime.Now;
        }
    }
}
