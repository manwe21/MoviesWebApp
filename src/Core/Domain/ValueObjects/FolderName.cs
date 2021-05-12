namespace Core.Domain.ValueObjects
{
    public class FolderName
    {
        public string Value { get; private set; }
        
        static FolderName(){}
        
        private FolderName(){}

        private FolderName(string value)
        {
            Value = value;
        }
        
        public static string WatchLater => "Watch Later";
        public static string FavoriteMovies => "Favorite Movies";

        public static implicit operator FolderName(string name) => new FolderName(name);

        public static bool operator ==(FolderName name1, FolderName name2)
        {
            if (name1 == null || name2 == null)
                return false;
            return name1.Equals(name2);
        }

        public static bool operator !=(FolderName name1, FolderName name2)
        {
            return !(name1 == name2);
        }
        
        protected bool Equals(FolderName other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FolderName) obj);
        }

        public override int GetHashCode()
        {
            return Value != null ? Value.GetHashCode() : 0;
        }

    }
}
