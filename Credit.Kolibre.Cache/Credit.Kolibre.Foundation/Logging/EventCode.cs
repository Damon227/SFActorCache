// ***********************************************************************
// Solution         : FengNiaoWu
// Project          : Credit.Kolibre.Foundation
// File             : EventCode.cs
// Created          : 2017-03-13  19:59
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
               10000 - 19999                   Credit.Kolibre.Foundation.AspNetCore
                   10000 - 10999               Credit.Kolibre.Foundation.AspNetCore.Common
                   11000 - 11999               Credit.Kolibre.Foundation.AspNetCore.Identity
                   12000 - 12999               Credit.Kolibre.Foundation.AspNetCore.Authentication
                       12000 - 12199           Credit.Kolibre.Foundation.AspNetCore.Authentication.Session
                   13000 - 13999               Credit.Kolibre.Foundation.AspNetCore.Cache
                   14000 - 14999               Credit.Kolibre.Foundation.AspNetCore.Session

               20000 - 49999                   Credit.Kolibre.Server
                   20000 - 20099               Credit.Kolibre.Server.Dev
                       20000 - 20010           Credit.Kolibre.Server.Dev.Index
                   20100 - 20199               Credit.Kolibre.Server.Utilities
                       20100 - 20140           Credit.Kolibre.Server.Utilities.Files
                   21000 - 21999               Credit.Kolibre.Server.Identities
                   22000 - 39999               Credit.Kolibre.Server.Apartment.Platform
                       22000 - 22099           Credit.Kolibre.Server.Apartment.Platform.HouseAccount
                       22100 - 22199           Credit.Kolibre.Server.Apartment.Platform.HouseAccountInfo
                       22200 - 22299           Credit.Kolibre.Server.Apartment.Platform.HouseInformation
                       22300 - 22399           Credit.Kolibre.Server.Apartment.Platform.LeaseOrder
                       22400 - 22499           Credit.Kolibre.Server.Apartment.Platform.LeaseTransaction
                       22500 - 22599           Credit.Kolibre.Server.Apartment.Platform.Lease
                       22600 - 22699           Credit.Kolibre.Server.Apartment.Platform.Credit
              
                60000 - 69999                  Credit.Kolibre.Apartment
                   60000 - 61999               Credit.Kolibre.Apartment.Data
                   62000 - 62999               Credit.Kolibre.Apartment.Platform
                       62000 - 62099           Credit.Kolibre.Apartment.Platform.HouseAccountService
                       62100 - 62199           Credit.Kolibre.Apartment.Platform.HouseAccountInfoService
                       62200 - 62299           Credit.Kolibre.Apartment.Platform.HouseInformationService
                       62300 - 62399           Credit.Kolibre.Apartment.Platform.LeaseOrderService
                       62400 - 62499           Credit.Kolibre.Apartment.Platform.LeaseService
                       62500 - 62599           Credit.Kolibre.Apartment.Platform.LeaseTransactionService
                       62600 - 62699           Credit.Kolibre.Apartment.Platform.ConractService
                       62700 - 62799           Credit.Kolibre.Apartment.Platform.HouseAccountExtensionInfoService
                       62800 - 62899           Credit.Kolibre.Apartment.Platform.StagingOrderService
                   64000 - 64299               Credit.Kolibre.Apartment.CreditService

                70000 - 71000                  Credit.Kolibre.Message
                    70000 - 70099              Credit.Kolibre.Message.KolibreAuthorizationService
                    70100 - 70199              Credit.Kolibre.Message.SmsService
                    70200 - 70299              Credit.Kolibre.FengNiaoWu.SmsStatelessActor

               100000 - 199999                 Credit.Kolibre.Apartment.DataService

               500000 - 699999                 Credit.Kolibre.Stack 
       */

        // 10000 Credit.Kolibre.Foundation.AspNetCore Credit.Kolibre.Foundation.AspNetCore.Common BEGIN
        public const int CREDIT_KOLIBRE_BASE = 10000;

        public const int CREDIT_KOLIBRE_FOUNDATION_ASPNETCORE_BASE = CREDIT_KOLIBRE_BASE;
        public const int CREDIT_KOLIBRE_FOUNDATION_ASPNETCORE_ERROR_UNEXPECTED = CREDIT_KOLIBRE_FOUNDATION_ASPNETCORE_BASE + 1;

        public const int CREDIT_KOLIBRE_FOUNDATION_ASPNETCORE_ERROR_DB_UPDATE_CONCURRENCY = CREDIT_KOLIBRE_FOUNDATION_ASPNETCORE_BASE + 2;
        // 1200 ~ 1599 已经分配到 http 对应的响应事件中
        // 10999 Credit.Kolibre.Foundation.AspNetCore.Common END

        // 11000 Credit.Kolibre.Foundation.AspNetCore.Identity BEGIN
        public const int CREDIT_KOLIBRE_IDENTITY_BASE = CREDIT_KOLIBRE_FOUNDATION_ASPNETCORE_BASE + 1000;

        public const int CREDIT_KOLIBRE_IDENTITY_USER_ALREADY_HAS_PASSWORD = CREDIT_KOLIBRE_IDENTITY_BASE + 1;
        public const int CREDIT_KOLIBRE_IDENTITY_CHANGE_PASSWORD_FAILED = CREDIT_KOLIBRE_IDENTITY_BASE + 2;
        public const int CREDIT_KOLIBRE_IDENTITY_03 = CREDIT_KOLIBRE_IDENTITY_BASE + 3;
        public const int CREDIT_KOLIBRE_IDENTITY_04 = CREDIT_KOLIBRE_IDENTITY_BASE + 4;
        public const int CREDIT_KOLIBRE_IDENTITY_USER_ALREADY_IN_ROLE = CREDIT_KOLIBRE_IDENTITY_BASE + 5;
        public const int CREDIT_KOLIBRE_IDENTITY_USER_NOT_IN_ROLE = CREDIT_KOLIBRE_IDENTITY_BASE + 6;
        public const int CREDIT_KOLIBRE_IDENTITY_CHANGE_PHONE_NUMBER_FAILED_WITH_INVALID_TOKEN = CREDIT_KOLIBRE_IDENTITY_BASE + 7;
        public const int CREDIT_KOLIBRE_IDENTITY_VERIFY_CHANGE_PHONE_NUMBER_TOKEN_FAILED = CREDIT_KOLIBRE_IDENTITY_BASE + 8;
        public const int CREDIT_KOLIBRE_IDENTITY_VERIFY_TOKEN_FAILED_WITH_PURPOSE = CREDIT_KOLIBRE_IDENTITY_BASE + 9;
        public const int CREDIT_KOLIBRE_IDENTITY_10 = CREDIT_KOLIBRE_IDENTITY_BASE + 10;
        public const int CREDIT_KOLIBRE_IDENTITY_INVALID_PASSWORD = CREDIT_KOLIBRE_IDENTITY_BASE + 11;
        public const int CREDIT_KOLIBRE_IDENTITY_USER_LOCKED_OUT = CREDIT_KOLIBRE_IDENTITY_BASE + 12;
        public const int CREDIT_KOLIBRE_IDENTITY_USER_VALIDATION_FAILED = CREDIT_KOLIBRE_IDENTITY_BASE + 13;
        public const int CREDIT_KOLIBRE_IDENTITY_USER_PASSWORD_VALIDATION_FAILED = CREDIT_KOLIBRE_IDENTITY_BASE + 14;
        public const int CREDIT_KOLIBRE_IDENTITY_15 = CREDIT_KOLIBRE_IDENTITY_BASE + 15;
        public const int CREDIT_KOLIBRE_IDENTITY_16 = CREDIT_KOLIBRE_IDENTITY_BASE + 16;
        public const int CREDIT_KOLIBRE_IDENTITY_17 = CREDIT_KOLIBRE_IDENTITY_BASE + 17;
        public const int CREDIT_KOLIBRE_IDENTITY_18 = CREDIT_KOLIBRE_IDENTITY_BASE + 18;
        public const int CREDIT_KOLIBRE_IDENTITY_19 = CREDIT_KOLIBRE_IDENTITY_BASE + 19;
        public const int CREDIT_KOLIBRE_IDENTITY_20 = CREDIT_KOLIBRE_IDENTITY_BASE + 20;
        public const int CREDIT_KOLIBRE_IDENTITY_LOCKOUT_FAILED_BECAUSE_LOCKOUT_NOT_ENABLE = CREDIT_KOLIBRE_IDENTITY_BASE + 21;
        public const int CREDIT_KOLIBRE_IDENTITY_22 = CREDIT_KOLIBRE_IDENTITY_BASE + 22;
        public const int CREDIT_KOLIBRE_IDENTITY_23 = CREDIT_KOLIBRE_IDENTITY_BASE + 23;
        public const int CREDIT_KOLIBRE_IDENTITY_24 = CREDIT_KOLIBRE_IDENTITY_BASE + 24;
        public const int CREDIT_KOLIBRE_IDENTITY_25 = CREDIT_KOLIBRE_IDENTITY_BASE + 25;
        public const int CREDIT_KOLIBRE_IDENTITY_26 = CREDIT_KOLIBRE_IDENTITY_BASE + 26;
        public const int CREDIT_KOLIBRE_IDENTITY_27 = CREDIT_KOLIBRE_IDENTITY_BASE + 27;
        public const int CREDIT_KOLIBRE_IDENTITY_28 = CREDIT_KOLIBRE_IDENTITY_BASE + 28;
        public const int CREDIT_KOLIBRE_IDENTITY_29 = CREDIT_KOLIBRE_IDENTITY_BASE + 29;
        public const int CREDIT_KOLIBRE_IDENTITY_USER_CAN_NOT_SIGN_WITHOUT_COMFIRMED_EMAIL = CREDIT_KOLIBRE_IDENTITY_BASE + 30;
        public const int CREDIT_KOLIBRE_IDENTITY_USER_CAN_NOT_SIGN_WITHOUT_COMFIRMED_PHONE_NUMBER = CREDIT_KOLIBRE_IDENTITY_BASE + 31;
        public const int CREDIT_KOLIBRE_IDENTITY_USER_CAN_NOT_SIGN_WHILE_LOCKED_OUT = CREDIT_KOLIBRE_IDENTITY_BASE + 32;
        public const int CREDIT_KOLIBRE_IDENTITY_USER_FAILED_TO_PROVIDE_PASSWORD = CREDIT_KOLIBRE_IDENTITY_BASE + 33;
        public const int CREDIT_KOLIBRE_IDENTITY_34 = CREDIT_KOLIBRE_IDENTITY_BASE + 34;
        public const int CREDIT_KOLIBRE_IDENTITY_35 = CREDIT_KOLIBRE_IDENTITY_BASE + 35;
        public const int CREDIT_KOLIBRE_IDENTITY_36 = CREDIT_KOLIBRE_IDENTITY_BASE + 36;
        public const int CREDIT_KOLIBRE_IDENTITY_37 = CREDIT_KOLIBRE_IDENTITY_BASE + 37;

        public const int CREDIT_KOLIBRE_IDENTITY_ERROR_CONCURRENCY_FAILURE = CREDIT_KOLIBRE_IDENTITY_BASE + 100;
        public const int CREDIT_KOLIBRE_IDENTITY_ERROR_DEFAULT = CREDIT_KOLIBRE_IDENTITY_BASE + 101;
        public const int CREDIT_KOLIBRE_IDENTITY_ERROR_DUPLICATE_EMAIL = CREDIT_KOLIBRE_IDENTITY_BASE + 102;
        public const int CREDIT_KOLIBRE_IDENTITY_ERROR_DUPLICATE_ROLE_NAME = CREDIT_KOLIBRE_IDENTITY_BASE + 103;
        public const int CREDIT_KOLIBRE_IDENTITY_ERROR_DUPLICATE_USER_NAME = CREDIT_KOLIBRE_IDENTITY_BASE + 104;
        public const int CREDIT_KOLIBRE_IDENTITY_ERROR_INVALID_EMAIL = CREDIT_KOLIBRE_IDENTITY_BASE + 105;
        public const int CREDIT_KOLIBRE_IDENTITY_ERROR_INVALID_ROLE_NAME = CREDIT_KOLIBRE_IDENTITY_BASE + 106;
        public const int CREDIT_KOLIBRE_IDENTITY_ERROR_INVALID_USER_NAME = CREDIT_KOLIBRE_IDENTITY_BASE + 107;
        public const int CREDIT_KOLIBRE_IDENTITY_ERROR_PASSWORD_MISMATCH = CREDIT_KOLIBRE_IDENTITY_BASE + 108;
        public const int CREDIT_KOLIBRE_IDENTITY_ERROR_PASSWORD_REQUIRES_DIGIT = CREDIT_KOLIBRE_IDENTITY_BASE + 109;
        public const int CREDIT_KOLIBRE_IDENTITY_ERROR_PASSWORD_REQUIRES_LOWER = CREDIT_KOLIBRE_IDENTITY_BASE + 110;
        public const int CREDIT_KOLIBRE_IDENTITY_ERROR_PASSWORD_REQUIRES_NON_ALPHAUMERIC = CREDIT_KOLIBRE_IDENTITY_BASE + 111;
        public const int CREDIT_KOLIBRE_IDENTITY_ERROR_PASSWORD_REQUIRES_UPPER = CREDIT_KOLIBRE_IDENTITY_BASE + 112;
        public const int CREDIT_KOLIBRE_IDENTITY_ERROR_PASSWORD_TOO_SHORT = CREDIT_KOLIBRE_IDENTITY_BASE + 113;
        public const int CREDIT_KOLIBRE_IDENTITY_ERROR_USER_ALREADY_HAS_PASSWORD = CREDIT_KOLIBRE_IDENTITY_BASE + 114;
        public const int CREDIT_KOLIBRE_IDENTITY_ERROR_USER_ALREADY_IN_ROLE = CREDIT_KOLIBRE_IDENTITY_BASE + 115;
        public const int CREDIT_KOLIBRE_IDENTITY_ERROR_USER_LOCKOUT_NOT_ENABLED = CREDIT_KOLIBRE_IDENTITY_BASE + 116;
        public const int CREDIT_KOLIBRE_IDENTITY_ERROR_USER_NOT_IN_ROLE = CREDIT_KOLIBRE_IDENTITY_BASE + 117;
        public const int CREDIT_KOLIBRE_IDENTITY_118 = CREDIT_KOLIBRE_IDENTITY_BASE + 118;

        public const int CREDIT_KOLIBRE_IDENTITY_119 = CREDIT_KOLIBRE_IDENTITY_BASE + 119;
        // 11999 Credit.Kolibre.Foundation.AspNetCore.Identity END

        // 12000 Credit.Kolibre.Foundation.AspNetCore.Authentication Credit.Kolibre.Foundation.AspNetCore.Authentication.Session BEGIN
        public const int CREDIT_KOLIBRE_AUTHENTICATION_BASE = CREDIT_KOLIBRE_FOUNDATION_ASPNETCORE_BASE + 2000;

        // 12199 Credit.Kolibre.Foundation.AspNetCore.Authentication.Session END
        // 12999 Credit.Kolibre.Foundation.AspNetCore.Authentication END

        // 13000 Credit.Kolibre.Foundation.AspNetCore.Cache BEGIN
        public const int CREDIT_KOLIBRE_CACHE_BASE = CREDIT_KOLIBRE_FOUNDATION_ASPNETCORE_BASE + 3000;

        public const int CREDIT_KOLIBRE_CACHE_JSON_REDIS_GET_BEGIN = CREDIT_KOLIBRE_CACHE_BASE + 1;
        public const int CREDIT_KOLIBRE_CACHE_JSON_REDIS_GET_END = CREDIT_KOLIBRE_CACHE_BASE + 2;
        public const int CREDIT_KOLIBRE_CACHE_JSON_REDIS_SET_BEGIN = CREDIT_KOLIBRE_CACHE_BASE + 3;
        public const int CREDIT_KOLIBRE_CACHE_JSON_REDIS_SET_END = CREDIT_KOLIBRE_CACHE_BASE + 4;
        public const int CREDIT_KOLIBRE_CACHE_JSON_REDIS_REFRESH_BEGIN = CREDIT_KOLIBRE_CACHE_BASE + 5;
        public const int CREDIT_KOLIBRE_CACHE_JSON_REDIS_REFRESH_END = CREDIT_KOLIBRE_CACHE_BASE + 6;
        public const int CREDIT_KOLIBRE_CACHE_JSON_REDIS_REMOVE_BEGIN = CREDIT_KOLIBRE_CACHE_BASE + 7;
        public const int CREDIT_KOLIBRE_CACHE_JSON_REDIS_REMOVE_END = CREDIT_KOLIBRE_CACHE_BASE + 8;
        public const int CREDIT_KOLIBRE_CACHE_HASH_REDIS_GET_BEGIN = CREDIT_KOLIBRE_CACHE_BASE + 9;
        public const int CREDIT_KOLIBRE_CACHE_HASH_REDIS_GET_END = CREDIT_KOLIBRE_CACHE_BASE + 10;
        public const int CREDIT_KOLIBRE_CACHE_HASH_REDIS_SET_BEGIN = CREDIT_KOLIBRE_CACHE_BASE + 11;
        public const int CREDIT_KOLIBRE_CACHE_HASH_REDIS_SET_END = CREDIT_KOLIBRE_CACHE_BASE + 12;
        public const int CREDIT_KOLIBRE_CACHE_HASH_REDIS_REFRESH_BEGIN = CREDIT_KOLIBRE_CACHE_BASE + 13;
        public const int CREDIT_KOLIBRE_CACHE_HASH_REDIS_REFRESH_END = CREDIT_KOLIBRE_CACHE_BASE + 14;
        public const int CREDIT_KOLIBRE_CACHE_HASH_REDIS_REMOVE_BEGIN = CREDIT_KOLIBRE_CACHE_BASE + 15;
        public const int CREDIT_KOLIBRE_CACHE_HASH_REDIS_REMOVE_END = CREDIT_KOLIBRE_CACHE_BASE + 16;
        public const int CREDIT_KOLIBRE_CACHE_LIST_REDIS_GET_BEGIN = CREDIT_KOLIBRE_CACHE_BASE + 17;
        public const int CREDIT_KOLIBRE_CACHE_LIST_REDIS_GET_END = CREDIT_KOLIBRE_CACHE_BASE + 18;
        public const int CREDIT_KOLIBRE_CACHE_LIST_REDIS_SET_BEGIN = CREDIT_KOLIBRE_CACHE_BASE + 19;
        public const int CREDIT_KOLIBRE_CACHE_LIST_REDIS_SET_END = CREDIT_KOLIBRE_CACHE_BASE + 20;
        public const int CREDIT_KOLIBRE_CACHE_LIST_REDIS_REFRESH_BEGIN = CREDIT_KOLIBRE_CACHE_BASE + 21;
        public const int CREDIT_KOLIBRE_CACHE_LIST_REDIS_REFRESH_END = CREDIT_KOLIBRE_CACHE_BASE + 22;
        public const int CREDIT_KOLIBRE_CACHE_LIST_REDIS_REMOVE_BEGIN = CREDIT_KOLIBRE_CACHE_BASE + 23;
        public const int CREDIT_KOLIBRE_CACHE_LIST_REDIS_REMOVE_END = CREDIT_KOLIBRE_CACHE_BASE + 24;

        // 13999 Credit.Kolibre.Foundation.AspNetCore.Cache END

        // 14000 Credit.Kolibre.Foundation.AspNetCore.Session BEGIN
        public const int CREDIT_KOLIBRE_SESSION_BASE = CREDIT_KOLIBRE_FOUNDATION_ASPNETCORE_BASE + 4000;

        public const int CREDIT_KOLIBRE_SESSION_ERROR_COMMIT = CREDIT_KOLIBRE_SESSION_BASE + 1;
        public const int CREDIT_KOLIBRE_SESSION_INIT = CREDIT_KOLIBRE_SESSION_BASE + 2;
        public const int CREDIT_KOLIBRE_SESSION_SESSION_MISSING = CREDIT_KOLIBRE_SESSION_BASE + 3;
        public const int CREDIT_KOLIBRE_SESSION_SESSION_LOADED = CREDIT_KOLIBRE_SESSION_BASE + 4;
        public const int CREDIT_KOLIBRE_SESSION_SESSION_STORED = CREDIT_KOLIBRE_SESSION_BASE + 5;
        public const int CREDIT_KOLIBRE_SESSION_ERROR_SESSION_LOAD_OR_INIT = CREDIT_KOLIBRE_SESSION_BASE + 6;
        public const int CREDIT_KOLIBRE_SESSION_ID_MISSMATCH = CREDIT_KOLIBRE_SESSION_BASE + 7;
        public const int CREDIT_KOLIBRE_SESSION_USER_ID_MISSMATCH = CREDIT_KOLIBRE_SESSION_BASE + 8;
        public const int CREDIT_KOLIBRE_SESSION_09 = CREDIT_KOLIBRE_SESSION_BASE + 9;
        public const int CREDIT_KOLIBRE_SESSION_10 = CREDIT_KOLIBRE_SESSION_BASE + 10;

        // 14999 Credit.Kolibre.Foundation.AspNetCore.Session END

        // 19999 Credit.Kolibre.Foundation.AspNetCore END


        public static bool IsKolibreCreditEvent(int eventCode)
        {
            return eventCode >= CREDIT_KOLIBRE_BASE;
        }
    }
}