﻿namespace Moo2U.Services {
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Moo2U.Model;
    using SQLite;

    public class DeliveryStopService : IDeliveryStopService {

        readonly SQLiteConnection _cn;
        readonly SQLiteAsyncConnection _cnA;

        public DeliveryStopService(ISQLiteConnectionService sqLiteConnectionService) {
            if (sqLiteConnectionService == null) {
                throw new ArgumentNullException(nameof(sqLiteConnectionService));
            }
            _cn = sqLiteConnectionService.GetConnection();
            _cnA = sqLiteConnectionService.GetAsyncConnection();
        }

        public Task<List<DeliveryListItem>> GetDeliveryListItemsForDateAsync(DateTime dateDelivery) {
            return _cnA.QueryAsync<DeliveryListItem>("SELECT DS.*, ORD.ItemPercentDelivered, ORD.OrderStatus, C.Address, C.CityStateZip, C.AddressType, C.Name FROM DeliveryStop DS INNER JOIN [Order] ORD ON DS.OrderId = ORD.Id INNER JOIN Customer C ON ORD.CustomerId = C.Id WHERE DS.DateDelivery = ?", dateDelivery);
        }

        public Int32 Insert(DeliveryStop deliveryStop) {
            if (deliveryStop == null) {
                throw new ArgumentNullException(nameof(deliveryStop));
            }
            return _cn.Insert(deliveryStop);
        }

    }
}
