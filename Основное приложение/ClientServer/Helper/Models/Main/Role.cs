namespace Helper.Models.Main
{
    public class Role : Base
    {
        public string Name { get; set; } = string.Empty;
        public override string ToString()
        {
            return this.Name;
        }

        //public int CountUsers
        //{
        //    get
        //    {
        //        if (Users == null)
        //        {
        //            return 0;
        //        }
        //        return Users.Count();
        //    }
        //}
    }
}
