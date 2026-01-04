using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.Extensions.Configuration;

namespace SK_DEV
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Load configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var modelId = configuration["AzureOpenAI:ModelId"]
                ?? throw new InvalidOperationException("AzureOpenAI:ModelId not found in appsettings.json");
            var endpoint = configuration["AzureOpenAI:Endpoint"]
                ?? throw new InvalidOperationException("AzureOpenAI:Endpoint not found in appsettings.json");
            var apiKey = configuration["AzureOpenAI:ApiKey"]
                ?? throw new InvalidOperationException("AzureOpenAI:ApiKey not found in appsettings.json");

            var builder = Kernel.CreateBuilder();
            builder.AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);

            Kernel kernel = builder.Build();
            Console.WriteLine("Kernel with Azure OpenAI Chat Completion service created successfully.");

            var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

            // creating a chat history
            var history = new ChatHistory();

            // this only works with openAI connector
            OpenAIPromptExecutionSettings settings = new()
            {
                ChatSystemPrompt = "You are a resourceful developer. You have much knowledge on software development",
                MaxTokens = 1000,
                Temperature = 0.7,
                TopP = 0.95,
                FrequencyPenalty = 0,
                PresencePenalty = 0
            };

            // chat history reducer
            var reducer = new ChatHistoryTruncationReducer(targetCount: 2);

            while (true)
            {
                Console.Write("\nEnter your prompt : ");
                var prompt = Console.ReadLine();

                if(string.IsNullOrEmpty(prompt))
                {
                    break;
                }

                history.AddUserMessage(prompt);
                var response = await chatCompletionService.GetChatMessageContentAsync(history, settings);

                // add to the chat history
                history.Add(response);

                OpenAI.Chat.ChatTokenUsage usage = ((OpenAI.Chat.ChatCompletion)response.InnerContent).Usage;
                Console.WriteLine(response.Content);
                Console.WriteLine($"\nTokens Used: Prompt - {usage.InputTokenCount}, Output - {usage.OutputTokenCount}, Total - {usage.TotalTokenCount}");

                var recducedMessages = await reducer.ReduceAsync(history);

                if (recducedMessages is not null)
                {
                    history = new(recducedMessages);
                }
            }

        }
    }
}
