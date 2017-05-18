// ***********************************************************************
// Solution         : FengNiaoWu
// Project          : Credit.Kolibre.Foundation
// File             : MessageEventCode.cs
// Created          : 2017-03-14  16:11
// ***********************************************************************
// <copyright>
//     Copyright © 2016 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

namespace Credit.Kolibre.Foundation.Logging
{
    public partial class EventCode
    {
        // Message EventCode

        /*
                70000 - 71000                  Credit.Kolibre.Message
                    70000 - 70099              Credit.Kolibre.Message.KolibreAuthorizationService
                    70100 - 70199              Credit.Kolibre.Message.SmsService 
                    70200 - 70299              Credit.Kolibre.FengNiaoWu.SmsStatelessActor
        */

        // 70000  Credit.Kolibre.Message BEGIN
        public const int CREDIT_KOLIBRE_MESSAGE_BASE = CREDIT_KOLIBRE_BASE + 60000;

        // 71000  Credit.Kolibre.Message END
    }
}