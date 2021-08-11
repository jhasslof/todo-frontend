namespace webui.Models
{
    public class TodoItemViewModel
    {
        public long Id { get; set; } = 0;
        public string Name { get; set; } = "";
        public bool IsComplete { get; set; } = false;
        public string ErrorMessage { get; set; } = "";
        public string Note { get; set; } = "";
    }
}
