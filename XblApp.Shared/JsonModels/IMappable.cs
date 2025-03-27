using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XblApp.DTO.JsonModels
{
    public interface IMappable<T>
    {
        List<T> MapTo();
    }

    public interface IAchievementMappable<T,K> : IMappable<T>
    {
        List<K> MapTo(long xuid);
    }
}
