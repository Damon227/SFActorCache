// ***********************************************************************
// Solution         : FengNiaoWu
// Project          : Credit.Kolibre.Foundation
// File             : ApartmentEventCode.cs
// Created          : 2017-03-22  5:04 PM
// ***********************************************************************
// <copyright>
//     Copyright © 2016 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

namespace Credit.Kolibre.Foundation.Logging
{
    public partial class EventCode
    {
        //Apartment EventCode

        /*
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
        */

        // 60000 Credit.Kolibre.Apartment BEGIN
        public const int CREDIT_KOLIBRE_KOLIBRE_APARTMENT_BASE = CREDIT_KOLIBRE_BASE + 50000;
        // 69999 Credit.Kolibre.Apartment END
        public const int CREDIT_KOLIBRE_KOLIBRE_APARTMENT_PLATFORM_BASE = 60000 + 2000;
    }
}