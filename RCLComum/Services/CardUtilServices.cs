using RCLComum.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLComum.Services
{
    public class CardsUtilsServices : ICardsUtilsServices
    {
        private int _value = 0;
        public int Index
        {
            get => _value;
            set
            {
                _value = value;
                NotificationOnChange();
            }
        }

        private int _valueCont = 0;
        public int CountSlide
        {
            get => _valueCont;
            set
            {
                _valueCont = value;
                NotificationOnChange();
            }
        }

        // Lista que guarda o "style='margin-left:...'" de cada cartão
        private List<string> _marginLeftSlide = new List<string>();
        public List<string> MarginLeftSlide
        {
            get => _marginLeftSlide;
            set
            {
                _marginLeftSlide = value;
                NotificationOnChange();
            }
        }

        public event Action OnChange;
        private void NotificationOnChange() => OnChange?.Invoke();
    }
}
