using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Vermilion.EVE.Modules;

namespace Vermilion.EVE.Functions
{
  public class CreateItemList
  {
    [FunctionName("CreateItemList")]
    public async Task RunAsync(
        [TimerTrigger("0 0 * * * *")] TimerInfo myTimer,
        [Blob("static-assets/items.json", FileAccess.Write), StorageAccount("VEAssetsStorage")] TextWriter output,
        ILogger log)
    {
      log.LogInformation($"CreateItemList started at: {DateTime.Now}");

      var creator = new ItemListCreator();
      var items = await creator.RunAsync();
      string json = JsonConvert.SerializeObject(items);
      await output.WriteAsync(json);
      await output.FlushAsync();

      log.LogInformation($"CreateItemList finished at: {DateTime.Now}");
    }
  }
}
