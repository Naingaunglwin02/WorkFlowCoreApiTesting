namespace WorkFlowCoreApiTesting.ProcessPayment.Steps
{
    public class Finalize : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Finalize");
            return ExecutionResult.Next();
        }
    }
}
