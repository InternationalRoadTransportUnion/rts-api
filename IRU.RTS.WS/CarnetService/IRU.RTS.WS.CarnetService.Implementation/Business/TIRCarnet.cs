using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRU.RTS.WS.CarnetService.Implementation.Business
{
    public class TIRCarnet
    {
        string _carnetNumber = null;

        public string CarnetNumber
        {
            set
            {
                _carnetNumber = value;
                int iVal;
                if (int.TryParse(_carnetNumber, out iVal))
                {
                    _carnetNumber = CheckChar(iVal) + _carnetNumber.Trim();
                }
            }
            
            get
            {
                return _carnetNumber;
            }
        }

        public TIRCarnet()
        {
        }

        public TIRCarnet(string carnetNumber): base()
        {
            CarnetNumber = carnetNumber;
        }

        private string CheckChar(long l_CarnetNo)
        {
            string s_CheckChar = "";
            long l_Carnet_Mod_23 = 0;
            int n_CheckLetter = 0;
            char c = 'A';

            if (l_CarnetNo < 25000000)
            {
                s_CheckChar = "";
            }
            else
            {
                l_Carnet_Mod_23 = (l_CarnetNo % 23);
                n_CheckLetter = (int)((decimal)(c) + ((3 * l_Carnet_Mod_23 + 17) % 26));

                if (l_Carnet_Mod_23 < 12)
                {
                    s_CheckChar = (((char)(n_CheckLetter)).ToString() + 'X');
                }
                else
                {
                    s_CheckChar = ('X' + ((char)(n_CheckLetter)).ToString());
                }
            }
            return s_CheckChar;
        }

    }
}
