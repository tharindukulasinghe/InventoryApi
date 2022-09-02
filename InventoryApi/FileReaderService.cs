using InventoryApi.Data;

namespace InventoryApi
{
    public class FileReaderService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<FileReaderService> _logger;
        private Timer? _timer = null;
        IServiceScopeFactory _serviceScopeFactory;

        public FileReaderService(ILogger<FileReaderService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);

            try
            {

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                    //other logic
                    foreach (string line in System.IO.File.ReadLines(@"InventoryItems.txt"))
                    {
                        var lineTrimmed = line.Trim();
                        Console.WriteLine(lineTrimmed);

                        String[] parts = lineTrimmed.Split(',');
                        
                        String itemCode = parts[0].Trim();
                        String itemName = parts[1].Trim();
                        String contactNo = parts[2].Trim();
                        String issue = parts[3].Trim().Trim(';');
                        
                        var item = getItem(itemCode, context);

                        if (item  == null)
                        {
                            Item newItem = new Item
                            {
                                ItemCode = itemCode,
                                ItemName = itemName
                            };

                            context.Items.Add(newItem);
                            await context.SaveChangesAsync();
                        }

                        var dbItem = getItem(itemCode, context);

                        Repair repair = new Repair
                        {
                            ContactNo = contactNo,
                            Issue = issue,
                            Status = (int)Status.Open,
                            Item = dbItem
                        };

                        context.Repairs.Add(repair);
                        await context.SaveChangesAsync();

                    }
                    moveFile();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Item? getItem(String itemCode, DataContext context)
        {
            return context.Items.SingleOrDefault(item => item.ItemCode == itemCode);
        }

        public void moveFile()
        {
            string path = @"InventoryItems.txt";
            string path2 = @"archieve/InventoryItems.txt";
            try
            {
                if (!File.Exists(path))
                {
                    using (FileStream fs = File.Create(path)) { }
                }
                if (File.Exists(path2))
                    File.Delete(path2);
                File.Move(path, path2);

                Console.WriteLine("{0} was moved to {1}.", path, path2);
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

    }
}
