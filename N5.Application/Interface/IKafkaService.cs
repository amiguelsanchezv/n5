using System.Threading.Tasks;

namespace N5.Application
{
    public interface IKafkaService
    {
        Task WriteKafka(string operation);
    }
}
