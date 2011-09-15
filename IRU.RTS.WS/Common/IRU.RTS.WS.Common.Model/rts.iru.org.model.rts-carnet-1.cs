using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace IRU.RTS.WS.Common.Model
{
    public partial class EntityBase<T>
    {
        public EntityBase()
        {
            this.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyInfo pi = sender.GetType().GetProperty(e.PropertyName);
            object oVal = pi.GetValue(sender, null);

            PropertyInfo pi2 = sender.GetType().GetProperty(e.PropertyName + "Specified");
            if (pi2 != null)
            {
                object oVal2 = pi2.GetValue(sender, null);
                if (oVal2 is bool)
                {
                    if ((!(oVal is string)) || (!String.IsNullOrEmpty((string)oVal)))
                        pi2.SetValue(sender, true, null);
                }
            }
        }
    }

    public partial class stoppedCarnetsType
    {
        public stoppedCarnetsType()
        {
            Total = new stoppedCarnetsTypeTotal();
            StoppedCarnets = new stoppedCarnetsTypeStoppedCarnets();            
        }
    }

    public partial class stoppedCarnetsTypeStoppedCarnets
    {
        public stoppedCarnetsTypeStoppedCarnets()
        {
            StoppedCarnet = new List<stoppedCarnetType>();
        }
    }

    public static class Extensions
    {
        public static int AsInteger(this invalidationStatusType val)
        {
            switch (val)
            {
                case invalidationStatusType.Destroyed:
                    return 92;
                case invalidationStatusType.Lost:
                    return 93;
                case invalidationStatusType.Stolen:
                    return 94;
                case invalidationStatusType.Retained:
                    return 96;
                case invalidationStatusType.Excluded:
                    return 97;
                case invalidationStatusType.Invalidated:
                    return 99;
                default:
                    return default(int);
            }
        }

        public static invalidationStatusType AsinvalidationStatusType(this int val)
        {
            switch (val)
            {
                case 92:
                    return invalidationStatusType.Destroyed;
                case 93:
                    return invalidationStatusType.Lost;
                case 94:
                    return invalidationStatusType.Stolen;
                case 96:
                    return invalidationStatusType.Retained;
                case 97:
                    return invalidationStatusType.Excluded;
                case 99:
                default:
                    return invalidationStatusType.Invalidated;
            }
        }
    }
}
