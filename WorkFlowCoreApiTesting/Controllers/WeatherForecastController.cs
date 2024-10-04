using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWorkflowHost _host;

    public WeatherForecastController()
    {

        var serviceProvider = new ServiceCollection()
        .AddLogging()
        .AddWorkflow()
        .AddWorkflowDSL()
        .BuildServiceProvider();

        _host = serviceProvider.GetService<IWorkflowHost>();
        if (_host == null)
            throw new Exception("Workflow Host not initialized");
    

        _host.Start();
        LoadDefinition(serviceProvider);
    }

    [HttpGet(Name = "GetWorkFlow")]
    public IActionResult StartWorkflow()
    {
        // Start the workflow "ProcessPaymentWorkflow"
        _host.StartWorkflow("ProcessPaymentWorkflow");

        return Ok("Workflow Started");
    }

    public static void LoadDefinition(IServiceProvider serviceProvider)
    {
        var type = Deserializers.Json;
       /* var data = @"
    {
    ""Id"": ""ProcessPaymentWorkflow"",
    ""Version"": 1,
    ""Steps"": [
        {
        ""Id"": ""Initialize"",
        ""StepType"": ""WorkFlowCoreApiTesting.ProcessPayment.Steps.Initialize, WorkFlowCoreApiTesting"",
        ""NextStepId"": ""ApplyShipping""
        },
        {
        ""Id"": ""ApplyDiscount"",
        ""StepType"": ""WorkFlowCoreApiTesting.ProcessPayment.Steps.ApplyDiscount, WorkFlowCoreApiTesting"",
        ""NextStepId"": ""Finalize""
        },
        {
        ""Id"": ""ApplyShipping"",
        ""StepType"": ""WorkFlowCoreApiTesting.ProcessPayment.Steps.ApplyShipping, WorkFlowCoreApiTesting"",
        ""NextStepId"": ""ApplyDiscount""
        },
        {
        ""Id"": ""Finalize"",
        ""StepType"": ""WorkFlowCoreApiTesting.ProcessPayment.Steps.Finalize, WorkFlowCoreApiTesting""
        }
    ]
}
        ";*/

        var file = System.IO.File.ReadAllText(@"D:\testproject\WorkFlowCoreApiTesting\WorkFlowCoreApiTesting\ProcessPayment\ProcessPaymentWorkflow.json");

        var loader = serviceProvider.GetService<IDefinitionLoader>();
        if (loader == null)
            throw new Exception("Definition Loader not initialized");

        loader.LoadDefinition(file, type);
    }
}
