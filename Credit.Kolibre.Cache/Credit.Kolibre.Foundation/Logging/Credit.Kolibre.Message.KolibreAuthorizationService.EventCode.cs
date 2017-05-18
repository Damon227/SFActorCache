// ***********************************************************************
// Solution         : FengNiaoWu
// Project          : Credit.Kolibre.Foundation
// File             : Credit.Kolibre.Message.KolibreAuthorizationService.EventCode.cs
// Created          : 2017-04-14  9:45
// ***********************************************************************
// <copyright>
//     Copyright © 2016 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

namespace Credit.Kolibre.Foundation.Logging
{
    public partial class EventCode
    {
        /*
                    70000 - 70099              Credit.Kolibre.Message.KolibreAuthorizationService
        */

        // 70000 - 70099 Credit.Kolibre.Message.KolibreAuthorizationService BEGIN
        public const int CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_BASE = CREDIT_KOLIBRE_MESSAGE_BASE;

        public const int CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_CREATE_SUCCESSFULLY = CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_BASE + 1;
        public const int CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_UPDATE_SUCCESSFULLY = CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_BASE + 2;
        public const int CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_FIND_SUCCESSFULLY = CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_BASE + 3;
        public const int CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_TOKEN_DISABLE = CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_BASE + 4;
        public const int CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_TOKEN_EXPIRED = CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_BASE + 5;
        public const int CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_TOKEN_NOT_VERIFIED = CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_BASE + 6;
        public const int CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_TOKEN_USED = CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_BASE + 7;
        public const int CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_TOKEN_VALID = CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_BASE + 8;
        public const int CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_TOKEN_NOT_EXIST = CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_BASE + 9;
        public const int CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_TOKEN_USED_OR_EXPIRED = CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_BASE + 10;
        public const int CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_DIFF_TEMPLATE_ADN_REQUEST_PARAMS = CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_BASE + 11;

        public const int CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_SEND_SMS_TEMPLATE_NOT_EXIST = CREDIT_KOLIBRE_MESSAGE_AUTHORIZATION_SERVICE_BASE + 12;
        // 70099 Credit.Kolibre.Message.KolibreAuthorizationService END
    }
}