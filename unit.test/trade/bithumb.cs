using CCXT.NET.Coin;
using CCXT.NET.Coin.Types;
using CCXT.NET.Converter;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnit
{
    public partial class TradeApi
    {
        [Fact]
        public async void Bithumb()
        {
            var _api_key = TestConfig.GetConnectionKey("Bithumb");
            var _args = new Dictionary<string, object>();
            var _timeframe = "1d";
            var _since = 0; //1514764800000;
            var _limit = 100;

            if (String.IsNullOrEmpty(_api_key.secret_key) == false || XApiClient.TestXUnitMode == XUnitMode.UseJsonFile)
            {
                var _trade_api = new CCXT.NET.Bithumb.Trade.TradeApi(_api_key.connect_key, _api_key.secret_key);

#if DEBUG
                if (XApiClient.TestXUnitMode != XUnitMode.UseExchangeServer)
                    await _trade_api.publicApi.LoadMarkets(false, GetJsonContent(_trade_api.tradeClient, tRootFolder.Replace(@"\trade", @"\public"), "fetchMarkets", _args));
#endif
                _args.Clear();
                {
                    _args.Add("order_id", "1535519253013361");
                    _args.Add("type", "ask");
                }

                var _fetch_my_orders = await _trade_api.FetchMyOrders("BTC", "KRW", _timeframe, _since, _limit, GetJsonContent(_trade_api.tradeClient, "fetchMyOrders", _args));
                if ((_fetch_my_orders.supported == true || TestConfig.SupportedCheck == true) && _fetch_my_orders.statusCode != 5600)
                {
                    this.WriteJson(_trade_api.tradeClient, _fetch_my_orders);

                    Assert.NotNull(_fetch_my_orders);
                    Assert.True(_fetch_my_orders.success, $"fetch my orders return error: {_fetch_my_orders.message}");
                    Assert.True(_fetch_my_orders.message == "success");
                    Assert.False(String.IsNullOrEmpty(_fetch_my_orders.marketId));

                    foreach (var _all_order in _fetch_my_orders.result)
                    {
                        Assert.False(String.IsNullOrEmpty(_all_order.orderId));

                        Assert.True(_all_order.quantity > 0.0m);
                        Assert.True(_all_order.price > 0.0m);
                        Assert.True(_all_order.amount == Math.Round(_all_order.quantity * _all_order.price, 0, MidpointRounding.AwayFromZero));
                    }
                }

                var _my_order_id = "";
                if (_fetch_my_orders.result.Count <= 0)
                    _my_order_id = _trade_api.tradeClient.GenerateNonceString(16);
                else
                    _my_order_id = _fetch_my_orders.result[0].orderId;

                _args.Clear();
                {
                    _args.Add("order_id", "1535519253013361");
                    _args.Add("type", "ask");
                }

                var _fetch_my_order = await _trade_api.FetchMyOrder("BTC", "KRW", _my_order_id, GetJsonContent(_trade_api.tradeClient, "fetchMyOrder", _args));
                if ((_fetch_my_order.supported == true || TestConfig.SupportedCheck == true) && _fetch_my_order.statusCode != 5600)
                {
                    this.WriteJson(_trade_api.tradeClient, _fetch_my_order);

                    Assert.NotNull(_fetch_my_order);
                    Assert.True(_fetch_my_order.success, $"fetch my order return error: {_fetch_my_order.message}");
                    Assert.True(_fetch_my_order.message == "success");
                    Assert.False(String.IsNullOrEmpty(_fetch_my_order.marketId));

                    Assert.False(String.IsNullOrEmpty(_fetch_my_order.result.orderId));
                    Assert.True(_fetch_my_order.result.quantity > 0.0m);
                    Assert.True(_fetch_my_order.result.price > 0.0m);
                    Assert.True(_fetch_my_order.result.amount == Math.Round(_fetch_my_order.result.quantity * _fetch_my_order.result.price, 0, MidpointRounding.AwayFromZero));
                }

                _args.Clear();
                {
                    _args.Add("order_id", "1535519253013361");
                    _args.Add("type", "ask");
                };

                var _fetch_open_orders = await _trade_api.FetchOpenOrders("BTC", "KRW", GetJsonContent(_trade_api.tradeClient, "fetchOpenOrders", _args));
                if ((_fetch_open_orders.supported == true || TestConfig.SupportedCheck == true) && _fetch_open_orders.statusCode != 5600)
                {
                    this.WriteJson(_trade_api.tradeClient, _fetch_open_orders);

                    Assert.NotNull(_fetch_open_orders);
                    Assert.True(_fetch_open_orders.success, $"fetch open orders return error: {_fetch_open_orders.message}");
                    Assert.True(_fetch_open_orders.message == "success");
                    Assert.False(String.IsNullOrEmpty(_fetch_open_orders.marketId));

                    foreach (var _o in _fetch_open_orders.result)
                    {
                        Assert.False(String.IsNullOrEmpty(_o.orderId));
                        Assert.False(String.IsNullOrEmpty(_o.symbol));
                        Assert.True(_o.quantity > 0.0m);
                        Assert.True(_o.price > 0.0m);
                        Assert.True(_o.amount == _o.quantity * _o.price);
                    }
                }

                var _all_open_orders = await _trade_api.FetchAllOpenOrders(GetJsonContent(_trade_api.tradeClient, "fetchAllOpenOrders", _args));
                if (_all_open_orders.supported == true || TestConfig.SupportedCheck == true)
                {
                    this.WriteJson(_trade_api.tradeClient, _all_open_orders);

                    Assert.NotNull(_all_open_orders);
                    Assert.True(_all_open_orders.success, $"fetch all open orders return error: {_all_open_orders.message}");
                    Assert.True(_all_open_orders.message == "success");

                    foreach (var _o in _all_open_orders.result)
                    {
                        Assert.False(String.IsNullOrEmpty(_o.orderId));
                        Assert.False(String.IsNullOrEmpty(_o.symbol));
                        Assert.True(_o.quantity > 0.0m);
                        Assert.True(_o.price > 0.0m);
                        Assert.True(_o.amount == _o.quantity * _o.price);
                    }
                }

                var _open_positions = await _trade_api.FetchAllOpenPositions(GetJsonContent(_trade_api.tradeClient, "fetchAllOpenPositions", _args));
                if (_open_positions.supported == true || TestConfig.SupportedCheck == true)
                {
                    this.WriteJson(_trade_api.tradeClient, _open_positions);

                    Assert.NotNull(_open_positions);
                    Assert.True(_open_positions.success, $"fetch all open positions return error: {_open_positions.message}");
                    Assert.True(_open_positions.message == "success");

                    foreach (var _p in _open_positions.result)
                    {
                        Assert.False(String.IsNullOrEmpty(_p.positionId));
                        Assert.False(String.IsNullOrEmpty(_p.symbol));
                        Assert.True(_p.sideType != SideType.Unknown);
                        Assert.True(Math.Abs(_p.quantity) > 0.0m);
                        Assert.True(_p.price > 0.0m);
                        Assert.True(_p.amount == Math.Abs(_p.quantity) * _p.price);
                        Assert.True(_p.liqPrice >= 0.0m);
                    }
                }

                var _fetch_my_trades = await _trade_api.FetchMyTrades("BTC", "KRW", _timeframe, _since, _limit, GetJsonContent(_trade_api.tradeClient, "fetchMyTrades", _args));
                if (_fetch_my_trades.supported == true || TestConfig.SupportedCheck == true)
                {
                    this.WriteJson(_trade_api.tradeClient, _fetch_my_trades);

                    Assert.NotNull(_fetch_my_trades);
                    Assert.True(_fetch_my_trades.success, $"fetch my trades return error: {_fetch_my_trades.message}");
                    Assert.True(_fetch_my_trades.message == "success");
                    Assert.False(String.IsNullOrEmpty(_fetch_my_trades.marketId));

                    foreach (var _closed_order in _fetch_my_trades.result)
                    {
                        Assert.False(String.IsNullOrEmpty(_closed_order.tradeId));

                        Assert.True(_closed_order.quantity > 0.0m);
                        Assert.True(_closed_order.price > 0.0m);
                        Assert.True(Numerical.CompareDecimal(_closed_order.amount, _closed_order.quantity * _closed_order.price));

                        Assert.True(_closed_order.timestamp >= 1000000000000 && _closed_order.timestamp <= 9999999999999);
                    }
                }
#if DEBUG
                if (XApiClient.TestXUnitMode != XUnitMode.UseExchangeServer)
                {
                    var _limit_order = await _trade_api.CreateLimitOrder("BTC", "KRW", 1.0m, 2000m, SideType.Ask, GetJsonContent(_trade_api.tradeClient, "createLimitOrder", _args));
                    if ((_limit_order.supported == true || TestConfig.SupportedCheck == true) && _limit_order.statusCode != 5600)
                    {
                        this.WriteJson(_trade_api.tradeClient, _limit_order);

                        Assert.NotNull(_limit_order);
                        Assert.True(_limit_order.success, $"create limit order return error: {_limit_order.message}");
                        Assert.True(_limit_order.message == "success");

                        Assert.False(String.IsNullOrEmpty(_limit_order.result.orderId));
                        Assert.False(String.IsNullOrEmpty(_limit_order.marketId));
                        Assert.True(_limit_order.result.orderType == OrderType.Limit);
                    }

                    var _market_order = await _trade_api.CreateMarketOrder("BTC", "KRW", 1.0m, 2000m, SideType.Ask, GetJsonContent(_trade_api.tradeClient, "createMarketOrder", _args));
                    if ((_market_order.supported == true || TestConfig.SupportedCheck == true) && _market_order.statusCode != 5600)
                    {
                        this.WriteJson(_trade_api.tradeClient, _market_order);

                        Assert.NotNull(_market_order);
                        Assert.True(_market_order.success, $"create market order return error: {_market_order.message}");
                        Assert.True(_market_order.message == "success");

                        Assert.False(String.IsNullOrEmpty(_market_order.result.orderId));
                        Assert.False(String.IsNullOrEmpty(_market_order.marketId));
                        Assert.True(_market_order.result.orderType == OrderType.Market);
                    }
                }
#endif
                var _open_order_id = "";
                if (_fetch_open_orders.result.Count <= 0)
                    _open_order_id = _trade_api.tradeClient.GenerateNonceString(16);
                else
                    _open_order_id = _fetch_open_orders.result[0].orderId;

                _args.Clear();
                {
                    _args.Add("type", "bid");
                }

                var _cancel_order = await _trade_api.CancelOrder("BTC", "KRW", _open_order_id, 0.1m, 20000000m, SideType.Ask, GetJsonContent(_trade_api.tradeClient, "cancelOrder", _args));
                if ((_cancel_order.supported == true || TestConfig.SupportedCheck == true) && _cancel_order.statusCode != 5600)
                {
                    this.WriteJson(_trade_api.tradeClient, _cancel_order);

                    Assert.NotNull(_cancel_order);
                    Assert.True(_cancel_order.success, $"cancel order return error: {_cancel_order.message}");
                    Assert.True(_cancel_order.message == "success");

                    Assert.False(String.IsNullOrEmpty(_cancel_order.result.orderId));
                    Assert.False(String.IsNullOrEmpty(_cancel_order.marketId));
                }

                var _oder_ids = new string[] { _open_order_id };

                var _cancel_orders = await _trade_api.CancelOrders("BTC", "KRW", _oder_ids, GetJsonContent(_trade_api.tradeClient, "cancelOrders", _args));
                if ((_cancel_orders.supported == true || TestConfig.SupportedCheck == true) && _cancel_orders.errorCode != ErrorCode.AuthenticationError)
                {
                    this.WriteJson(_trade_api.tradeClient, _cancel_orders);

                    Assert.NotNull(_cancel_orders);
                    Assert.True(_cancel_orders.success, $"cancel orders return error: {_cancel_orders.message}");
                    Assert.True(_cancel_orders.message == "success");
                }

                var _cancel_all_orders = await _trade_api.CancelAllOrders(GetJsonContent(_trade_api.tradeClient, "cancelAllOrders", _args));
                if ((_cancel_all_orders.supported == true || TestConfig.SupportedCheck == true) && _cancel_all_orders.errorCode != ErrorCode.AuthenticationError)
                {
                    this.WriteJson(_trade_api.tradeClient, _cancel_all_orders);

                    Assert.NotNull(_cancel_all_orders);
                    Assert.True(_cancel_all_orders.success, $"cancel all orders return error: {_cancel_all_orders.message}");
                    Assert.True(_cancel_all_orders.message == "success");
                }
            }
        }
    }
}