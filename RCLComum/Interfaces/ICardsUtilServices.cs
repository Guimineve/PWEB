using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLComum.Interfaces
{
    public interface ICardsUtilsServices
    {
        int Index { get; set; }
        int CountSlide { get; set; }
        List<string> MarginLeftSlide { get; set; }
        event Action OnChange;
    }
}
