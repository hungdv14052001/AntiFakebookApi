using System.Dynamic;
using System.Reflection;
using System.Text;

namespace AntiFakebookApi.Dto
{
    public class MessageData
    {
        public string Code { get; set; } = "1000";
        public object Data { get; set; }
        public string Message { get; set; } = "null";

        public MessageData(object data)
        {
            Data = data;
        }
    }
}
