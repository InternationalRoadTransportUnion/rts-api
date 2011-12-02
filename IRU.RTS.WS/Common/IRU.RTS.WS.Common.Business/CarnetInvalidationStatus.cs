﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRU.RTS.WS.Common.Business
{
    public enum CarnetInvalidationStatus
    {
        DESTROYED = 92,
        LOST = 93,
        STOLEN = 94,
        RETAINED = 96,
        EXCLUDED = 97,
        INVALIDATED = 99
    }

    public static class CarnetInvalidationStatusExtension
    {
        public static int AsInteger(this CarnetInvalidationStatus val)
        {
            switch (val)
            {
                case CarnetInvalidationStatus.DESTROYED:
                    return 92;
                case CarnetInvalidationStatus.LOST:
                    return 93;
                case CarnetInvalidationStatus.STOLEN:
                    return 94;
                case CarnetInvalidationStatus.RETAINED:
                    return 96;
                case CarnetInvalidationStatus.EXCLUDED:
                    return 97;
                case CarnetInvalidationStatus.INVALIDATED:
                    return 99;
                default:
                    return default(int);
            }
        }

        public static CarnetInvalidationStatus AsCarnetInvalidationStatus(this int val)
        {
            switch (val)
            {
                case 92:
                    return CarnetInvalidationStatus.DESTROYED;
                case 93:
                    return CarnetInvalidationStatus.LOST;
                case 94:
                    return CarnetInvalidationStatus.STOLEN;
                case 96:
                    return CarnetInvalidationStatus.RETAINED;
                case 97:
                    return CarnetInvalidationStatus.EXCLUDED;
                case 99:
                default:
                    return CarnetInvalidationStatus.INVALIDATED;
            }
        }
    }
}