# SK-DEV - Semantic Kernel Development Project

A .NET console application demonstrating Azure OpenAI integration using Microsoft Semantic Kernel with streaming chat completion and intelligent chat history management.

## Overview

This project showcases a conversational AI application built with Microsoft Semantic Kernel that connects to Azure OpenAI services. It features streaming responses, token usage tracking, and automatic chat history summarization to maintain context while managing conversation length.

## Features

- **Azure OpenAI Integration**: Seamless connection to Azure OpenAI Chat Completion services
- **Streaming Responses**: Real-time streaming of AI responses for better user experience
- **Chat History Management**: Automatic summarization of conversation history to maintain context
- **Token Usage Tracking**: Real-time monitoring of prompt, output, and total token consumption
- **Configurable AI Behavior**: Customizable system prompts, temperature, and other OpenAI parameters
- **Interactive Console Interface**: Simple command-line chat interface

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- Azure OpenAI Service account with API access
- Azure OpenAI deployment with a chat completion model (e.g., GPT-4, GPT-3.5-turbo)

## Installation

1. Clone the repository:
```bash
git clone <repository-url>
cd SK-DEV
```

2. Restore dependencies:
```bash
dotnet restore
```

## Configuration

### Option 1: Using appsettings.json (Recommended)

1. Copy the example configuration file:
```bash
copy appsettings.example.json appsettings.json
```

2. Edit `appsettings.json` with your Azure OpenAI credentials:
```json
{
  "AzureOpenAI": {
    "ModelId": "your-deployment-name",
    "Endpoint": "https://your-resource.openai.azure.com/",
    "ApiKey": "your-api-key"
  }
}
```

### Option 2: Using .env file

1. Copy the example environment file:
```bash
copy .env.example .env
```

2. Edit `.env` with your credentials:
```
AZURE_OPENAI_MODEL_ID=your-deployment-name
AZURE_OPENAI_ENDPOINT=https://your-resource.openai.azure.com/
AZURE_OPENAI_API_KEY=your-api-key
```

**Note**: The current implementation uses `appsettings.json` by default.

## Usage

1. Build the project:
```bash
dotnet build
```

2. Run the application:
```bash
dotnet run
```

3. Start chatting:
```
Kernel with Azure OpenAI Chat Completion service created successfully.

Enter your prompt : Hello, can you help me with C# development?
```

4. Press Enter without typing to exit the application.

## Project Structure

```
SK-DEV/
├── Program.cs                    # Main application entry point
├── SK-DEV.csproj                # Project configuration and dependencies
├── appsettings.json             # Configuration file (not in repo)
├── appsettings.example.json     # Example configuration template
├── .env                         # Environment variables (not in repo)
├── .env.example                 # Example environment template
└── README.md                    # This file
```

## Key Components

### Semantic Kernel Setup
The application initializes a Semantic Kernel instance with Azure OpenAI Chat Completion service:
```csharp
var builder = Kernel.CreateBuilder();
builder.AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);
Kernel kernel = builder.Build();
```

### OpenAI Execution Settings
Configurable parameters for AI behavior:
- **System Prompt**: Defines the AI's role and expertise
- **Max Tokens**: Maximum response length (1000)
- **Temperature**: Creativity level (0.7)
- **Top P**: Nucleus sampling parameter (0.95)
- **Frequency/Presence Penalty**: Controls repetition (0)

### Chat History Reducer
Implements `ChatHistorySummarizationReducer` to automatically summarize conversation history when it exceeds 2 messages, maintaining context while reducing token usage.

## Dependencies

- **Microsoft.SemanticKernel** (v1.68.0): Core Semantic Kernel framework
- **Microsoft.SemanticKernel.Connectors.OpenAI** (v1.68.0): Azure OpenAI connector
- **Microsoft.Extensions.Configuration** (v10.0.1): Configuration management
- **Microsoft.Extensions.Configuration.Json** (v10.0.1): JSON configuration provider
- **DotNetEnv** (v3.1.1): Environment variable management

## Token Usage Monitoring

The application displays token usage after each response:
```
Tokens Used: Prompt - 45, Output - 120, Total - 165
```

This helps you monitor API costs and optimize conversation length.

## Security Best Practices

- Never commit `appsettings.json` or `.env` files to version control
- Use Azure Key Vault for production deployments
- Rotate API keys regularly
- Implement rate limiting for production use
- Monitor token usage to control costs

## Troubleshooting

### Configuration Not Found
**Error**: `AzureOpenAI:ModelId not found in appsettings.json`

**Solution**: Ensure `appsettings.json` exists and contains all required fields.

### Connection Issues
**Error**: Unable to connect to Azure OpenAI endpoint

**Solution**: 
- Verify your endpoint URL is correct
- Check that your API key is valid
- Ensure your Azure OpenAI resource is active
- Verify network connectivity

### Model Not Found
**Error**: Model deployment not found

**Solution**: Use your actual deployment name (not the model name) in the `ModelId` field.

## Development

### Building
```bash
dotnet build
```

### Running in Debug Mode
```bash
dotnet run --configuration Debug
```

### Cleaning Build Artifacts
```bash
dotnet clean
```

## Future Enhancements

Potential improvements for this project:
- Add plugin support for extended functionality
- Implement conversation persistence (save/load chat history)
- Add support for multiple AI models
- Create a web-based UI
- Add function calling capabilities
- Implement RAG (Retrieval Augmented Generation)
- Add conversation export functionality

## Contributing

Contributions are welcome! Please follow these steps:
1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Open a Pull Request

## License

[Specify your license here]

## Resources

- [Microsoft Semantic Kernel Documentation](https://learn.microsoft.com/en-us/semantic-kernel/)
- [Azure OpenAI Service Documentation](https://learn.microsoft.com/en-us/azure/cognitive-services/openai/)
- [.NET 9.0 Documentation](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9)

## Support

For issues and questions:
- Open an issue in the repository
- Check existing documentation
- Review Azure OpenAI service status

---

**Note**: This is a development/demonstration project. For production use, implement proper error handling, logging, authentication, and security measures.
