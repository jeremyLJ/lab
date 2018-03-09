using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace StockTicker
{
    public class StockTicker
    {
        private static readonly Lazy<StockTicker> _instance = new Lazy<StockTicker>(() => new StockTicker(GlobalHost.ConnectionManager.GetHubContext<StockTickerHub>().Clients));
        private readonly ConcurrentDictionary<string, Stock> _stocks = new ConcurrentDictionary<string, Stock>();

        private readonly object _updatePriceStockLock = new object();

        // stock can go up or down by percentage of this factor  on each change
        private readonly double _rangePercent = 0.02;

        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(250);
        private readonly Random _updateOrNotRandom = new Random();

        private readonly Timer _timer;
        private volatile bool _updatingStockPrices = false;

        private StockTicker(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;

            _stocks.Clear();
            var stocks = new List<Stock>
            {
                new Stock {Symbol = "MSFT", Price = 30.31m},
                new Stock { Symbol = "APPL", Price = 578.18m },
                new Stock { Symbol = "GOOG", Price = 570.30m }
            };
            stocks.ForEach(stock => _stocks.TryAdd(stock.Symbol, stock));

            _timer = new Timer(UpdateStockPrices, null, _updateInterval, _updateInterval);
        }

        private void UpdateStockPrices(object state)
        {
            lock (_updatePriceStockLock)
            {
                if (!_updatingStockPrices)
                {
                    _updatingStockPrices = true;

                    foreach (var stock in _stocks.Values)
                    {
                        if (TryUpdateStockPrice(stock))
                        {
                            BroadcastStockPrice(stock);
                        }
                    }

                    _updatingStockPrices = false;
                }
            }
        }

        private void BroadcastStockPrice(Stock stock)
        {
            Clients.All.updateStockPrice(stock);
        }

        private bool TryUpdateStockPrice(Stock stock)
        {
            // Random choose whether to update this stock or not
            var r = _updateOrNotRandom.NextDouble();
            if (r > .1)
            {
                return false;
            }

            // Update the stock price by a random factor of the range percentage
            var random = new Random((int)Math.Floor(stock.Price));
            var percentChange = random.NextDouble() * _rangePercent;

            var pos = random.NextDouble() > .51;
            var change = Math.Round(stock.Price * (decimal) percentChange, 2);
            change = pos ? change : -change;

            stock.Price += change;
            return true;
        }

        public IHubConnectionContext<dynamic> Clients { get; set; }

        public static StockTicker Instance
        {
            get { return _instance.Value; }
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            return _stocks.Values;
        }
    }
}