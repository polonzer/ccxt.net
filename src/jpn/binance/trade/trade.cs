﻿using Newtonsoft.Json;
using OdinSdk.BaseLib.Coin.Trade;
using OdinSdk.BaseLib.Coin.Types;

namespace CCXT.NET.Binance.Trade
{
    /// <summary>
    ///
    /// </summary>
    public class BMyTradeItem : OdinSdk.BaseLib.Coin.Trade.MyTradeItem, IMyTradeItem
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "orderId")]
        public override string orderId
        {
            get;
            set;
        }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public override string tradeId
        {
            get;
            set;
        }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "qty")]
        public override decimal quantity
        {
            get;
            set;
        }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "price")]
        public override decimal price
        {
            get;
            set;
        }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public override decimal amount
        {
            get;
            set;
        }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "time")]
        public override long timestamp
        {
            get;
            set;
        }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "isBuyer")]
        private bool sideValue
        {
            set
            {
                sideType = (value == true) ? SideType.Bid : SideType.Ask;
            }
        }
    }
}