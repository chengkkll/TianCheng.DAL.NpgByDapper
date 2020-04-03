namespace WebDemo.Model
{

    public class MockGuidQuery : TianCheng.DAL.NpgByDapper.QueryInfo
    {
        public string FirstName { get; set; }

        public string LikeName { get; set; }

        public bool? IsDelete { get; set; } = null;
    }
}
