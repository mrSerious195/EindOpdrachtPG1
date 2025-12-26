using KlantenSimulatorBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Domein
{
    public class Adress
    {
        public Adress(string huisNr, Straat straat)
        {
            HuisNr = huisNr;
            Straat = straat;
        }

        private string _huisnr;
        public string HuisNr {  get=> _huisnr;
            set {
                if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value.Trim()))
                {
                    throw new KlantenException("Adress._huisnr is null or empty");
                }
                else
                {
                    _huisnr = value;
                }

            } 
        }

        private Straat _straat;
        public Straat Straat { get=>_straat;
            set
            {
                if (value == null)
                {
                    throw new KlantenException("Adress.Straat is null");
                }
                else
                {
                    _straat = value;
                }
            }
                
                }

        public override bool Equals(object? obj)
        {
            return obj is Adress adress &&
                   _huisnr == adress._huisnr &&
                   EqualityComparer<Straat>.Default.Equals(_straat, adress._straat);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_huisnr, _straat);
        }
    }
}
