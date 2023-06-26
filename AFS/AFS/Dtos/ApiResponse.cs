namespace AFS.Dtos
{
    public class ApiResponse
    {
        public Contents contents;
        public Success success;
    }
    public class Contents
    {
        public string translated { get; set; }
        public string text { get; set; }
        public string translation { get; set; }
    }
    public class Success
    {
        public int total { get; set; }
    }
}
